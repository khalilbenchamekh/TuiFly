using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using TuiFly.Application.Interfaces;
using TuiFly.Application.Services;
using TuiFly.Application.Services.Seed;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Customer.Handler;
using TuiFly.Domain.Events.Customer;
using TuiFly.Domain.Events.Flight;
using TuiFly.Domain.Events.Reservation;
using TuiFly.Domain.Interfaces;
using TuiFly.Domain.Interfaces.Repository.Flight;
using TuiFly.Domain.Interfaces.Repository.Reservation;
using TuiFly.Domain.Interfaces.Repository.SeatArrangement;
using TuiFly.Domain.Interfaces.Repository.Seed;
using TuiFly.Domain.Interfaces.Service.Flight;
using TuiFly.Domain.Interfaces.Service.Seed;
using TuiFly.Infra.CrossCutting.Bus;
using TuiFly.Infra.Data.Repository;
using TuiFly.Infra.Data.Repository.Seed;

namespace TuiFly.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<IInitSeedService, InitSeedService>();
            services.AddScoped<IFlightService, FlightService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<FlightRegisteredEvent>, FlightEventHandler>();
            services.AddScoped<INotificationHandler<ReservationRegisteredEvent>, ReservationEventHandler>();
            services.AddScoped<INotificationHandler<SeatArrangementRegisteredEvent>, SeatArrangementEventHandler>();
            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<CreateMultipleCustomersCommand, ValidationResult>, CreateMultipleCustomersCommandHandler>();

            // Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ISeatArrangementRepository, SeatArrangementRepository>();
            services.AddScoped<IInitSeedRepository, InitSeedRepository>();
            //services.AddScoped<TuiFlyContext>();

            // Infra - Data EventSourcing
        }
    }
}