using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuiFly.Domain.Models.Entities;

namespace TuiFly.Domain.Interfaces.Repository.Reservation
{
    public interface IReservationRepository: IRepository<ReservationEntity>
    {
        Task AddReservationEntity(List<ReservationEntity> reservations);
    }
}
