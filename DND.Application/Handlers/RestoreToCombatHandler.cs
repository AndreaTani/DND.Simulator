using DND.Application.Contracts;
using DND.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Application.Handlers
{
    public class RestoreToCombatHandler : IDomainEventHandler<CreatureRevivedEvent>
    {
        private readonly ICombatSessionService _combatSessionService;
        private readonly ILoggingService _loggingService;
        public RestoreToCombatHandler(
            ICombatSessionService combatSessionService,
            ILoggingService loggingService
            )
        {
            _combatSessionService = combatSessionService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureRevivedEvent domainEvent)
        {
            string logMessage = $"Creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) has been restored to combat.";
            await _combatSessionService.RestoreToCombatAsync(domainEvent.CreatureId);
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
