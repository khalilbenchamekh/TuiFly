using NetDevPack.Domain;
using System.Collections.Generic;

namespace TuiFly.Domain.Models.Entities
{
    public class PlanEntity : Entity, IAggregateRoot
    {
        public string ModelName { get; set; }
        public ICollection<FlightEntity> Flights { get; set; }
    }
}
