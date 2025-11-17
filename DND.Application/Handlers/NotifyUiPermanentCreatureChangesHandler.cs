using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class NotifyUiPermanentCreatureChangesHandler : IDomainEventHandler<IPermanentAttributeChangedEvent>
    {
        private readonly IUIDispatcher _dispatcher;

        public NotifyUiPermanentCreatureChangesHandler(IUIDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task HandleAsync(IPermanentAttributeChangedEvent domainEvent)
        {
            await _dispatcher.NotifyCharacterSheetRefreshAsync(domainEvent.CreatureId);
        }
    }
}
