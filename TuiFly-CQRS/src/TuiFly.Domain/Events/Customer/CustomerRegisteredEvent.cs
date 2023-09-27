using System;
using NetDevPack.Messaging;

namespace TuiFly.Domain.Events.Customer
{
    public class CustomerRegisteredEvent : Event
    {
        public CustomerRegisteredEvent(Guid id, string firstName, string lastName, string email,string seatNumber, Guid reservationId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            SeatNumber = seatNumber;
            ReservationId = reservationId;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string SeatNumber { get; set; }
        public Guid ReservationId { get; set; }
    }
}