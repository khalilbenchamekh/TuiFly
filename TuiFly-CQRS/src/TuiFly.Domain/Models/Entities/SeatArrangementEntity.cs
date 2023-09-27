using NetDevPack.Domain;
using System;

namespace TuiFly.Domain.Models.Entities
{
    public class SeatArrangementEntity : Entity, IAggregateRoot
    {
        public Guid FlightId { get; set; }
        public string SeatNumber { get; set; }
        public bool Status { get; set; } = true;
        public FlightEntity Flight { get; set; }
    }
}
