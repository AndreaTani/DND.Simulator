using DND.Application.Contracts;
using DND.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Application.Handlers
{
    public class CreatureDamageVulnerabilitiesRemovedEventHandler : IDomainEventHandler<CreatureDamageVulnerabilitiesRemovedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureDamageVulnerabilitiesRemovedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureDamageVulnerabilitiesRemovedEvent domainEvent)
        {
            await _creatureService.RemoveDamageVulnerabilitiesAsync(domainEvent.CreatureId, domainEvent.Type);

            string logMessage = $"The following vulnerabilities '{string.Join(',', domainEvent.Type)}' are no longer affecting Creature {domainEvent.Name} (ID:{domainEvent.CreatureId}) for the following reason: {domainEvent.Reason}";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
