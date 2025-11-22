using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureProficiencySavingThrowsRemovedEventHandler : IDomainEventHandler<CreatureProficiencySavingThrowsRemovedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureProficiencySavingThrowsRemovedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureProficiencySavingThrowsRemovedEvent domainEvent)
        {
            await _creatureService.RemoveProficiencySavingThrowsAsync(domainEvent.CreatureId, domainEvent.Abilities);

            string logMessage = $"The creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) following abilities '{string.Join(',', domainEvent.Abilities)}' are not proficient asnymore for saving throws";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
