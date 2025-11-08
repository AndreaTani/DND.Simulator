namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Condition a creature can be affected with, in DND
    /// None is not a real condition in the D&D rules, just a placeholder for 
    /// "no condition". It's useful for default values in some cases.
    /// </summary>
    public enum Condition
    {
        None = 0,
        Blinded,
        Charmed,
        Deafened,
        Frightened,
        Grappled,
        Incapacitated,
        Invisible,
        Paralyzed,
        Petrified,
        Poisoned,
        Prone,
        Restrained,
        Stunned,
        Unconscious,
        Dying,
        Dead
    }
}
