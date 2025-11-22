using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class CreatureConditionImmunitiesEventHandlersTest
    {
        public static TheoryData<Condition> AllConditions => [.. Enum.GetValues(typeof(Condition)).Cast<Condition>().ToArray()];

        [Theory]
        [MemberData(nameof(AllConditions))]
        public async Task Handle_CreatureConditionImmunitiesRemovedEvent_PersistAndLog(Condition condition)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureConditionImmunitiesRemovedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            var creatureName = "Hero";
            var conditions = new List<Condition>() { condition };

            var domainEvent = new CreatureConditionImmunitiesRemovedEvent(creatureId, creatureName, conditions);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveConditionImmunitiesAsync(
                creatureId,
                It.Is<List<Condition>>(c => c.SequenceEqual(conditions))
            ), Times.Once);

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(domainEvent.CreatureId.ToString()) &&
                s.Contains(domainEvent.CreatureName) &&
                conditions.All(condition => s.Contains(condition.ToString())))
            ), Times.Once);

        }
    }
}
