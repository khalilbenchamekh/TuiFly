using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using TuiFly.Domain.Commands.Customer.Commands;

namespace TuiFly.Domain.Commands.Flight.Commands
{
    public class MakeReservationCommand : Command
    {
        public Guid FlightId { get; set; }
        public List<RegisterNewCustomerCommand> Passengers { get; set; }
    }
}
