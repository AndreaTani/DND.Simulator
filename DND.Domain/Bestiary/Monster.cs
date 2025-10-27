using DND.Domain.SharedKernel;

namespace DND.Domain.Bestiary
{
    public sealed class Monster : Creature
    {
        // Overrides the base class ProficiencyBonus property in order to
        // get it from the MonsterBuilder call to the Monster constructor
        public override int ProficiencyBonus { get; }

        // Properties to store a hit dice expression for the maximum hit points calculation
        public string? HitDiceExpression { get; private set; }

        internal Monster(
            string name,
            int proficiencyBonus,
            CreatureType creatureType,
            Size size,
            AbilityScores abilityScores,
            int maxHitPoints,
            int currentHitPoints,
            string? hitDiceExpression,
            int armorClass,
            Speed speed,
            IEnumerable<DamageType> immunities,
            IEnumerable<DamageType> resistances,
            IEnumerable<DamageType> vulnerabilities,
            IEnumerable<Condition> initialConditions,
            IEnumerable<Condition> conditionImmunities,
            IEnumerable<Sense> senses,
            IEnumerable<Language> languages,
            IEnumerable<Skill> proficientSkills,
            IEnumerable<Skill> expertSkills,
            IEnumerable<Ability> proficientSavingThrows
        ) : base(name, creatureType, size, abilityScores, maxHitPoints, currentHitPoints, speed, armorClass)
        {
            ProficiencyBonus = proficiencyBonus;
            HitDiceExpression = hitDiceExpression;

            AddConditionImmunities(conditionImmunities);
            AddDamageImmunities(immunities);
            AddDamageResistances(resistances);
            AddDamageVulnerabilities(vulnerabilities);
            AddConditions(initialConditions);
            AddSenses(senses);
            AddLanguages(languages);
            AddProficientSkills(proficientSkills);
            AddExpertSkills(expertSkills);
            AddProficientSavingThrows(proficientSavingThrows);
        }
    }
}