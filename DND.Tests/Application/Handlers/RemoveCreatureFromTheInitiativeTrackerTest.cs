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
            var handler = new RemoveCreatureFromTheInitiativeTracker(combatServiceMock.Object);

            var domainEvent = new CreatureDiedEvent(
                Guid.NewGuid()
            );

            // Act
            await handler.Handle(domainEvent);
            
            // Assert
            combatServiceMock.Verify(m => m.RemoveFromInitiativeAsync(
                domainEvent.CreatureId
            ), Times.Once);
        }
    }
}
