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
        ) : base(name, creatureType, size, abilityScores, maxHitPoints, speed, armorClass)
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

        // Calculates the final damage after applying immunities, resistances, and vulnerabilities
        // For monsters it's sufficent to apply the immunities, resistances, and vulnerabilities directly
        // without considering any special abilities or traits
        protected override int CalculateFinalDamage(int baseDamage, DamageType damageType)
        {
            var finalDamage = baseDamage;

            if (IsImmuneTo(damageType))
            {
                finalDamage = 0;
            }
            else if (IsResistantTo(damageType))
            {
                finalDamage /= 2;
            }
            else if (IsVulnerableTo(damageType))
            {
                finalDamage *= 2;
            }

            return finalDamage;
        }

    }
}