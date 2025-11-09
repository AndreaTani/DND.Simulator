using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class RemoveConditionsHandler : IDomainEventHandler<CreatureRemoveConditionsEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public RemoveConditionsHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task Handle(CreatureRemoveConditionsEvent domainEvent)
        {
            await _creatureService.RemoveConditionsAsync(domainEvent.CreatureId, domainEvent.Conditions);

            string logMessage = $"The following conditions: '{string.Join(", ", domainEvent.Conditions)}' have been removed from creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) ";
            await _loggingService.Log(logMessage);

        }
    }
}
