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

        // Add specific senses to the creature
        Task AddSensesAsync(Guid creatureId, IEnumerable<Sense> senses);

        // Remove specific senses from the creature
        Task RemoveSensesAsync(Guid creatureId, IEnumerable<Sense> senses);

        // Add specific languages to the creature
        Task AddLanguagesAsync(Guid creatureId, IEnumerable<Language> languages);

        // Remove specific languages from the creature
        Task RemoveLanguagesAsync(Guid creatureId, IEnumerable<Language> languages);

        // Add specific saving throw proficiencies to the creature
        Task AddProficiencySavingThrowsAsync(Guid creatureId, IEnumerable<Ability> abilities);

        // Remove specific saving throw proficiencies from the creature
        Task RemoveProficiencySavingThrowsAsync(Guid creatureId, IEnumerable<Ability> abilities);

        // Add specifric damage type immunities to the creature
        Task AddDamageImmunityAsync(Guid creatureId, IEnumerable<DamageType> damageTypes);

        // Remove specifric damage type immunities to the creature
        Task RemoveDamageImmunityAsync(Guid creatureId, IEnumerable<DamageType> damageTypes);

        // Check if the creature is a player character.
        Task<bool> IsPlayerCharacterAsync(Guid creatureId);
    }
}
