using NetDevPack.Domain;
using System;
using System.Collections.Generic;

namespace TuiFly.Domain.Models.Entities
{
    public class ReservationEntity : Entity, IAggregateRoot
    {
        public int NumberOfPassengers { get; set; }
        public string SeatNumber { get; set; }
        public DateTime DateReservation { get; set; }
        public Guid FlightId { get; set; }
        public FlightEntity Flight { get; set; }
        public ICollection<CustomerEntity> Passengers { get; set; }
    }
}
