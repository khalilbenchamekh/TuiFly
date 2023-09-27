using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuiFly.Domain.Models.Response;
using TuiFly.Domain.Models.ViewModels;

namespace TuiFly.Domain.Interfaces.Service.Flight
{
    public interface IFlightService : IDisposable
    {
        Task<IEnumerable<FlightResponse>> findAll(FlightViewModel request);
        Task<bool> save(ReservationViewModel request);
    }
}
