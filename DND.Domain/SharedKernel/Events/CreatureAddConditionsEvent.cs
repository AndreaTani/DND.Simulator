namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when adding conditions to a creature
    /// </summary>
    /// <param name="CreatureId">Id of the creature</param>
    /// <param name="Conditions">Conditions added</param>
    public record CreatureAddConditionsEvent(
        Guid CreatureId,
        IEnumerable<Condition> Conditions
        ) : IDomainEvent;
}
