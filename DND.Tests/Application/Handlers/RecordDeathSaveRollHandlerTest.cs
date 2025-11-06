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
            var rollValue = 15; // A success

            var domainEvent = new CreatureDeathSaveRolledEvent(
                creatureId,
                rollValue
            );

            var logMessage = $"Recorded death save roll of {rollValue} for creature with ID: {creatureId}";

            // Act
            await handler.Handle(domainEvent);

            // Assert
            deathSaveManagerServiceMock.Verify(m => m.RecordDeathSaveRollAsync(
                creatureId,
                rollValue
            ), Times.Once);

            loggingServiceMock.Verify(m => m.Log(logMessage), Times.Once);
        }
    }
}
