using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    public record CreaturePermanentAttributeChangedEvent(Guid CreatureId) : IPermanentAttributeChangedEvent;
    public class NotifyUiPermanentCreatureChangesTest
    {
        [Fact]
        public async Task Handle_NotifyUiPermanentCreatureChanges_UpdateUiCharacterSheet()
        {
            // Arrange
            var uiDispatcherMock = new Mock<IUIDispatcher>();
            var handler = new NotifyUiPermanentCreatureChangesHandler(uiDispatcherMock.Object);

            var creatureId = Guid.NewGuid();

            var domainEvent = new CreaturePermanentAttributeChangedEvent(creatureId);

            // Act
            await handler.HandleAsync(domainEvent);

            // Assert
            uiDispatcherMock.Verify(m => m.NotifyCharacterSheetRefreshAsync(
                It.Is<Guid>(id => id == creatureId)
                ), Times.Once());
        }
    }
}
