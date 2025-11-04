using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class LogCreatureHPChangeHandler : IDomainEventHandler<CreatureHPChangedEvent>
    {
        private readonly ILoggingService _logger;
         
        public LogCreatureHPChangeHandler(ILoggingService logger)
        {
            _logger = logger;
        }

        public Task Handle(CreatureHPChangedEvent domainEvent)
        {
            var message = $"Creature {domainEvent.CreatureId} HP changed from {domainEvent.PreviousHp} to {domainEvent.CurrentHp} (Change: {domainEvent.Amount}, Type: {domainEvent.Type})";
            _logger.Log(message);
            return Task.CompletedTask;
        }
    }
}
