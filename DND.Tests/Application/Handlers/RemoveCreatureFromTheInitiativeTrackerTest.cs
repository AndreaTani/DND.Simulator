using DND.Domain.SharedKernel;
using DND.Application.Contracts;
using DND.Application.Handlers;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class RemoveCreatureFromTheInitiativeTrackerTest
    {
        [Fact]
        public async Task Handle_CreatureDiedEvent_ShouldRemoveCreatureFromInitiativeTracker()
        {
            // Arrange
            var combatServiceMock = new Mock<ICombatSessionService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new RemoveCreatureFromTheInitiativeTrackerHandler(combatServiceMock.Object, loggingServiceMock.Object);

            var domainEvent = new CreatureDiedEvent(
                Guid.NewGuid(),
                "Sample Creature"
            );

            string expectedMessage = $"Creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) has been removed from the initiative tracker.";

            // Act
            await handler.Handle(domainEvent);
            
            // Assert
            combatServiceMock.Verify(m => m.RemoveFromInitiativeAsync(
                domainEvent.CreatureId
            ), Times.Once);

            loggingServiceMock.Verify(m => m.Log(expectedMessage), Times.Once);
        }
    }
}
