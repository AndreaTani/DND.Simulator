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

            // Act
            await handler.HandleAsync(domainEvent);
            
            // Assert
            combatServiceMock.Verify(m => m.RemoveFromInitiativeAsync(
                domainEvent.CreatureId
            ), Times.Once);

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s => 
                s.Contains(domainEvent.CreatureId.ToString()) && 
                s.Contains(domainEvent.CreatureName))
            ), Times.Once);
        }
    }
}
