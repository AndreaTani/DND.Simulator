namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Value Object that stores the data that helps the service manage 
    /// the duration of the temporaty effect
    /// </summary>
    public record TemporaryDamageModification(
        DamageType TypeOfDamage,
        float Modifier,
        Guid SourceId,
        int ExpiresOnTurn,
        ExpirationType ExpiresAt
     );
}
