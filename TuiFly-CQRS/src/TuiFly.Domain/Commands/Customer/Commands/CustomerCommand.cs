using System;
using NetDevPack.Messaging;
namespace TuiFly.Domain.Commands.Customer.Commands
{
    public abstract class CustomerCommand : Command
    {
        public Guid Id { get;  set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SeatNumber { get; set; }
        public Guid ReservationId { get; set; }
    }
}