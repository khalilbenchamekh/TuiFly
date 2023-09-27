using System;
using NetDevPack.Messaging;

namespace TuiFly.Domain.Events.Reservation
{
    public class ReservationRegisteredEvent : Event
    {
        public ReservationRegisteredEvent(Guid id, int numberOfPassengers, string seatNumber, DateTime dateReservation,Guid flightId)
        {
            Id = id;
            NumberOfPassengers = numberOfPassengers;
            SeatNumber = seatNumber;
            DateReservation = dateReservation;
            FlightId = flightId;
        }
        public Guid Id { get; set; }
        public int NumberOfPassengers { get; set; }
        public string SeatNumber { get; set; }
        public DateTime DateReservation { get; set; }
        public Guid FlightId { get; set; }
    }
}