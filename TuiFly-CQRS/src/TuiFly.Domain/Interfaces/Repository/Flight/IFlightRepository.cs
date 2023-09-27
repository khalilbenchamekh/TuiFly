using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuiFly.Domain.Models.Entities;
using TuiFly.Domain.Models.ViewModels;

namespace TuiFly.Domain.Interfaces.Repository.Flight
{
    public interface IFlightRepository : IRepository<FlightEntity>
    {
        Task<FlightEntity> findById(Guid id);
        void Update(FlightEntity entity);
        Task<IEnumerable<FlightEntity>> findAll(FlightViewModel request);
        Task AddRanger(List<FlightEntity> entity);
    }
}
