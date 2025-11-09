using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class ProcessNewConditionsHandlerTest
    {
        public static TheoryData<Condition> AllConditions => [.. Enum.GetValues(typeof(Condition)).Cast<Condition>().ToArray()];

        [Theory]
        [MemberData(nameof(AllConditions))]
        public async Task Handle_CreatureAddConditionsEvent_ShoudCallServiceToProcessConditions(Condition condition)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new ProcessNewConditionsHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            Guid creatureId = Guid.NewGuid();
            string creatureName = "Phlumph";
            var conditions = new List<Condition> { condition };

            var domainEvent = new CreatureAddConditionsEvent (creatureId, creatureName, conditions);

            // Act
            await handler.Handle(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.ApplyConditionsAsync(
                creatureId,
                It.Is<List<Condition>>(c => c.SequenceEqual(conditions))
            ), Times.Once);

            loggingServiceMock.Verify(m => m.Log(
                It.Is<string>(s =>
                s.Contains(domainEvent.CreatureId.ToString()) &&
                conditions.All(condition => s.Contains(condition.ToString())))
            ), Times.Once);
        }   
    }
}
