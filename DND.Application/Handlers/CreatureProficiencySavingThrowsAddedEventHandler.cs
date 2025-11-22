using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureProficiencySavingThrowsAddedEventHandler : IDomainEventHandler<CreatureProficiencySavingThrowsAddedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureProficiencySavingThrowsAddedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureProficiencySavingThrowsAddedEvent domainEvent)
        {
            await _creatureService.AddProficiencySavingThrowsAsync(domainEvent.CreatureId, domainEvent.Abilities);

            string logMessages = $"The creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) following abilities '{string.Join(',', domainEvent.Abilities)}' are now proficient for saving throws";
            await _loggingService.LogMessageAsync(logMessages);
        }
    }
}
