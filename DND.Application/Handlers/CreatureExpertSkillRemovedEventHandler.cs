using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureExpertSkillRemovedEventHandler : IDomainEventHandler<CreatureExpertSkillsRemovedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureExpertSkillRemovedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureExpertSkillsRemovedEvent domainEvent)
        {
            await _creatureService.RemoveExpertSkillsAsync(domainEvent.CreatureId, domainEvent.Skills);

            string logMessage = $"The following skills '{string.Join(',', domainEvent.Skills)}' have been removed from creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) expertise list";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
