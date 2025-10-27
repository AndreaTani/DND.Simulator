namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Represents the contextual reason why a damage resistance was removed
    /// from a creature. This is used to distinguish between manual edits, 
    /// automatic domain enforcement, or effect expiration for logging and
    /// downstream processing.
    /// </summary>
    public enum RemovalReason
    {
        Manual = 1,
        OverridenByExculsivity = 2,
        EffectExpired = 3,
        RuleTriggered = 4
    }
}
