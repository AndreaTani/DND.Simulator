using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureLanguagesAddedEventHandler : IDomainEventHandler<CreatureLanguagesAddedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureLanguagesAddedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureLanguagesAddedEvent domainEvent)
        {
            await _creatureService.AddLanguagesAsync(domainEvent.CreatureId, domainEvent.Languages);

            string logMessage = $"Creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) can now speak the following languages '{string.Join(',', domainEvent.Languages)}'";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
