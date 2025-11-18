using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class DamageRuleChangeEventsTest
    {
        public static TheoryData<DamageType> AllDamageTypes => [.. Enum.GetValues(typeof(DamageType)).Cast<DamageType>().ToArray()];

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public async Task Handle_CreatureDamageImunitiesAddedEvent_PersistAdLog(DamageType damageType)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureDamageImmunitiesAddedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = new Guid();
            string name = "Hero";
            var damageTypes = new List<DamageType>() { damageType };

            var domainEvent = new CreatureDamageImmunitiesAddedEvent(creatureId, name, damageTypes);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.AddDamageImmunityAsync(
                creatureId,
                It.Is<List<DamageType>>(s => s.SequenceEqual(damageTypes))
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(name) &&
                damageTypes.All(damageType => s.Contains(damageType.ToString())))
            ), Times.Once);
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public async Task Handle_CreatureDamageImunitiesRemovedEvent_PersistAdLog(DamageType damageType)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureDamageImmunitiesRemovedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = new Guid();
            string name = "Hero";
            var damageTypes = new List<DamageType>() { damageType };
            var removalReason = RemovalReason.EffectExpired;

            var domainEvent = new CreatureDamageImmunitiesRemovedEvent(creatureId, name, damageTypes, removalReason);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveDamageImmunityAsync(
                creatureId,
                It.Is<List<DamageType>>(s => s.SequenceEqual(damageTypes))
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(name) &&
                s.Contains(removalReason.ToString()) &&
                damageTypes.All(damageType => s.Contains(damageType.ToString())))
            ), Times.Once);
        }


    }
}
