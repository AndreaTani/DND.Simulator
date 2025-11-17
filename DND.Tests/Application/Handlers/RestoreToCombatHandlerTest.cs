using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class RestoreToCombatHandlerTest
    {
        [Fact]
        public async Task Handle_CreatureRevivedEvent_ShouldRestoreCreatureToCombat()
        {
            // Arrange
            var combatServiceMock = new Mock<ICombatSessionService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new RestoreToCombatHandler(combatServiceMock.Object, loggingServiceMock.Object);
            var domainEvent = new CreatureRevivedEvent(
                Guid.NewGuid(),
                "Sample Creature"
            );

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            combatServiceMock.Verify(m => m.RestoreToCombatAsync(domainEvent.CreatureId), Times.Once);
            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s => 
                s.Contains(domainEvent.CreatureId.ToString()) && 
                s.Contains(domainEvent.CreatureName))
            ), Times.Once);
        }
    }
}
