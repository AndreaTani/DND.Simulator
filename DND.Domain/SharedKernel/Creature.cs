using DND.Domain.SharedKernel.Enum;
using DND.Domain.SharedKernel.ValueObjects;

namespace DND.Domain.SharedKernel
{
    public abstract class Creature : IAggregateRoot
    {
        // --- Identifications, basic Info and fundamental stata ---
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public int Level { get; protected set; }
        public virtual int Speed { get; protected set; }


        // --- Value objects and stats ---
        public AbilityScore AbilityScores { get; protected set; }
        public virtual int TemporaryHitPoints { get; protected set; }
        public int MaxHitPoints { get; protected set; }
        public int CurrentHitPoints { get; protected set; }
        public virtual int ArmorClass { get; protected set; }

        // Overridable property to return AC, can be customized in derived classes, calculated from Dexterity by default
        public virtual int DexterityModifier => AbilityScores.GetModifier(AbilityType.Dexterity);


        // List  of conditions currently affecting the creature
        public List<ConditionType> Conditions { get; protected set; } = [];


        // Prociencies
        public int ProficiencyBonus => 2 + (Level - 1) / 4;                             // Bonus based on level
        public List<SkillType> ProficientSkills { get; protected set; } = [];           // Skills to add to ability checks
        public List<SkillType> ExpertSkills { get; protected set; } = [];               // Skills to add double to ability checks
        public List<AbilityType> ProficientSavingThrows { get; protected set; } = [];   // Saving throws


        // Resistances, immunities, and vulnerabilities
        public List<DamageType> DamageResistances { get; protected set; } = [];
        public List<DamageType> DamageImmunities { get; protected set; } = [];
        public List<DamageType> DamageVulnerabilities { get; protected set; } = [];

        // Methoids for common behaviors can be added here

        // Constructor
        protected Creature(string name, int level, AbilityScore abilityScores, int maxHitPoints, int armorClass, int speed)
        {
            Id = Guid.NewGuid();
            Name = name;
            Level = level;
            AbilityScores = abilityScores;
            MaxHitPoints = maxHitPoints;
            CurrentHitPoints = maxHitPoints; // Start at full health
            ArmorClass = armorClass;
            Speed = speed;
        }


        // Support method to calculate final damage, complex logic is isolated here
        protected abstract int CalculateFinalDamage(int baseDamage, Enum.DamageType damageType);

        // Method to apply damage to the creature taking into account resistances, immunities, and vulnerabilities
        public void TakeDamage(int damage, Enum.DamageType damageType)
        {
            int finalDamage = CalculateFinalDamage(damage, damageType);

            // Deplete TemporaryHitPoints first
            if (TemporaryHitPoints > 0)
            {
                int tempHpUsed = Math.Min(TemporaryHitPoints, finalDamage);
                TemporaryHitPoints -= tempHpUsed;
                finalDamage -= tempHpUsed;
            }

            // Apply any remaining damage to CurrentHitPoints
            if (finalDamage > 0)
            {
                CurrentHitPoints = Math.Max(0, CurrentHitPoints - finalDamage);
            }

            // generate domain events if needed (e.g., CreatureUnconscious, CreatureDied)
        }


        // Method to calculate saving throw modifier for a given ability keping into account proficiencies
        public int GetSavingThrowModifier(AbilityType ability)
        {
            int modifier = AbilityScores.GetModifier(ability);
            if (ProficientSavingThrows.Contains(ability))
            {
                modifier += ProficiencyBonus;
            }
            return modifier;
        }


        // Method to calculate skill check modifier for a given skill keeping into account proficiencies and expertise
        public int GetSkillCheckModifier(SkillType skill)
        {
            int modifier = AbilityScores.GetModifier(skill.GetAbilityType());

            // Expertise takes precedence over proficiency
            if (ExpertSkills.Contains(skill))
            {
                modifier += ProficiencyBonus * 2;
            }
            else if (ProficientSkills.Contains(skill))
            {
                modifier += ProficiencyBonus;
            }

            return modifier;
        }


        // Method to heal the creature, cannot exceed MaxHitPoints and it does not affect TemporaryHitPoints
        public void Heal(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Heal amount must be non-negative.");
            }
            CurrentHitPoints = Math.Min(MaxHitPoints, CurrentHitPoints + amount);
        }
    }
}
