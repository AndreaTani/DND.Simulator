namespace DND.Application.Contracts
{
    /// <summary>
    /// Service responsible for managing the overall combat state, such as 
    /// initiative order and session status.
    /// </summary>
    public interface ICombatSessionService
    {
        /// <summary>
        /// Removes the specified creature from the active combat initiative 
        /// order, usually when the creature is dead.
        /// </summary>
        /// <param name="creatureId">The ID of the creature being removed.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task RemoveFromInitiativeAsync(Guid creatureId);

        /// <summary>
        /// Restores the specified creature to the active combat initiative 
        /// order, typically when the creature is revived. 
        /// </summary>
        /// <param name="creatureId">The ID of the creature being removed.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task RestoreToCombatAsync(Guid creatureId);

        // Future method example: Task CheckForCombatEndAsync();
    }
}
