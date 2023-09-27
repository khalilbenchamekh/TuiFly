using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TuiFly.Domain.Interfaces.Repository.Seed;
using TuiFly.Domain.Interfaces.Service.Seed;

namespace TuiFly.Application.Services.Seed
{
    public class InitSeedService : IInitSeedService
    {
        private readonly IInitSeedRepository _initSeedRepository;
        private readonly ILogger<IInitSeedService> _logger;
        public InitSeedService(ILoggerFactory loggerFactory,
            IInitSeedRepository initSeedRepository
            )
        {
            _initSeedRepository = initSeedRepository;
            _logger = loggerFactory.CreateLogger<IInitSeedService>();
        }
        public async Task Seed()
        {
            _logger.LogInformation("Seed");
            try
            {
                var plans = await _initSeedRepository.CreatePlan();
                foreach(var plan in plans)
                {
                    var flights = await _initSeedRepository.GenerateFlightsAsync(plan, 10);
                    foreach(var flight in flights)
                    {
                        await _initSeedRepository.CreateArrangement(flight);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Exception seed " + e.Message, e);
            }
        }
    }
}
