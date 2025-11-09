using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class ProcessNewConditionsHandler : IDomainEventHandler<CreatureAddConditionsEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public ProcessNewConditionsHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task Handle(CreatureAddConditionsEvent domainEvent)
        {
            await _creatureService.ApplyConditionsAsync(domainEvent.CreatureId, domainEvent.Conditions);

            string logMessage = $"The following conditions: '{string.Join(", ", domainEvent.Conditions)}' have been applied to creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) ";
            await _loggingService.Log(logMessage);
        }
    }
}
