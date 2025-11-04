using DND.Domain.SharedKernel;

namespace DND.Application.Contracts
{
    public interface ICreatureService
    {
        // Handler needs to check the HP state and apply the necessary conditions if any.
        Task HandleCreatureHpStatusAsync(Guid creatureId, int maxHp, int currentHp);

        // Apply the specified condition to the creature.
        Task ApplyConditionsAsync(Guid creatureId, Condition condition);
    }
}
