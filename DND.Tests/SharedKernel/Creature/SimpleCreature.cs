using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public class SimpleCreature : Creature
    {
        public SimpleCreature(
            string name,
            CreatureType creatureType,
            Size size,
            AbilityScores abilityScores,
            int maxHitPoints,
            int currentHitPoints,
            Speed speed,
            int armorClass = 0,
            int level = 1
        ) : base(name, creatureType, size, abilityScores, maxHitPoints, currentHitPoints, speed, armorClass, level)
        {
        }

        public void SetupDamageImmunities(params DamageType[] damageTypes)
        {
            AddDamageImmunities(damageTypes);
        }

        public void SetupDamageImmunity(DamageType damageType)
        {
            AddDamageImmunity(damageType);
        }

        public void SetupDamageResistances(params DamageType[] damageTypes)
        {
            AddDamageResistances(damageTypes);
        }

        public void SetupDamageResistance(DamageType damageType)
        {
            AddDamageResistance(damageType);
        }

        public void SetupVulnerability(DamageType damageType)
        {
            AddDamageVulnerability(damageType);
        }

        public void SetupSpecialRule(IDamageAdjustmentRule rule)
        {
            AddSpecialDamageRule(rule);
        }

        public void SetupConditionImmunities(params Condition[] conditions)
        {
            AddConditionImmunities(conditions);
        }

        public void SetupConditionImmunity(Condition condition)
        {
            AddConditionImmunity(condition);
        }

        public void SetupConditions(params Condition[] conditions)
        {
            AddConditions(conditions);
        }

        public void SetupCondition(Condition condition)
        {
            AddCondition(condition);
        }

        public void SetupSenses(params Sense[] senses)
        {
            AddSenses(senses);
        }

        public void SetupSense(Sense sense)
        {
            AddSense(sense);
        }

        public void SetupLanguages(params Language[] languages)
        {
            AddLanguages(languages);
        }

        public void SetupLanguage(Language language)
        {
            AddLanguage(language);
        }

        public void SetupProficientSavingThrows(params Ability[] abilities)
        {
            AddProficientSavingThrows(abilities);
        }

        public void SetupProficientSavingThrow(Ability ability)
        {
            AddProficientSavingThrow(ability);
        }

        public void SetupProficientSkills(params Skill[] skills)
        {
            AddProficientSkills(skills);
        }

        public void SetupProficientSkill(Skill skill)
        {
            AddProficientSkill(skill);
        }

        public void SetupExpertSkills(params Skill[] skills)
        {
            AddExpertSkills(skills);
        }

        public void SetupExpertSkill(Skill skill)
        {
            AddExpertSkill(skill);
        }
    }
}
