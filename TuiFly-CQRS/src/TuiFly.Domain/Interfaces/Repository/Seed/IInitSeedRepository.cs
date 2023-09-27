using System.Collections.Generic;
using System.Threading.Tasks;
using TuiFly.Domain.Models.Entities;

namespace TuiFly.Domain.Interfaces.Repository.Seed
{
    public interface IInitSeedRepository
    {
        Task<List<PlanEntity>> CreatePlan();
        Task CreateArrangement(FlightEntity flight);
        Task<List<FlightEntity>> GenerateFlightsAsync(PlanEntity plan, int numberOfFlights);
    }
}
