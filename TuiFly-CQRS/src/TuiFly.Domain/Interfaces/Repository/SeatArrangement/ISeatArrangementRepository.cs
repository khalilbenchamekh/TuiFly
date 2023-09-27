using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuiFly.Domain.Commands.Flight.Commands;
using TuiFly.Domain.Models.Entities;

namespace TuiFly.Domain.Interfaces.Repository.SeatArrangement
{
    public interface ISeatArrangementRepository : IRepository<ReservationEntity>
    {
        void Update(List<SeatArrangementEntity> entities);
        Task<List<SeatArrangementEntity>> GetSeatArrangement(Guid flightId, IList<string> seats);
        int GetRange(string seatNumber);
        Task<IList<string>> GetAvailableSeats(Guid FlightId, int numberOfFamilyMembers);
        char GetPosition(string seatNumber);
        List<string> GenerateFamilySeats(int familySize, List<string> availableSeats);
    }
}
