using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureProficientSkillRemovedEventHandler : IDomainEventHandler<CreatureProficientSkillsRemovedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureProficientSkillRemovedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureProficientSkillsRemovedEvent domainEvent)
        {
            await _creatureService.RemoveProficientSkillsAsync(domainEvent.CreatureId, domainEvent.Skills);

            string logMessage = $"The following skills '{string.Join(',', domainEvent.Skills)}' have been removed from creature {domainEvent.Name} (ID: {domainEvent.CreatureId}) proficiency list";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
