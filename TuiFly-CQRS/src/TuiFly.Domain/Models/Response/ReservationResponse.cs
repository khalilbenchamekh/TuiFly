using System;
using System.Collections.Generic;
using TuiFly.Domain.Models.ViewModels;

namespace TuiFly.Domain.Models.Response
{
    public class ReservationResponse
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public int NumberOfPassengers { get; set; }
        public string PassengerName { get; set; }
        public string SeatNumber { get; set; }
        public DateTime DateReservation { get; set; }
        public ICollection<CustomerViewModel> Passengers { get; set; }
    }
}
