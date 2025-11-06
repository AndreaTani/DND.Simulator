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
            var sut = new Mock<IDeathSaveManagerService>();
            var handler = new RecordDeathSaveRollHandler(sut.Object);

            var creatureId = Guid.NewGuid();
            var rollValue = 15; // A success

            var domainEvent = new CreatureDeathSaveRolledEvent(
                creatureId,
                rollValue
            );

            // Act
            await handler.Handle(domainEvent);

            // Assert
            sut.Verify(m => m.RecordDeathSaveRollAsync(
                creatureId,
                rollValue
            ), Times.Once);
        }
    }
}
