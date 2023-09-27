using System;
using NetDevPack.Messaging;

namespace TuiFly.Domain.Events.Reservation
{
    public class SeatArrangementRegisteredEvent : Event
    {
        public SeatArrangementRegisteredEvent(Guid id, string seatNumber, bool status, Guid flightId)
        {
            Id = id;
            SeatNumber = seatNumber;
            Status = status;
            FlightId = flightId;
        }
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public string SeatNumber { get; set; }
        public bool Status { get; set; } = true;
    }
}