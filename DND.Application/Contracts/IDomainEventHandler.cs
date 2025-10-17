using DND.Domain.SharedKernel.Events;

namespace DND.Application.Contracts
{
    // The domain handler knows how to react to a specific type of event (TEvent)
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        // Method to handle the domain event
        Task Handle(TEvent domainEvent);
    }
}
