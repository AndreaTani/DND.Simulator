namespace DND.Application.Contracts
{
    /// <summary>
    /// Manages the state and logic specific to a dying creature's death saving throws.
    /// </summary>
    public interface IDeathSaveManagerService
    {
        /// <summary>
        /// Initializes the death save process for a creature that just entered the Dying state.
        /// This typically sets their success/failure count to zero.
        /// </summary>
        /// <param name="creatureId">The ID of the creature that is dying.</param>
        Task InitializeDeathSavesAsync(Guid creatureId);

        /// <summary>
        /// Records a death save roll, updates the creature's success/failure counters,
        /// and triggers events if the creature stabilizes or dies.
        /// </summary>
        /// <param name="creatureId">The ID of the creature rolling the save.</param>
        /// <param name="rollValue">The raw value of the d20 roll (1-20).</param>
        Task RecordDeathSaveRollAsync(Guid creatureId, int rollValue);

        // Other methods like RecordDeathSaveRollAsync will be added later.
    }
}