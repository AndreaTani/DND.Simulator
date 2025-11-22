using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureProficientSkillAddedEventHandler : IDomainEventHandler<CreatureProficientSkillsAddedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureProficientSkillAddedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureProficientSkillsAddedEvent domainEvent)
        {
            await _creatureService.AddProficientSkillsAsync(domainEvent.CreatureId, domainEvent.Skills);

            string logMessage = $"The following skills '{string.Join(',', domainEvent.Skills)}' have been marked as proficient for creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId})";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
