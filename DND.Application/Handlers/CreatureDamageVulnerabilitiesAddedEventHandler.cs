using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureDamageVulnerabilitiesAddedEventHandler : IDomainEventHandler<CreatureDamageVulnerabilitiesAddedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureDamageVulnerabilitiesAddedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureDamageVulnerabilitiesAddedEvent domainEvent)
        {
            await _creatureService.AddDamageVulnerabilitiesAsync(domainEvent.CreatureId, domainEvent.DamageTypes);

            string logMessage = $"The creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) now suffers from the following vulnerabilities: '{string.Join(',', domainEvent.DamageTypes)}'";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
