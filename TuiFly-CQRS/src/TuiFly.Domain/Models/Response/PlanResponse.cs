using System;
using System.Collections.Generic;

namespace TuiFly.Domain.Models.Response
{
    public class PlanResponse
    {
        public Guid Id { get; set; }
        public string ModelName { get; set; }
        public ICollection<FlightResponse> Flights { get; set; }
    }
}
