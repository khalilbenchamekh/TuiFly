using MediatR;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Flight.Validation;
using TuiFly.Domain.Events.Reservation;
using TuiFly.Domain.Interfaces.Repository.Flight;
using TuiFly.Domain.Interfaces.Repository.Reservation;
using TuiFly.Domain.Models.Entities;
using TuiFly.Domain.Interfaces.Repository.SeatArrangement;
using TuiFly.Domain.Events.Flight;
using System.Linq;

namespace TuiFly.Domain.Commands.Flight.Handler
{
    public class MakeReservationCommandHandler : CommandHandler,
        IRequestHandler<MakeReservationCommandValidation, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IFlightRepository _flightRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ISeatArrangementRepository _seatArrangementRepository;
        
        public MakeReservationCommandHandler(ISeatArrangementRepository seatArrangementRepository, IReservationRepository reservationRepository, IFlightRepository flightRepository,IMediator mediator)
        {
            _mediator = mediator;
            _flightRepository = flightRepository;
            _reservationRepository = reservationRepository;
            _seatArrangementRepository = seatArrangementRepository;
        }

        public async Task<ValidationResult> Handle(MakeReservationCommandValidation message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
            
            var passengersCount = message.Passengers.Count;
            var flightId = message.FlightId;
            // create Reservayion
            var bestChoiceSeats = await _seatArrangementRepository.GetAvailableSeats(flightId, passengersCount);
            if (bestChoiceSeats.Count < passengersCount)
            {
                var res = new ValidationResult();
                res.Errors.Add(new ValidationFailure("MakeReservationCommand", "Not enough available seats for the family."));
                return res;
            }
            var reservations = new List<ReservationEntity>();
            foreach(var seat in bestChoiceSeats)
            {
                var reservation = new ReservationEntity
                {
                    Id = Guid.NewGuid(),
                    NumberOfPassengers = passengersCount,
                    DateReservation = DateTime.Now,
                    SeatNumber = seat,
                    FlightId = flightId
                };
                reservation.AddDomainEvent(new ReservationRegisteredEvent(reservation.Id, reservation.NumberOfPassengers, reservation.SeatNumber, reservation.DateReservation, reservation.FlightId));
                reservations.Add(reservation);
            }
            await _reservationRepository.AddReservationEntity(reservations);

            var resResrvation = await Commit(_reservationRepository.UnitOfWork);
            if (resResrvation.Errors.Any()) return resResrvation;

            // Create Passengers and associed with reservation
            int i = 0;
            foreach(var reservation in reservations)
            {
                message.Passengers[i].ReservationId = reservation.Id;
                message.Passengers[i].SeatNumber = reservation.SeatNumber;
                i++;
            }
            var queryManyCustomers = new CreateMultipleCustomersCommand()
            {
                Customers = message.Passengers
            };
            var validationResult = await _mediator.Send(queryManyCustomers);
            if (!validationResult.IsValid) return validationResult;

            // update Flight
            var entity = await _flightRepository.findById(message.FlightId);
            if (entity is null)
            {
                AddError("The Flight doesn't exists.");
                return ValidationResult;
            }
            entity.NumberOfAvailableSeats -= passengersCount;

            entity.AddDomainEvent(new FlightRegisteredEvent(entity.Id, entity.Airline, entity.FlightNumber,
            entity.DepartureCity, entity.ArrivalCity, entity.DepartureDate, entity.ArrivalDate, entity.Price, entity.NumberOfAvailableSeats, entity.PlanId));

            _flightRepository.Update(entity);
            var resFlight = await Commit(_flightRepository.UnitOfWork);
            if (resFlight.Errors.Any()) return resFlight;

            // update seatArrangement
            var listSeatArrangement = await _seatArrangementRepository.GetSeatArrangement(flightId, bestChoiceSeats);
            foreach(var seat in listSeatArrangement)
            {
                seat.Status = false;
                seat.AddDomainEvent(new SeatArrangementRegisteredEvent(seat.Id, seat.SeatNumber, seat.Status, seat.FlightId));
            }
            _seatArrangementRepository.Update(listSeatArrangement);
            return await Commit(_seatArrangementRepository.UnitOfWork);
        }
    }
}
