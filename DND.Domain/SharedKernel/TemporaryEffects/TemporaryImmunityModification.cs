namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Value Object that stores the data that helps the service manage 
    /// the duration of the temporary effect
    /// </summary>
    public record TemporaryImmunityModification(
        DamageType TypeOfDamage,
        Guid SourceId,
        int ExpiresOnTurn,
        ExpirationType ExpiresAt
     );
}
