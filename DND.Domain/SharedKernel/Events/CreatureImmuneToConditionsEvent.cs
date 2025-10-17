using DND.Domain.SharedKernel.Events;

namespace DND.Domain.SharedKernel
{
    public sealed class CreatureImmuneToConditionsEvent : IDomainEvent
    {
        public Guid CreatureId { get; }
        public List<Condition> Conditions { get; }

        public CreatureImmuneToConditionsEvent(Guid creatureId, List<Condition> conditions)
        {
            CreatureId = creatureId;
            Conditions = conditions;
        }
    }
}
