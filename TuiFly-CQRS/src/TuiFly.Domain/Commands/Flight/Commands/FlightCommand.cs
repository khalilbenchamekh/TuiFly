using NetDevPack.Messaging;
using System;
namespace TuiFly.Domain.Commands.Flight.Commands
{
    public class FlightCommand : Command
    {
        public Guid FlightId { get; set; }
        public int NumberOfAvailableSeats { get; set; }
    }
}
