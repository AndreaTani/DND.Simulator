using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureExpertSkillAddedEventHandler : IDomainEventHandler<CreatureExpertSkillsAddedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureExpertSkillAddedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureExpertSkillsAddedEvent domainEvent)
        {
            await _creatureService.AddExpertSkillsAsync(domainEvent.CreatureId, domainEvent.Skills);

            string logMessage = $"The following skills '{string.Join(',', domainEvent.Skills)}' have been marked with Expertise for the creature {domainEvent.Name} (ID: {domainEvent.CreatureId})";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
