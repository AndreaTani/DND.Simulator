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

            var creatureId = Guid.NewGuid();
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

            var creatureId = Guid.NewGuid();
            string name = "Hero";
            var damageTypes = new List<DamageType>() { damageType };
            var removalReason = RemovalReason.EffectExpired;

            var domainEvent = new CreatureDamageImmunitiesRemovedEvent(creatureId, name, damageTypes, removalReason);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveDamageImmunitiesAsync(
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

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public async Task Handle_CreatureDamageResistancesAddedEvent_PersistAndLog (DamageType damageType)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureDamageResistancesAddedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Hero";
            var damageTypes = new List<DamageType> { damageType };

            var domainEvent = new CreatureDamageResistancesAddedEvent(creatureId, name, damageTypes);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.AddDamageResistancesAsync(
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
        public async Task Handle_CreatureDamageResistancesRemovedEvent_PeristAndLog(DamageType damageType)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureDamageResistancesRemovedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Hero";
            var damageTypes = new List<DamageType>() { damageType };
            var removalReason = RemovalReason.EffectExpired;

            var domainEvent = new CreatureDamageResistancesRemovedEvent(creatureId, name, damageTypes, removalReason);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveDamageResistancesAsync(
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

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public async Task Handle_CreatureDamageVulnerabilitiesAddedEvent_PeristAndLog(DamageType damageType)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureDamageVulnerabilitiesAddedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Hero";
            var damageTypes = new List<DamageType>() { damageType };

            var domainEvent = new CreatureDamageVulnerabilitiesAddedEvent(creatureId, name, damageTypes);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.AddDamageVulnerabilitiesAsync(
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
        public async Task Handle_CreatureDamageVulnerabilitiesRemovedEvent_PersistAndLog(DamageType damageType)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureDamageVulnerabilitiesRemovedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Hero";
            var damageTypes = new List<DamageType>() { damageType };
            var removalReason = RemovalReason.EffectExpired;

            var domainEvent = new CreatureDamageVulnerabilitiesRemovedEvent(creatureId, name, damageTypes, removalReason);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveDamageVulnerabilitiesAsync(
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
