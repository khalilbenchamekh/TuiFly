using Microsoft.EntityFrameworkCore;
using Moq;
using NetDevPack.Data;
using NetDevPack.Mediator;
using TuiFly.Domain.Interfaces.Repository.Reservation;
using TuiFly.Domain.Models.Entities;
using TuiFly.Infra.Data.Context;
using TuiFly.Infra.Data.Repository;
using Xunit;

namespace TuiFly.Infra.Data.UnitTest.Repository
{
    public class ReservationRepositoryTests
    {
        private readonly TuiFlyContext _context;
        private readonly IReservationRepository _repository;
        private readonly Mock<IReservationRepository> _repositoryMock;

        public ReservationRepositoryTests()
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

            // Configure the _repository mock to return the unitOfWorkMock
            _repositoryMock.Setup(x => x.UnitOfWork).Returns(unitOfWorkMock.Object);

            _repository = new ReservationRepository(_context);
        }

        [Fact]
        public async Task AddReservationEntity_AddsReservations()
        {
            // Arrange
            var reservationsToAdd = new List<ReservationEntity>
        {
            new ReservationEntity { Id = Guid.NewGuid(), /* Set other properties */ },
            new ReservationEntity { Id = Guid.NewGuid(), /* Set other properties */ },
            // Add more reservation entities as needed
        };

            // Act
            await _repository.AddReservationEntity(reservationsToAdd);
            await _repository.UnitOfWork.Commit();

            // Assert
            var addedReservations = await _context.Reservations.ToListAsync();
            Assert.Equal(reservationsToAdd.Count, addedReservations.Count);
            // You can further validate if the added reservations match the expected ones.
        }
    }
}
