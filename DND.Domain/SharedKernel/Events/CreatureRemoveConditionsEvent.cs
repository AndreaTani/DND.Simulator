namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when removing conditions to a creature
    /// </summary>
    /// <param name="CreatureId">Id of the creature</param>
    /// <param name="CreatureName">Name of the creature</param>
    /// <param name="Conditions">Conditions removed</param>
    public record CreatureRemoveConditionsEvent(
        Guid CreatureId,
        string CreatureName,
        IEnumerable<Condition> Conditions
        ) : IDomainEvent;
}
