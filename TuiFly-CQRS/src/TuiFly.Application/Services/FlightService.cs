using AutoMapper;
using Microsoft.Extensions.Logging;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuiFly.Domain.Commands.Flight.Validation;
using TuiFly.Domain.Interfaces.Repository.Flight;
using TuiFly.Domain.Interfaces.Service.Flight;
using TuiFly.Domain.Models.Response;
using TuiFly.Domain.Models.ViewModels;

namespace TuiFly.Application.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly ILogger<IFlightService> _logger;
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;
        public FlightService(ILoggerFactory loggerFactory , IMapper mapper,
            IFlightRepository flightRepository,
            IMediatorHandler mediator
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _flightRepository = flightRepository;
            _logger = loggerFactory.CreateLogger<IFlightService>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<FlightResponse>> findAll(FlightViewModel request)
        {
            var res = await _flightRepository.findAll(request);
            return _mapper.Map<IEnumerable<FlightResponse>>(res);
        }

        public async Task<bool> save(ReservationViewModel request)
        {
            Guid flightId = request.FlightId;
            _logger.LogInformation("looking for saving the Flight with id: " + flightId.ToString());
            try
            {
                _logger.LogInformation("looking Flight with id: " + flightId.ToString());
                var flight = await _flightRepository.findById(flightId);
                if (flight == null) return false;

                var registerCommand = _mapper.Map<MakeReservationCommandValidation>(request);
                var createReservation =  await _mediator.SendCommand(registerCommand);
                return createReservation.Errors.Any() == false;
            }
            catch (Exception e)
            {
                _logger.LogError("Flight with " + flightId.ToString() + " cannot be added", e);
                return default;
            }
        }
    }
}