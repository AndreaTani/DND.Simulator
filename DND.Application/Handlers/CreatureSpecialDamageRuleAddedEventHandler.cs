using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureSpecialDamageRuleAddedEventHandler : IDomainEventHandler<CreatureSpecialDamageRuleAddedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureSpecialDamageRuleAddedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureSpecialDamageRuleAddedEvent domainEvent)
        {
            await _creatureService.AddSpecialDamageRuleAsync(domainEvent.CreatureId, domainEvent.damageType);

            string logMessage = $"The creature {domainEvent.Name} (ID:{domainEvent.CreatureId}) has gained a new Special rule '{domainEvent.RuleName}' pertaining this damage type: '{domainEvent.damageType}'";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
