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

        // Base movement speed in feet per round, can be set for monsters/NPCs or computed for player characters using equipment, class, racial bonuses, etc.
        public virtual Speed BaseSpeed { get; protected set; }

        // Current movement speed in feet per round, can be set to base as a default or modified by conditions, spells, effects, etc.
        public virtual Speed CurrentSpeed { get; protected set; } 

        // --- Value objects and stats ---
        public AbilityScore AbilityScores { get; protected set; }
        public virtual int TemporaryHitPoints { get; protected set; }
        public int MaxHitPoints { get; protected set; }
        public int CurrentHitPoints { get; protected set; }
        public virtual int ArmorClass { get; protected set; } = 0; // Base AC, can be set for monsters/NPCs

        // Overridable property to return AC, can be customized in derived classes, calculated from Dexterity by default
        // By default, if ArmorClass is set (e.g., for monsters/NPCs), use it directly. 
        // Otherwise, calculate AC based on D&D rules: 10 + DexterityModifier (unarmored), or allow derived classes to override for custom logic.
        public virtual int DexterityModifier => AbilityScores.GetModifier(AbilityType.Dexterity);

        /// <summary>
        /// Returns the current Armor Class (AC) of the creature.
        /// - If ArmorClass is explicitly set (e.g., for monsters/NPCs), use it.
        /// - Otherwise, calculate AC as 10 + DexterityModifier (unarmored default).
        /// - Derived classes (e.g., PlayerCharacter, special NPCs) should override this property to implement
        ///   equipment, class, or racial bonuses, or other custom AC rules.
        /// </summary>
        public virtual int CurrentArmorClass
        {
            get
            {
                // If ArmorClass is set to a positive value, use it (for monsters/NPCs).
                if (ArmorClass > 0)
                    return ArmorClass;

                // Default unarmored AC: 10 + Dex modifier
                return 10 + DexterityModifier;
            }
        }


        // List  of conditions currently affecting the creature
        public List<ConditionType> Conditions { get; protected set; } = [];

        // properties to check specific conditions (unconscious, dead, etc.)
        // When a creature is unconscious either for a preexisting condition or for having negative hit points
        public bool IsUnconscious => Conditions.Contains(ConditionType.Unconscious) || CurrentHitPoints <= 0;
        // When a creature is dead either for a preexisting condition or for having hit points less than negative max hit points
        public bool IsDead => Conditions.Contains(ConditionType.Dead) || CurrentHitPoints <= -MaxHitPoints; 


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
        protected Creature(string name, int level, AbilityScore abilityScores, int maxHitPoints, Speed speed, int armorClass = 0)
        {
            Id = Guid.NewGuid();
            Name = name;
            Level = level;
            AbilityScores = abilityScores;
            MaxHitPoints = maxHitPoints;
            CurrentHitPoints = maxHitPoints; // Start at full health
            BaseSpeed = speed;
            CurrentSpeed = speed; // Start with base speed
            ArmorClass = armorClass;
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
                CurrentHitPoints -= finalDamage;
            }

            // When currrent hit points drop to 0 or below but stays over the
            // max negative hit points, the creature becomes unconscious,
            // if the current hit points drop below the max negative hit points, the creature dies
            if (CurrentHitPoints <= 0 && CurrentHitPoints > -MaxHitPoints)
            {
                if (!Conditions.Contains(ConditionType.Unconscious))
                {
                    Conditions.Add(ConditionType.Unconscious);
                }
                // if needed generate domain event CreatureUnconscious
            }
            else if (CurrentHitPoints <= -MaxHitPoints)
            {
                if (!Conditions.Contains(ConditionType.Dead))
                {
                    Conditions.Add(ConditionType.Dead);
                }
                // if needed generate domain event CreatureDead
            }
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

        // Propery to track successful and unsuccessful Death Saving Throws
        public (int Successes, int Failures) DeathSavingThrows { get; protected set; } = (0, 0);

        // Method to perform a death saving throw, updating the DeathSavingThrows property accordingly, left for the derived classes
        public abstract (int Successes, int Failures) PerformDeathSavingThrow(int roll);


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
