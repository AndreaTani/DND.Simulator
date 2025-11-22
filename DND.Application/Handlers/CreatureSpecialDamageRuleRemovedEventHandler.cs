using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureSpecialDamageRuleRemovedEventHandler : IDomainEventHandler<CreatureSpecialDamageRuleRemovedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureSpecialDamageRuleRemovedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureSpecialDamageRuleRemovedEvent domainEvent)
        {
            await _creatureService.RemoveSpecialDamageRuleAsync(domainEvent.CreatureId, domainEvent.DamageType);

            string logMessage = $"The creature {domainEvent.CreatureName} (ID:{domainEvent.CreatureId}) has lost a Special rule '{domainEvent.RuleName}' pertaining this damage type: '{domainEvent.DamageType}'";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
