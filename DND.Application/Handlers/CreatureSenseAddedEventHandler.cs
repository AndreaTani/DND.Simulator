using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureSenseAddedEventHandler : IDomainEventHandler<CreatureSensesAddedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureSenseAddedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureSensesAddedEvent domainEvent)
        {
            await _creatureService.AddSensesAsync(domainEvent.CreatureId, domainEvent.Senses);

            string logMessage = $"The following senses '{string.Join(',', domainEvent.Senses)}' have been added to Creature {domainEvent.Name} (ID: {domainEvent.CreatureId})";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
