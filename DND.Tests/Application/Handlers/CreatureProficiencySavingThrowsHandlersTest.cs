using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class CreatureProficiencySavingThrowsHandlersTest
    {
        public static TheoryData<Ability> AllAbilities => [.. Enum.GetValues(typeof(Ability)).Cast<Ability>().ToArray()];

        [Theory]
        [MemberData(nameof(AllAbilities))]
        public async Task Handle_CreatureProficiencySavingThrowsAddedEvent_PersistAndLog(Ability ability)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureProficiencySavingThrowsAddedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Test Creature";
            var abilities = new List<Ability>() { ability };

            var creatureEvent = new CreatureProficiencySavingThrowsAddedEvent(creatureId, name, abilities);  

            // Act
            await handler.HandleAsync(creatureEvent);

            // Assert
            creatureServiceMock.Verify(m => m.AddProficiencySavingThrowsAsync(
                creatureId,
                It.Is<List<Ability>>(s => s.SequenceEqual(abilities))
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(name) &&
                abilities.All(ability => s.Contains(ability.ToString())))
            ), Times.Once);
        }

        [Theory]
        [MemberData(nameof(AllAbilities))]
        public async Task Handle_CreatureProficiencySavingThrowsRemovedEvent_PersistAndLog(Ability ability)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureProficiencySavingThrowsRemovedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Test Creature";
            var abilities = new List<Ability>() { ability };

            var creatureEvent = new CreatureProficiencySavingThrowsRemovedEvent(creatureId, name, abilities);

            // Act
            await handler.HandleAsync(creatureEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveProficiencySavingThrowsAsync(
                creatureId,
                It.Is<List<Ability>>(s => s.SequenceEqual(abilities))
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(name) &&
                abilities.All(ability => s.Contains(ability.ToString())))
            ), Times.Once);
        }

    }
}
