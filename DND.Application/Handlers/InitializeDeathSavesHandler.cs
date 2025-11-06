using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class InitializeDeathSavesHandler : IDomainEventHandler<CreatureIsDyingEvent>
    {
        private readonly IDeathSaveManagerService _deathSaveManager;
        private readonly ICreatureService _creatureService;

        public InitializeDeathSavesHandler(IDeathSaveManagerService deathSaveManager, ICreatureService creatureService)
        {
            _deathSaveManager = deathSaveManager;
            _creatureService = creatureService;
        }

        public async Task Handle(CreatureIsDyingEvent domainEvent)
        {
            bool isPlayerCharacter = await _creatureService.IsPlayerCharacterAsync(domainEvent.CreatureId);

            if (isPlayerCharacter)
            {
                await _deathSaveManager.InitializeDeathSavesAsync(domainEvent.CreatureId);
            }
        }
    }
}
