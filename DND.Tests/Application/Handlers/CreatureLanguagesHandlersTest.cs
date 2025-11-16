using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public class CreatureLanguagesHandlersTest
    {
        public static TheoryData<Language> AllLanguages => [.. Enum.GetValues(typeof(Language)).Cast<Language>().ToArray()];

        [Theory]
        [MemberData(nameof(AllLanguages))]
        public async Task Handle_CreatureLanguagesAddedEvent_PersistAndLog(Language language)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureLanguagesAddedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Test Creature";
            var languages = new List<Language>() { language };

            var creatureEvent = new CreatureLanguagesAddedEvent(creatureId, name, languages);

            // Act
            await handler.HandleAsync(creatureEvent);

            // Assert
            creatureServiceMock.Verify(m => m.AddLanguagesAsync(
                creatureId,
                It.Is<List<Language>>(s => s.SequenceEqual(languages))
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(name) &&
                languages.All(language => s.Contains(language.ToString())))
            ), Times.Once);
        }

        [Theory]
        [MemberData(nameof(AllLanguages))]
        public async Task Handle_CreatureLanguagesRemovedEvent_PersistAndLog(Language language)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureLanguagesRemovedEventHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var creatureId = Guid.NewGuid();
            string name = "Test Creature";
            var languages = new List<Language>() { language };

            var creatureEvent = new CreatureLanguagesRemovedEvent(creatureId, name, languages);

            // Act
            await handler.HandleAsync(creatureEvent);

            // Assert
            creatureServiceMock.Verify(m => m.RemoveLanguagesAsync(
                creatureId,
                It.Is<List<Language>>(s => s.SequenceEqual(languages))
                ), Times.Once());

            loggingServiceMock.Verify(m => m.LogMessageAsync(
                It.Is<string>(s =>
                s.Contains(creatureId.ToString()) &&
                s.Contains(name) &&
                languages.All(language => s.Contains(language.ToString())))
            ), Times.Once);
        }
    }
}
