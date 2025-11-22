namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Value Object that stores the data that helps the service manage 
    /// the duration of the temporary effect
    /// </summary>
    public record TemporaryDamageModification(
        DamageType TypeOfDamage,
        float Modifier,
        Guid SourceId,
        Guid CreatureId,
        string Name,
        int ExpiresOnTurn,
        ExpirationType ExpiresAt,
        ExpirationTrigger ExpiresOn
     );
}
