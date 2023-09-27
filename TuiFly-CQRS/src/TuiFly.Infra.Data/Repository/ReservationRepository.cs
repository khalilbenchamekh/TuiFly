using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuiFly.Domain.Interfaces.Repository.Reservation;
using TuiFly.Domain.Models.Entities;
using TuiFly.Infra.Data.Context;

namespace TuiFly.Infra.Data.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        protected readonly TuiFlyContext Db;
        protected readonly DbSet<ReservationEntity> DbSet;

        public ReservationRepository(TuiFlyContext context)
        {
            Db = context;
            DbSet = Db.Set<ReservationEntity>();
        }

        public IUnitOfWork UnitOfWork => Db;
        public async Task AddReservationEntity(List<ReservationEntity> reservations)
        {
            await DbSet.AddRangeAsync(reservations);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
