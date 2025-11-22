using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureSenseRemovedEventHandler : IDomainEventHandler<CreatureSensesRemovedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureSenseRemovedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureSensesRemovedEvent domainEvent)
        {
            await _creatureService.RemoveSensesAsync(domainEvent.CreatureId, domainEvent.Senses);

            string logMessage = $"The following senses '{string.Join(',', domainEvent.Senses)}' have been removed from creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId})";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
