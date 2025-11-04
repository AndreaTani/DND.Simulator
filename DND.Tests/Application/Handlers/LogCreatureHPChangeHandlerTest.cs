using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class LogCreatureHPChangeHandlerTest
    {
        [Fact]
        public async Task Handle_CreatureHPChangedEvent_ShouldLogCorrectMessage()
        {
            // Arrange
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new LogCreatureHPChangeHandler(loggingServiceMock.Object);

            var domainEvent = new CreatureHPChangedEvent(
                CreatureId: Guid.NewGuid(),
                PreviousHp: 50,
                CurrentHp: 30,
                MaxHp: 100,
                Amount: -20,
                Type: DamageType.Slashing
            );

            var expectedMessage = $"Creature {domainEvent.CreatureId} HP changed from {domainEvent.PreviousHp} to {domainEvent.CurrentHp} (Change: {domainEvent.Amount}, Type: {domainEvent.Type})";

            // Act
            await handler.Handle(domainEvent);

            // Assert
            loggingServiceMock.Verify(ls => ls.Log(expectedMessage), Times.Once);
        }

    }
}
