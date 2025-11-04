using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class LogCreatureDeathHandlerTest
    {
        [Fact]
        public async Task Handle_CreatureDeathEvent_ShouldLogCorrectMessage()
        {
            // Arrange
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new LogCreatureDeathHandler(loggingServiceMock.Object);

            var domainEvent = new CreatureDiedEvent(
                Guid.NewGuid(),
                "Sample Creature"
            );

            // Act
            await handler.Handle(domainEvent);
            var expectedMessage = $"Creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) has died.";

            // Assert
            loggingServiceMock.Verify(ls => ls.Log(expectedMessage), Times.Once);

        }
    }
}
