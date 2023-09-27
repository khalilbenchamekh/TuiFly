using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuiFly.Domain.Interfaces.Repository.Flight;
using TuiFly.Domain.Models.Entities;
using TuiFly.Domain.Models.ViewModels;
using TuiFly.Infra.Data.Context;

namespace TuiFly.Infra.Data.Repository
{
    public class FlightRepository : IFlightRepository
    {
        protected readonly TuiFlyContext Db;
        protected readonly DbSet<FlightEntity> DbSet;
        public IUnitOfWork UnitOfWork => Db;
        public FlightRepository(TuiFlyContext context)
        {
            Db = context;
            DbSet = Db.Set<FlightEntity>();
        }
        
        public async Task<IEnumerable<FlightEntity>> findAll(FlightViewModel request)
        {
            var query = DbSet.AsQueryable(); // Assuming "_context" is your Entity Framework context

            // Ensure DepartureCity, ArrivalCity, and DepartureDate are provided (required)
            if (string.IsNullOrEmpty(request.DepartureCity) || string.IsNullOrEmpty(request.ArrivalCity))
            {
                return Enumerable.Empty<FlightEntity>();
            }

            // Filter by DepartureCity, ArrivalCity, and DepartureDate
            query = query
                .Where(f => f.NumberOfAvailableSeats >= request.CustomersCount)
                .Where(f => f.DepartureCity == request.DepartureCity)
                .Where(f => f.ArrivalCity == request.ArrivalCity)
                .Where(f => f.DepartureDate.Date == request.DepartureDate.Date);

            // Filter by ArrivalDate if provided
            if (request.ArrivalDate.HasValue)
            {
                query = query.Where(f => f.ArrivalDate.HasValue && f.ArrivalDate.Value.Date == request.ArrivalDate.Value.Date);
            }

            // Implement pagination
            int skipCount = (request.PageNumber - 1) * request.PageSize;
            var filteredFlights = await query
                .OrderBy(f => f.Price)
                .Skip(skipCount)
                .Take(request.PageSize)
                .ToListAsync();

            return filteredFlights.AsEnumerable();
        }
        public async Task<FlightEntity?> findById(Guid id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
        public void Update(FlightEntity entity)
        {
            DbSet.Update(entity);
        }
        public async Task AddRanger(List<FlightEntity> entity)
        {
            await DbSet.AddRangeAsync(entity);
        }
        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
