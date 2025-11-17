using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class InitializeDeathSavesHandler : IDomainEventHandler<CreatureIsDyingEvent>
    {
        private readonly IDeathSaveManagerService _deathSaveManager;
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public InitializeDeathSavesHandler(IDeathSaveManagerService deathSaveManager, ICreatureService creatureService, ILoggingService loggingService)
        {
            _deathSaveManager = deathSaveManager;
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureIsDyingEvent domainEvent)
        {
            bool isPlayerCharacter = await _creatureService.IsPlayerCharacterAsync(domainEvent.CreatureId);

            if (isPlayerCharacter)
            {
                var logMessage = $"Initialized death saves for player {domainEvent.CreatureName} (character with ID: {domainEvent.CreatureId})";
                await _deathSaveManager.InitializeDeathSavesAsync(domainEvent.CreatureId);
                await _loggingService.LogMessageAsync(logMessage);
            }
        }
    }
}
