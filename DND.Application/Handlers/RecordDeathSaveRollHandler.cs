using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class RecordDeathSaveRollHandler : IDomainEventHandler<CreatureDeathSaveRolledEvent>
    {
        private readonly IDeathSaveManagerService _deathSaveManagerService;

        public RecordDeathSaveRollHandler(IDeathSaveManagerService deathSaveManagerService)
        {
            _deathSaveManagerService = deathSaveManagerService;
        }

        public async Task Handle(CreatureDeathSaveRolledEvent domainEvent)
        {
            await _deathSaveManagerService.RecordDeathSaveRollAsync(
                domainEvent.CreatureId,
                domainEvent.RollValue
            );
        }
    }
}

