using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureConditionImmunitiesRemovedEventHandler : IDomainEventHandler<CreatureConditionImmunitiesRemovedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureConditionImmunitiesRemovedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureConditionImmunitiesRemovedEvent domainEvent)
        {
            await _creatureService.RemoveConditionImmunitiesAsync(domainEvent.CreatureId, domainEvent.Conditions);

            string logMessage = $"The creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) has lot the dfollowing condition immunities '{string.Join(',', domainEvent.Conditions)}'";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
