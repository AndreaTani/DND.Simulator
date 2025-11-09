using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Services
{
    public class CreatureService : ICreatureService
    {
        // TODO: remember to trigger the CreatureDiedEvent when the dead condition is applied to a creature
        public Task ApplyConditionsAsync(Guid creatureId, IEnumerable<Condition> conditions)
        {
            throw new NotImplementedException();
        }

        public Task HandleCreatureHpStatusAsync(Guid creatureId, int maxHp, int currentHp)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsPlayerCharacterAsync(Guid creatureId)
        {
            throw new NotImplementedException();
        }
    }
}
