using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class CreatureSensesHandlersTest
    {
        public static TheoryData<Sense> AllSenses => [.. Enum.GetValues(typeof(Sense)).Cast<Sense>().ToArray()];

        [Theory]
        [MemberData(nameof(AllSenses))]
        public async Task Handle_CreatureSenseAddedEvent_PersistAndLog(Sense sense)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureSenseAddedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Test Creature";
            var senses = new List<Sense>() { sense };

            var creatureEvent = new CreatureSensesAddedEvent(creatureId, name, senses);

            // Act
            await handler.HandleAsync(creatureEvent);

            // Assert
            creatureServiceMock.Verify(m => m.AddSensesAsync(
                creatureId,
                It.Is<List<Sense>>(s => s.SequenceEqual(senses))
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(name) &&
                senses.All(sense => s.Contains(sense.ToString())))
            ), Times.Once);
        }

        [Theory]
        [MemberData(nameof(AllSenses))]
        public async Task Handle_CreatureSenseRemovedEvent_PersistAndLog(Sense sense)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureSenseRemovedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Test Creature";
            var senses = new List<Sense>() { sense };

            var creatureEvent = new CreatureSensesRemovedEvent(creatureId, name, senses);

            // Act
            await handler.HandleAsync(creatureEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveSensesAsync(
                creatureId,
                It.Is<List<Sense>>(s => s.SequenceEqual(senses))
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(name) &&
                senses.All(sense => s.Contains(sense.ToString())))
            ), Times.Once);
        }
    }
}
