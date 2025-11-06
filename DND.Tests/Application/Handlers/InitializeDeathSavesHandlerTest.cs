using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class InitializeDeathSavesHandlerTest
    {
        private readonly Mock<IDeathSaveManagerService> _deathSaveManagerMock;
        private readonly Mock<ICreatureService> _creatureServiceMock;
        private readonly InitializeDeathSavesHandler _handler;

        public InitializeDeathSavesHandlerTest()
        {
            _deathSaveManagerMock = new Mock<IDeathSaveManagerService>();
            _creatureServiceMock = new Mock<ICreatureService>();
            _handler = new InitializeDeathSavesHandler(_deathSaveManagerMock.Object, _creatureServiceMock.Object);
        }

        [Fact]
        public async Task Handle_WhenDyingCreatureIsPlayer_ShouldInitializeDeathSaves()
        {
            // Arrange
            var creatureId = Guid.NewGuid();
            var domainEvent = new CreatureIsDyingEvent(creatureId);

            _creatureServiceMock
                .Setup(c => c.IsPlayerCharacterAsync(creatureId))
                .ReturnsAsync(true);

            // Act
            await _handler.Handle(domainEvent);

            // Assert
            _creatureServiceMock.Verify(m => m.IsPlayerCharacterAsync(creatureId), Times.Once);
            _deathSaveManagerMock.Verify(m => m.InitializeDeathSavesAsync(creatureId), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenDyingCreatureIsNotPlayer_ShouldNotInitializeDeathSaves()
        {
            // Arrange
            var creatureId = Guid.NewGuid();
            var domainEvent = new CreatureIsDyingEvent(creatureId);

            _creatureServiceMock
                .Setup(c => c.IsPlayerCharacterAsync(creatureId))
                .ReturnsAsync(false);

            // Act
            await _handler.Handle(domainEvent);

            // Assert
            _creatureServiceMock.Verify(m => m.IsPlayerCharacterAsync(creatureId), Times.Once);
            _deathSaveManagerMock.Verify(m => m.InitializeDeathSavesAsync(creatureId), Times.Never);
        }

    }
}
