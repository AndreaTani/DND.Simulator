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
            await _creatureService.RemoveSpecialDamageRuleAsync(domainEvent.CreatureId, domainEvent.damageType);

            string logMessage = $"The creature {domainEvent.Name} (ID:{domainEvent.CreatureId}) has lost a Special rule '{domainEvent.RuleName}' pertaining this damage type: '{domainEvent.damageType}'";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
