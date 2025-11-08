using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class RecordDeathSaveRollHandlerTest
    {
        [Fact]
        public async Task Handle_CreatureDeathSaveRolledEvent_ShouldCallManagerToRecordRoll()
        {
            // Arrange
            var deathSaveManagerServiceMock = new Mock<IDeathSaveManagerService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new RecordDeathSaveRollHandler(deathSaveManagerServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            var creatureName = "Brave Adventurer";
            var rollValue = 15; // A success

            var domainEvent = new CreatureDeathSaveRolledEvent(
                creatureId,
                creatureName,
                rollValue
            );

            // Act
            await handler.Handle(domainEvent);

            // Assert
            deathSaveManagerServiceMock.Verify(m => m.RecordDeathSaveRollAsync(
                creatureId,
                rollValue
            ), Times.Once);

            loggingServiceMock.Verify(m => m.Log(
                It.Is<string>(s =>
                s.Contains(domainEvent.CreatureId.ToString()) &&
                s.Contains(domainEvent.CreatureName) &&
                s.Contains(domainEvent.RollValue.ToString()))
            ), Times.Once);
        }
    }
}
