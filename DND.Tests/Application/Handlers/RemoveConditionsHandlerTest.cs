using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class RemoveConditionsHandlerTest
    {
        public static TheoryData<Condition> AllConditions => [.. Enum.GetValues(typeof(Condition)).Cast<Condition>().ToArray()];

        [Theory]
        [MemberData(nameof(AllConditions))]
        public async Task Handle_CreatureAddConditionsEvent_ShoudCallServiceToRemoveConditions(Condition condition)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new RemoveConditionsHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            Guid creatureId = Guid.NewGuid();
            string creatureName = "Phlumph";
            var conditions = new List<Condition> { condition };

            var domainEvent = new CreatureRemoveConditionsEvent(creatureId, creatureName, conditions);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveConditionsAsync(
                creatureId,
                It.Is<List<Condition>>(c => c.SequenceEqual(conditions))
            ), Times.Once);

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(domainEvent.CreatureId.ToString()) &&
                conditions.All(condition => s.Contains(condition.ToString())))
            ), Times.Once);
        }


    }
}
