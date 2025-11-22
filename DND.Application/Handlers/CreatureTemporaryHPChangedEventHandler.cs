using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureTemporaryHPChangedEventHandler : IDomainEventHandler<CreatureTemporaryHPChangedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureTemporaryHPChangedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
           _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureTemporaryHPChangedEvent domainEvent)
        {
            await _creatureService.ChangeCreatureTempHpAsync(domainEvent.CreatureId, domainEvent.CreatureName, domainEvent.CurrentTemporaryHp, domainEvent.Amount);

            string logMessage = $"The creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) received {domainEvent.Amount} temporary Hit Points, bringing the total number of Hit Points to '{Math.Max(domainEvent.Amount, domainEvent.CurrentTemporaryHp)}' from '{domainEvent.CurrentTemporaryHp}'.";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
