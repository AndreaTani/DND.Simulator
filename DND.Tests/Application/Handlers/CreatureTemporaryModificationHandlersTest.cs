using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class CreatureTemporaryModificationHandlersTest
    {
        public static TheoryData<DamageType> AllDamageTypes => [.. Enum.GetValues(typeof(DamageType)).Cast<DamageType>().ToArray()];

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public async Task Handle_CreatureTemporaryDamageModificationAppliedEvent_PersistAndLog(DamageType damageType)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureTemporaryDamageModificationAppliedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            var sourceId = Guid.NewGuid();
            var modification = new TemporaryDamageModification(damageType, 0.5f, sourceId, creatureId, "Hero", 47, ExpirationType.AtTheBeginning, ExpirationTrigger.Source);

            var domainEvent = new CreatureTemporaryDamageModificationAppliedEvent(creatureId, sourceId, modification);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.AddTemporaryDamageModificationAsync(
                creatureId,
                sourceId,
                It.Is<TemporaryDamageModification>(s => s.TypeOfDamage == damageType)
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(modification.CreatureName) &&
                s.Contains(modification.Modifier.ToString()) &&
                s.Contains(creatureId.ToString()) &&
                s.Contains(sourceId.ToString()) &&
                s.Contains(damageType.ToString()))
            ), Times.Once);
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public async Task Handle_CreatureTemporaryDamageModificationRemovedEvent_PersistAndLog(DamageType damageType)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureTemporaryDamageModificationRemovedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Hero";

            var domainEvent = new CreatureTemporaryDamageModificationRemovedEvent(creatureId, name, damageType);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveTemporaryDamageModificationAsync(
                creatureId,
                name,
                damageType
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(name) &&
                s.Contains(creatureId.ToString()) &&
                s.Contains(damageType.ToString()))
            ), Times.Once);

        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public async Task Handle_CreatureTemporaryDamageImmunityAppliedEvent_PersistAndLog(DamageType damageType)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureTemporaryDamageImmunityAppliedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            var sourceId = Guid.NewGuid();
            var modification = new TemporaryImmunityModification(damageType, sourceId, creatureId, "Hero", 47, ExpirationType.AtTheBeginning, ExpirationTrigger.Source);

            var domainEvent = new CreatureTemporaryDamageImmunityAppliedEvent(creatureId, sourceId, modification);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.AddTempopraryDamageImmunityAsync(
                creatureId,
                sourceId,
                It.Is<TemporaryImmunityModification>(s => s.TypeOfDamage == damageType)
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(modification.CreatureName) &&
                s.Contains(creatureId.ToString()) &&
                s.Contains(sourceId.ToString()) &&
                s.Contains(damageType.ToString()))
            ), Times.Once);
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public async Task Handle_CreatureTemporaryDamageImmunityRemovedEvent_PersistAndLog(DamageType damageType)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureTemporaryDamageImmunityRemovedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Hero";

            var domainEvent = new CreatureTemporaryDamageImmunityRemovedEvent(creatureId, name, damageType);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveTemporaryDamageImmunityAsync(
                creatureId,
                name,
                damageType
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(name) &&
                s.Contains(creatureId.ToString()) &&
                s.Contains(damageType.ToString()))
            ), Times.Once);

        }


    }
}
