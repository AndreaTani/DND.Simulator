namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Differrent sources of damage depending from the origin of the damage itself
    /// A mundane weapon, a magical spell or weapon, an environmental hazard, etc.
    /// </summary>
    public enum DamageSource
    {
        Mundane = 0,
        Magical = 1,
        Natural = 2
    }
}
