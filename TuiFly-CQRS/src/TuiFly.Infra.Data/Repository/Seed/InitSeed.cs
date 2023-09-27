using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuiFly.Domain.Interfaces.Repository.Flight;
using TuiFly.Domain.Interfaces.Repository.Seed;
using TuiFly.Domain.Models.Entities;
using TuiFly.Infra.Data.Context;

namespace TuiFly.Infra.Data.Repository.Seed
{
    public class InitSeedRepository : IInitSeedRepository
    {
        private readonly TuiFlyContext _dbContext;
        private readonly ILogger<InitSeedRepository> _logger;
        private readonly IFlightRepository _flightRepository;

        public InitSeedRepository(ILoggerFactory loggerFactory, TuiFlyContext context, IFlightRepository flightRepository)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory.CreateLogger<InitSeedRepository>();
            _flightRepository = flightRepository;
        }
        public async Task<List<PlanEntity>> CreatePlan()
        {
            try
            {
                _logger.LogInformation("save InitSeedRepository CreatePlan");
                var planFaker = new Faker<PlanEntity>()
                                .RuleFor(p => p.ModelName, f => f.Random.Word());

                var plans = planFaker.Generate(2); // Generate 50 fake plan entities
                await _dbContext.AddRangeAsync(plans);
                await _dbContext.SaveChangesAsync();
                // Retrieve the list of plans (including any database-generated IDs)
                var savedPlans = await _dbContext.Plans.ToListAsync();
                // Return the list of saved plans
                return savedPlans;
            }
            catch (Exception e)
            {
                _logger.LogError("error save InitSeedRepository", e);
                return Enumerable.Empty<PlanEntity>().ToList();
            }
        }
        
        public async Task CreateArrangement(FlightEntity flight)
        {
            try
            {
                _logger.LogInformation("save InitSeedRepository CreatePlan");
                var seatNumbers = new List<string>();
                var letters = new List<char> { 'A', 'B', 'C', 'D' }; // Add more letters if needed
                var numberOfSeats = 200; // Change this to the number of seats you want

                for (int i = 0; i < numberOfSeats; i++)
                {
                    var seatLetter = letters[i % letters.Count];
                    var seatNumber = (i / letters.Count) + 1;
                    var seat = $"{seatLetter}{seatNumber}";
                    seatNumbers.Add(seat);
                }
                int seatNumberIndex = 0;

                var seatArrangementFaker = new Faker<SeatArrangementEntity>()
                    .RuleFor(s => s.FlightId, (f, s) =>
                    {
                        return flight.Id;
                    }).RuleFor(s => s.SeatNumber, f =>
                    {
                        // Use seatNumbers in sequence
                        string seatNumber = seatNumbers[seatNumberIndex];
                        seatNumberIndex++;

                        return seatNumber;
                    })
                    .RuleFor(s => s.Status, true);

                var seatArrangements = seatArrangementFaker.Generate(numberOfSeats); // Adjust the number as needed
                await _dbContext.AddRangeAsync(seatArrangements);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("error save InitSeedRepository", e);
            }
        }
        public async Task<List<FlightEntity>> GenerateFlightsAsync(PlanEntity plan, int numberOfFlights)
        {
            try
            {
                var cities = new List<string> { "Paris", "Londres", "New York", "Carrollside", "Josuefurt" };
                var flightFaker = new Faker<FlightEntity>()
                    .StrictMode(true) // Enable strict mode to throw exceptions on rule violations
                    .RuleFor(f => f.Id, f => f.Random.Guid())
                    .RuleFor(f => f.Airline, f => f.Company.CompanyName())
                    .RuleFor(f => f.FlightNumber, f => f.Random.AlphaNumeric(6))
                    .RuleFor(f => f.PlanId, plan.Id)
                    .RuleFor(f => f.Price, f => f.Random.Decimal(100, 1000))
                    .RuleFor(f => f.NumberOfAvailableSeats, 200)
                    .RuleFor(f => f.DepartureCity, f => f.PickRandom(cities))
                    .RuleFor(f => f.ArrivalCity, (f, flight) =>
                    {
                        // Ensure that ArrivalCity is different from DepartureCity
                        string arrivalCity;
                        do
                        {
                            arrivalCity = f.PickRandom(cities);
                        } while (arrivalCity == flight.DepartureCity);
                        return arrivalCity;
                    })
                    .RuleFor(f => f.DepartureDate, (f, flight) =>
                    {
                        // Generate a random departure date
                        var minDate = DateTime.Today.AddDays(10);
                        var maxDate = DateTime.Today.AddDays(30);
                        return f.Date.Between(minDate, maxDate);
                    })
                    .RuleFor(f => f.ArrivalDate, (f, flight) =>
                    {
                        return f.Date.Between(flight.DepartureDate.AddDays(6), flight.DepartureDate.AddDays(10));
                    })
                    .Ignore(f => f.Reservations)
                    .Ignore(f => f.Plan);
            var list = flightFaker.Generate(numberOfFlights);
            await _flightRepository.AddRanger(list);
            await _flightRepository.UnitOfWork.Commit();
             return list;
            } catch (Exception e)
            {
                _logger.LogError("error save InitSeedRepository", e);
                return Enumerable.Empty<FlightEntity>().ToList();
            }
        }
    }
}
