using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Services
{
    public class CreatureService : ICreatureService
    {
        public async Task AddExpertSkillsAsync(Guid creatureId, IEnumerable<Skill> skills)
        {
            throw new NotImplementedException();
        }

        public async Task AddLanguagesAsync(Guid creatureId, IEnumerable<Language> languages)
        {
            throw new NotImplementedException();
        }

        public async Task AddProficientSkillsAsync(Guid creatureId, IEnumerable<Skill> skills)
        {
            throw new NotImplementedException();
        }

        public async Task AddSensesAsync(Guid creatureId, IEnumerable<Sense> senses)
        {
            throw new NotImplementedException();
        }

        // TODO: remember to trigger the CreatureDiedEvent when the dead condition is applied to a creature
        public async Task ApplyConditionsAsync(Guid creatureId, IEnumerable<Condition> conditions)
        {
            throw new NotImplementedException();
        }

        public async Task HandleCreatureHpStatusAsync(Guid creatureId, int maxHp, int currentHp)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsPlayerCharacterAsync(Guid creatureId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveConditionsAsync(Guid creatureId, IEnumerable<Condition> conditions)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveExpertSkillsAsync(Guid creatureId, IEnumerable<Skill> skills)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveLanguagesAsync(Guid creatureId, IEnumerable<Language> languages)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveProficientSkillsAsync(Guid creatureId, IEnumerable<Skill> skills)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSensesAsync(Guid creatureId, IEnumerable<Sense> senses)
        {
            throw new NotImplementedException();
        }
    }
}
