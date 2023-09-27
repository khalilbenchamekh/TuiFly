using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using NetDevPack.Data;
using NetDevPack.Mediator;
using TuiFly.Domain.Commands.Customer.Commands;
using TuiFly.Domain.Commands.Flight.Commands;
using TuiFly.Domain.Commands.Flight.Handler;
using TuiFly.Domain.Commands.Flight.Validation;
using TuiFly.Domain.Interfaces;
using TuiFly.Domain.Interfaces.Repository.Flight;
using TuiFly.Domain.Interfaces.Repository.Reservation;
using TuiFly.Domain.Interfaces.Repository.SeatArrangement;
using TuiFly.Domain.Models.Entities;
using TuiFly.Infra.Data.Context;
using TuiFly.Infra.Data.Repository;
using Xunit;

namespace TuiFLy.Domain.UnitTest.Commands.Flight.Handler
{
    public class MakeReservationCommandHandlerTests
        {

            private readonly TuiFlyContext _context;
            private readonly IReservationRepository _repository;
            private readonly Mock<IReservationRepository> _repositoryMock;

            public MakeReservationCommandHandlerTests()
            {
                _repositoryMock = new Mock<IReservationRepository>();
                var options = new DbContextOptionsBuilder<TuiFlyContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;
                // Create a mock of IMediatorHandler using Moq
                var mediatorHandlerMock = new Mock<IMediatorHandler>();
                _context = new TuiFlyContext(options, mediatorHandlerMock.Object);
                // Create a mock for IUnitOfWork
                var unitOfWorkMock = new Mock<IUnitOfWork>();
                _repositoryMock.Setup(x => x.UnitOfWork).Returns(unitOfWorkMock.Object);
                _repository = new ReservationRepository(_context);
            }

            [Fact]
            public async Task Handle_NotEnoughAvailableSeats_ShouldReturnError()
            {
                // Arrange: Mock the repositories to return insufficient available seats.
                var handler = CreateHandlerWithMocks(insufficientSeats: true);

                var command = CreateValidMakeReservationCommand();

                // Act: Execute the handler.
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert: Verify that an appropriate error message is returned.
                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, error => error.PropertyName == "MakeReservationCommand" && error.ErrorMessage == "Not enough available seats for the family.");
            }
            public MakeReservationCommandHandler CreateHandlerWithMocks(bool insufficientSeats = false, bool flightNotFound = false, bool validationFailures = false)
            {
            // Mock the dependencies
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mediatorMock = new Mock<IMediator>();
                var flightRepositoryMock = new Mock<IFlightRepository>();
                flightRepositoryMock.Setup(x => x.UnitOfWork).Returns(unitOfWorkMock.Object);
                var reservationRepositoryMock = new Mock<IReservationRepository>();
            reservationRepositoryMock.Setup(x => x.UnitOfWork).Returns(unitOfWorkMock.Object);

            var seatArrangementRepositoryMock = new Mock<ISeatArrangementRepository>();
            seatArrangementRepositoryMock.Setup(x => x.UnitOfWork).Returns(unitOfWorkMock.Object);

            var mediatorHandlerMock = new Mock<IMediatorHandler>();
                if (insufficientSeats)
                {
                    seatArrangementRepositoryMock.Setup(repo => repo.GetAvailableSeats(It.IsAny<Guid>(), It.IsAny<int>()))
                        .ReturnsAsync(new List<string>()); // Return an empty list to simulate not enough seats.
                }
                else
                {
                    seatArrangementRepositoryMock.Setup(repo => repo.GetAvailableSeats(It.IsAny<Guid>(), It.IsAny<int>()))
                        .ReturnsAsync(new List<string> { "1A", "1B", "2A", "2B" }); // Return available seats.
                }

                if (flightNotFound)
                {
                    flightRepositoryMock.Setup(repo => repo.findById(It.IsAny<Guid>()))
                        .ReturnsAsync((FlightEntity)null); // Return null to simulate flight not found.
                }
                else
                {
                    flightRepositoryMock.Setup(repo => repo.findById(It.IsAny<Guid>()))
                        .ReturnsAsync(new FlightEntity()); // Return a flight entity.
                }

                if (validationFailures)
                {
                    mediatorMock.Setup(mediator => mediator.Send(It.IsAny<CreateMultipleCustomersCommand>(), default))
                        .ReturnsAsync(new ValidationResult(new List<ValidationFailure>
                        {
                new ValidationFailure("FirstName", "Invalid first name."),
                new ValidationFailure("LastName", "Invalid last name."),
                            // Add more validation failures as needed.
                        }));
                }
                else
                {
                    mediatorMock.Setup(mediator => mediator.Send(It.IsAny<CreateMultipleCustomersCommand>(), default))
                        .ReturnsAsync(new ValidationResult()); // Return a valid result.
                }

                // Create the handler with the mocked dependencies
                return new MakeReservationCommandHandler(seatArrangementRepositoryMock.Object, _repository, flightRepositoryMock.Object, mediatorMock.Object);
            }
            public MakeReservationCommandValidation CreateValidMakeReservationCommand()
            {
                return new MakeReservationCommandValidation
                {
                    FlightId = Guid.NewGuid(), // Replace with a valid FlightId
                    Passengers = new List<RegisterNewCustomerCommand>
                    {
                        new RegisterNewCustomerCommand("John", "Doe", "johndoe@example.com"),
                        new RegisterNewCustomerCommand("Jane", "Smith", "janesmith@example.com"),
                        new RegisterNewCustomerCommand("John", "Doe", "johndoe@example.com"),
                        new RegisterNewCustomerCommand("Jane", "Smith", "janesmith@example.com")
                    }
                };
            }

    }
}
