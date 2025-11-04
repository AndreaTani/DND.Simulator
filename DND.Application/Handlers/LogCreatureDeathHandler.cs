using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class LogCreatureDeathHandler : IDomainEventHandler<CreatureDiedEvent>
    {
        private readonly ILoggingService _loggingService;

        public LogCreatureDeathHandler(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public async Task Handle(CreatureDiedEvent domainEvent)
        {
            var message = $"Creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) has died.";
            await _loggingService.Log(message);
        }
    }
}
