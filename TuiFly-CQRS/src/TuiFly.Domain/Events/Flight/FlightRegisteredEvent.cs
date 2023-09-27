using System;
using NetDevPack.Messaging;

namespace TuiFly.Domain.Events.Flight
{
    public class FlightRegisteredEvent : Event
    {
        public FlightRegisteredEvent(Guid id, string airline, string flightNumber, 
            string departureCity, string arrivalCity, DateTime departureDate,
            DateTime? arrivalDate, decimal price, int numberOfAvailableSeats,
            Guid planId
            )
        {
            Id = id;
            Airline = airline;
            FlightNumber = flightNumber;
            DepartureCity = departureCity;
            ArrivalCity = arrivalCity;
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            Price = price;
            NumberOfAvailableSeats = numberOfAvailableSeats;
            PlanId = id;
        }
        public Guid Id { get; set; }
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public decimal Price { get; set; }
        public int NumberOfAvailableSeats { get; set; }

        // Navigation property to represent the associated aircraft
        public Guid PlanId { get; set; }
    }
}