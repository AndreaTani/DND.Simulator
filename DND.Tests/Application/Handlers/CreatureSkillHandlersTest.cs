using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class CreatureSkillHandlersTest
    {
        public static TheoryData<Skill> AllSkills => [.. Enum.GetValues(typeof(Skill)).Cast<Skill>().ToArray()];

        [Theory]
        [MemberData(nameof(AllSkills))]
        public async Task Handle_CreatureProficientSkillAddedEvent_PersistAndLog(Skill skill)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureProficientSkillAddedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Hero";
            var skills = new List<Skill> { skill };

            var creatureEvent = new CreatureProficientSkillsAddedEvent(creatureId, name, skills);

            // Act
            await handler.HandleAsync(creatureEvent);

            // Assert
            creatureServiceMock.Verify(m =>m.AddProficientSkillsAsync(
                creatureId,
                It.Is<List<Skill>>(s => s.SequenceEqual(skills))
                ), Times.Once());

            loggingServiceMock.Verify(m =>m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(name) &&
                skills.All(skill => s.Contains(skill.ToString())))
            ), Times.Once);
        }

        [Theory]
        [MemberData(nameof(AllSkills))]
        public async Task Handle_CreatureProficientSkillRemovedEvent_PersistAndLog(Skill skill)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureProficientSkillRemovedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Hero";
            var skills = new List<Skill> { skill };

            var creatureEvent = new CreatureProficientSkillsRemovedEvent(creatureId, name, skills);

            // Act
            await handler.HandleAsync(creatureEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveProficientSkillsAsync(
                creatureId,
                It.Is<List<Skill>>(s => s.SequenceEqual(skills))
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(name) &&
                skills.All(skill => s.Contains(skill.ToString())))
            ), Times.Once);
        }

        [Theory]
        [MemberData(nameof(AllSkills))]
        public async Task Handle_CreatureExpertSkillAddedEvent_PersistAndLog(Skill skill)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureExpertSkillAddedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Hero";
            var skills = new List<Skill> { skill };

            var creatureEvent = new CreatureExpertSkillsAddedEvent(creatureId, name, skills);

            // Act
            await handler.HandleAsync(creatureEvent);

            // Assert
            creatureServiceMock.Verify(m => m.AddExpertSkillsAsync(
                creatureId,
                It.Is<List<Skill>>(s => s.SequenceEqual(skills))
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(name) &&
                skills.All(skill => s.Contains(skill.ToString())))
            ), Times.Once);
        }

        [Theory]
        [MemberData(nameof(AllSkills))]
        public async Task Handle_CreatureExpertSkillRemovedEvent_PersistAndLog(Skill skill)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureExpertSkillRemovedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Hero";
            var skills = new List<Skill> { skill };

            var creatureEvent = new CreatureExpertSkillsRemovedEvent(creatureId, name, skills);

            // Act
            await handler.HandleAsync(creatureEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveExpertSkillsAsync(
                creatureId,
                It.Is<List<Skill>>(s => s.SequenceEqual(skills))
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(name) &&
                skills.All(skill => s.Contains(skill.ToString())))
            ), Times.Once);
        }
    }
}
