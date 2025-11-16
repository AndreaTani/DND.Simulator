using DND.Domain.SharedKernel;

namespace DND.Application.Contracts
{
    public interface ICreatureService
    {
        // Handler needs to check the HP state and apply the necessary conditions if any.
        Task HandleCreatureHpStatusAsync(Guid creatureId, int maxHp, int currentHp);

        // Apply the specified conditions to the creature.
        Task ApplyConditionsAsync(Guid creatureId, IEnumerable<Condition> conditions);

        // Remove the specific conditions from the creature
        Task RemoveConditionsAsync(Guid creatureId, IEnumerable<Condition> conditions);

        // Mark the specific skills with Proficiency
        Task AddProficientSkillsAsync(Guid creatureId, IEnumerable<Skill> skills);

        // Remove the specific skills from the ProficientSkills list
        Task RemoveProficientSkillsAsync(Guid creatureId, IEnumerable<Skill> skills);

        // Mark the specific skill with Expertise
        Task AddExpertSkillsAsync(Guid creatureId, IEnumerable<Skill> skills);

        // Remove the specific skills from the ExpertSkills list
        Task RemoveExpertSkillsAsync(Guid creatureId, IEnumerable<Skill> skills);

        // Check if the creature is a player character.
        Task<bool> IsPlayerCharacterAsync(Guid creatureId);
    }
}
