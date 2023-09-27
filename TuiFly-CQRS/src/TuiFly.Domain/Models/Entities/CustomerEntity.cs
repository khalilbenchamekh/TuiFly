using System;
using NetDevPack.Domain;

namespace TuiFly.Domain.Models.Entities
{
    public class CustomerEntity : Entity, IAggregateRoot
    {
        public CustomerEntity(Guid id, string firstName, string lastName, string email, string seatNumber, Guid reservationId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ReservationId = reservationId;
            SeatNumber = seatNumber;
        }

        // Empty constructor for EF
        public CustomerEntity() { }

        public string FirstName { get;  set; }
        public string LastName { get;  set; }

        public string Email { get;  set; }

        public string SeatNumber { get; set; }
        public Guid ReservationId { get; set; }
        public ReservationEntity Reservation { get; set; }
    }
}