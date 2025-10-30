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

        public void SetupImmunity(DamageType damageType)
        {
            AddDamageImmunity(damageType);
        }

        public void SetupResistance(DamageType damageType)
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

        public void SetupProficientySavingThrow(Ability ability)
        {
            AddProficientSavingThrow(ability);
        }

        public void SetupProficientSkill(Skill skill)
        {
            AddProficientSkill(skill);
        }

        public void SetupExpertSkill(Skill skill)
        {
            AddExpertSkill(skill);
        }
    }
}
