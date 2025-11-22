using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class CreatureTemporaryHPChangedEventHandlerTest
    {
        [Fact]
        public async Task Handle_CreatureTemporaryHPChangedEvent_PersistAndLog()
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureTemporaryHPChangedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string creatureName = "Hero";
            int currentTemporaryHP = 2;
            int amount = 5;

            var domainEvent = new CreatureTemporaryHPChangedEvent(creatureId, creatureName, currentTemporaryHP, amount);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.ChangeCreatureTempHpAsync(
                creatureId,
                creatureName,
                currentTemporaryHP,
                amount
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(creatureName) &&
                s.Contains(currentTemporaryHP.ToString()) &&
                s.Contains(amount.ToString())
                )
            ), Times.Once);

        }
    }
}
