using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureLanguagesRemovedEventHandler : IDomainEventHandler<CreatureLanguagesRemovedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureLanguagesRemovedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureLanguagesRemovedEvent domainEvent)
        {
            await _creatureService.RemoveLanguagesAsync(domainEvent.CreatureId, domainEvent.Languages);

            string logMessage = $"The creature {domainEvent.Name} (ID: {domainEvent.CreatureId}) can't speak the following languages '{string.Join(',', domainEvent.Languages)}' anymore";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
