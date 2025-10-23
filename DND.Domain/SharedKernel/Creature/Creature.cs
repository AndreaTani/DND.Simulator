using DND.Domain.SharedKernel.Events;

namespace DND.Domain.SharedKernel
{
    public abstract class Creature : IAggregateRoot
    {
        // Private fields
        private readonly List<Ability> _proficientSavingThrows = [];
        private readonly List<Condition> _conditions = [];
        private readonly List<Condition> _conditionImmunities = [];
        private readonly List<DamageType> _damageImmunities = [];
        private readonly List<DamageType> _damageResistances = [];
        private readonly List<DamageType> _damageVulnerabilities = [];
        private readonly List<IDamageAdjustmentRule> _damageAdjustmentRules = [];
        private readonly List<Language> _languages = [];
        private readonly List<Sense> _senses = [];
        private readonly List<Skill> _expertSkills = [];
        private readonly List<Skill> _proficientSkills = [];
        private readonly List<IDomainEvent> _domainEvents = [];
        private readonly List<TemporaryDamageModification> _temporaryDamageModifications = [];

        // Identifications, basic Info and fundamental stata
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public int Level { get; protected set; }

        public CreatureType CreatureType { get; protected set; }


        // Base movement speed in feet per round, can be set for monsters/NPCs or computed for player characters using equipment, class, racial bonuses, etc.
        // Current movement speed in feet per round, can be set to base as a default or modified by conditions, spells, effects, etc.
        public virtual Speed BaseSpeed { get; protected set; }
        public virtual Speed CurrentSpeed { get; protected set; }

        // Ability scores, can be set for monsters/NPCs or computed for player characters using point buy, standard array, or rolling
        public AbilityScores AbilityScores { get; protected set; }

        // Hit points, can be set for monsters/NPCs or computed for player characters using class, level, Constitution modifier, and other bonuses
        public virtual int TemporaryHitPoints { get; private set; }
        public virtual int MaxHitPoints { get; private set; }
        public int CurrentHitPoints { get; private set; }

        // Flags for spellcasting and concentration, can be set for monsters/NPCs or player characters
        public bool IsSpellcaster { get; protected set; } = false;
        public bool IsConcentrating { get; protected set; } = false;

        // Initiative bonus, can be set for monsters/NPCs or computed for player characters using Dexterity modifier and other bonuses
        public virtual int InitiativeModifier => DexterityModifier; // Overrideable for Feats/Class bonuses

        // Base AC, can be set for monsters/NPCs
        public virtual int ArmorClass { get; protected set; } = 0;

        // Overridable property to return AC, can be customized in derived classes, calculated from Dexterity by default
        // By default, if ArmorClass is set (e.g., for monsters/NPCs), use it directly. 
        // Otherwise, calculate AC based on D&D rules: 10 + DexterityModifier (unarmored), or allow derived classes to override for custom logic.
        public virtual int DexterityModifier => AbilityScores.GetModifier(Ability.Dexterity);

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


        // Size of the creature, can affect combat and other mechanics
        public Size Size { get; protected set; } = Size.Medium;

        // List of senses the creature possesses
        public IReadOnlyList<Sense> Senses => _senses;

        // List  of conditions currently affecting the creature
        public IReadOnlyList<Condition> Conditions => _conditions;

        // List of condition immunities the creature possesses
        public IReadOnlyList<Condition> ConditionImmunities => _conditionImmunities;

        // Languages the creature can speak and understand
        public IReadOnlyList<Language> Languages => _languages;


        // Properties to check if the creature is Unconcious or Dead based on conditions or hit points
        public bool IsUnconscious => Conditions.Contains(Condition.Unconscious);
        public bool IsDead => Conditions.Contains(Condition.Dead);


        // Proficiencies (skills, saving throws, etc.)
        public virtual int ProficiencyBonus => 2 + (Level - 1) / 4;
        public IReadOnlyList<Skill> ProficientSkills => _proficientSkills;
        public IReadOnlyList<Skill> ExpertSkills => _expertSkills;
        public IReadOnlyList<Ability> ProficientSavingThrows => _proficientSavingThrows;


        // Resistances, immunities, and vulnerabilities
        public IReadOnlyList<DamageType> DamageResistances => _damageResistances;
        public IReadOnlyList<DamageType> DamageImmunities => _damageImmunities;
        public IReadOnlyList<DamageType> DamageVulnerabilities => _damageVulnerabilities;


        // Properties to check if the creature is resistant, immune, or vulnerable to a specific damage type
        public bool IsResistantTo(DamageType damageType) => _damageResistances.Contains(damageType);
        public bool IsImmuneTo(DamageType damageType) => _damageImmunities.Contains(damageType);
        public bool IsVulnerableTo(DamageType damageType) => _damageVulnerabilities.Contains(damageType);

        // List of damage adjustment rules applied to this creature
        protected IReadOnlyList<IDamageAdjustmentRule> DamageAdjustmentRules => _damageAdjustmentRules;




        // Domain events
        /// <summary>
        /// Exposes the domain events as read-only toi the Application/Infrastructure layers
        /// </summary>
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Clears all domain events after they have been published, procesed, 
        /// and dispatched by the Application/Infrastructure layers
        /// </summary>
        public void ClearDomainEvents() => _domainEvents.Clear();

        /// <summary>
        /// Add a new event to the internal list of domain events
        /// accessible only within this class and derived classes
        /// </summary>
        /// <param name="domainEvent"></param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }


        protected Creature(string name, CreatureType creatureType, Size size, AbilityScores abilityScores, int maxHitPoints, Speed speed, int armorClass = 0, int level = 1)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatureType = creatureType;
            Size = size;
            AbilityScores = abilityScores;
            MaxHitPoints = maxHitPoints;
            CurrentHitPoints = maxHitPoints; // Start at full health
            BaseSpeed = speed;
            CurrentSpeed = speed; // Start with base speed
            ArmorClass = armorClass;
            Level = level;
        }


        // Helper method to check if the creature is proficient in a skill or even an expert in it
        public bool IsProficientInSkill(Skill skill) => ProficientSkills.Contains(skill) || ExpertSkills.Contains(skill);
        public bool HasExpertiseInSkill(Skill skill) => ExpertSkills.Contains(skill);


        // Calculate final damage using the damage adjustment rules
        public virtual int CalculateFinalDamage(int baseDamage, DamageType damageType, DamageSource damageSource, bool isSilvered)
        {
            int finalDamage = baseDamage;

            // Immunity check, if true no damage is received
            foreach (var rule in DamageAdjustmentRules.OfType<IImmunityRule>())
            {
                if (rule.IsImmune(damageType, damageSource, isSilvered))
                {
                    return 0;
                }
            }

            float modifier = 1.0f;

            // Manages resistances and vulnerabilities
            foreach (var rule in DamageAdjustmentRules.OfType<IModificationRule>())
            {
                float ruleModifier = rule.GetModificationFactor(damageType, damageSource, isSilvered);

                if (ruleModifier < 1.0f)
                {
                    // this grants resistance
                    modifier = Math.Min(modifier, ruleModifier);
                }
                else if (ruleModifier > 1.0f)
                {
                    // this grants vulnerability
                    modifier = Math.Max(modifier, ruleModifier);
                }
            }

            // Manages temporary damage modifier
            var tempMod = _temporaryDamageModifications.FirstOrDefault(mod => mod.TypeOfDamage == damageType);
            if (tempMod != null)
            {
                if (tempMod.Modifier < 1.0f)
                {
                    // this grants temporary resistance
                    modifier = Math.Min(modifier, tempMod.Modifier);
                }
                else if (tempMod.Modifier > 1.0f)
                {
                    // this grants temporary vulnerability
                    modifier = Math.Max(modifier, tempMod.Modifier);
                }
            }

            finalDamage = (int)Math.Round(finalDamage * modifier);

            return Math.Max(0, finalDamage);
        }


        /// <summary>
        /// Applies damage to the creature, taking into account resistances, immunities, and vulnerabilities.
        /// </summary>
        /// <remarks>Damage is first applied to the creature's temporary hit points, if any. Any remaining
        /// damage is then subtracted from the creature's current hit points. If the creature's current hit points drop
        /// to 0 or below, it may become unconscious or die, depending on the total damage taken relative to its maximum
        /// hit points.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="damage"/> is less than 0.</exception>
        public void TakeDamage(int damage, DamageType damageType, DamageSource damageSource, bool isSilvered)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage), "Damage amount must be non-negative.");
            }

            int initialHp = CurrentHitPoints;
            int finalDamage = CalculateFinalDamage(damage, damageType, damageSource, isSilvered);
            int damageTaken = finalDamage; // Store the actual damage taken after calculations

            // Deplete TemporaryHitPoints first
            if (TemporaryHitPoints > 0)
            {
                int tempHpUsed = Math.Min(TemporaryHitPoints, finalDamage);
                TemporaryHitPoints -= tempHpUsed;
                damageTaken -= tempHpUsed;
            }

            // Apply any remaining damage to CurrentHitPoints
            if (damageTaken > 0)
            {
                CurrentHitPoints -= damageTaken;
            }

            int amountChanged = CurrentHitPoints - initialHp;

            if (amountChanged != 0)
            {
                // Create and add a domain event to notify about the HP change
                var damagingEvent = new CreatureHPChangedEvent(
                    creatureId: Id,
                    previousHp: initialHp,
                    currentHp: CurrentHitPoints,
                    maxHp: MaxHitPoints,
                    amount: amountChanged,  // negative value if damage taken
                    damageType: damageType
                );
                AddDomainEvent(damagingEvent);
            }
        }


        /// <summary>
        /// Calculates the saving throw modifier for the specified ability, taking into account proficiencies.
        /// </summary>
        /// <param name="ability">The ability for which the saving throw modifier is calculated.</param>
        /// <returns>The saving throw modifier for the specified ability. This includes the ability's base modifier and the
        /// proficiency bonus if the character is proficient in the saving throw.</returns>
        public int GetSavingThrowModifier(Ability ability)
        {
            int modifier = AbilityScores.GetModifier(ability);
            if (ProficientSavingThrows.Contains(ability))
            {
                modifier += ProficiencyBonus;
            }
            return modifier;
        }


        /// <summary>
        /// Calculates the skill check modifier for the specified skill, taking into account ability scores,
        /// proficiencies, and expertise.
        /// </summary>
        /// <remarks>If the skill is associated with expertise, the proficiency bonus is doubled. If the
        /// skill is only associated with proficiency, the standard proficiency bonus is added.</remarks>
        /// <param name="skill">The skill for which to calculate the modifier.</param>
        /// <returns>The total skill check modifier, which includes the ability score modifier and any applicable proficiency or
        /// expertise bonuses.</returns>
        public int GetSkillCheckModifier(Skill skill)
        {
            int modifier = AbilityScores.GetModifier(skill.GetAbility());

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


        /// <summary>
        /// Restores hit points to the creature, up to its maximum hit points.
        /// </summary>
        /// <remarks>This method does not affect temporary hit points. If the specified amount would cause
        /// the  creature's hit points to exceed its maximum, the hit points are capped at the maximum value.</remarks>
        /// <param name="amount">The amount of hit points to restore. Must be non-negative.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="amount"/> is less than zero.</exception>
        public void Heal(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Heal amount must be non-negative.");
            }
            int initialHp = CurrentHitPoints;

            CurrentHitPoints = Math.Min(MaxHitPoints, CurrentHitPoints + amount);

            // Create and add a domain event to notify about the HP change
            // the negative amount value is for damage
            var healingEvent = new CreatureHPChangedEvent(
                creatureId: Id,
                previousHp: initialHp,
                currentHp: CurrentHitPoints,
                maxHp: MaxHitPoints,
                amount: amount
            );
            AddDomainEvent(healingEvent);
        }


        /// <summary>
        /// Adds temporary hit points, replacing the existing value if the new amount is greater.
        /// </summary>
        /// <remarks>Temporary hit points are updated only if the specified <paramref name="amount"/>  is
        /// greater than the current value. This ensures that temporary hit points  never decrease as a result of this
        /// method.</remarks>
        /// <param name="amount">The number of temporary hit points to add. Must be non-negative.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="amount"/> is less than 0.</exception>
        public void AddTemporaryHitPoints(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Temporary hit points must be non-negative.");
            }
            TemporaryHitPoints = Math.Max(TemporaryHitPoints, amount);
        }


        // Add or remove a condition immunity to the creature, avoiding duplicates when adding
        protected void AddConditionImmunities(IEnumerable<Condition> conditions)
        {
            _conditionImmunities.AddRange(conditions.Where(c => !_conditionImmunities.Contains(c)));
        }
        protected void AddConditionImmunity(Condition condition)
        {
            if (!_conditionImmunities.Contains(condition))
            {
                _conditionImmunities.Add(condition);
            }
        }
        protected void RemoveConditionImmunity(Condition condition)
        {
            _conditionImmunities.Remove(condition);
        }

        // Add or remove a condition to the creature, avoiding duplicates when adding
        protected void AddConditions(IEnumerable<Condition> conditions)
        {
            _conditions.AddRange(conditions.Where(c => !_conditions.Contains(c)));
        }
        protected void AddCondition(Condition condition)
        {
            if (_conditionImmunities.Contains(condition))
            {
                var immuneEvent = new CreatureImmuneToConditionsEvent(Id, [condition]);
                AddDomainEvent(immuneEvent);
                return;
            }

            if (!_conditions.Contains(condition))
            {
                _conditions.Add(condition);
            }
        }
        protected void RemoveCondition(Condition condition)
        {
            _conditions.Remove(condition);
        }


        // Add or remove a sense to the creature, avoiding duplicates when adding
        protected void AddSenses(IEnumerable<Sense> senses)
        {
            _senses.AddRange(senses.Where(s => !_senses.Contains(s)));
        }
        protected void AddSense(Sense sense)
        {
            if (!_senses.Contains(sense))
            {
                _senses.Add(sense);
            }
        }
        protected void RemoveSense(Sense sense)
        {
            _senses.Remove(sense);
        }


        // Add a or remove a language to the creature, avoiding duplicates when adding
        protected void AddLanguages(IEnumerable<Language> languages)
        {
            _languages.AddRange(languages.Where(l => !_languages.Contains(l)));
        }
        protected void AddLanguage(Language language)
        {
            if (!_languages.Contains(language))
            {
                _languages.Add(language);
            }
        }
        protected void RemoveLanguage(Language language)
        {
            _languages.Remove(language);
        }


        // Reset current speed to base speed, useful after effects that modify speed end
        protected void ResetSpeed()
        {
            CurrentSpeed = BaseSpeed;
        }


        // Add a or remove a proficient skill to the creature, avoiding duplicates when adding
        protected void AddProficientSkills(IEnumerable<Skill> skills)
        {
            _proficientSkills.AddRange(skills.Where(s => !_proficientSkills.Contains(s) && !_expertSkills.Contains(s)));
        }
        protected void AddProficientSkill(Skill skill)
        {
            if (!_proficientSkills.Contains(skill) && !_expertSkills.Contains(skill))
            {
                _proficientSkills.Add(skill);
            }
        }
        protected void RemoveProficientSkill(Skill skill)
        {
            _proficientSkills.Remove(skill);
        }


        // Add or remove an expert skill to the creature, avoiding duplicates when adding
        // If the skill is already in proficient skills, remove it to avoid duplication
        protected void AddExpertSkills(IEnumerable<Skill> skills)
        {
            _expertSkills.AddRange(skills.Where(s => !_expertSkills.Contains(s)).Select(s =>
            {
                _proficientSkills.Remove(s);
                return s;
            }));
        }
        protected void AddExpertSkill(Skill skill)
        {
            if (!_expertSkills.Contains(skill))
            {
                _proficientSkills.Remove(skill);
                _expertSkills.Add(skill);
            }
        }
        protected void RemoveExpertSkill(Skill skill)
        {
            _expertSkills.Remove(skill);
        }


        // Add or remove a proficient saving throw to the creature, avoiding duplicates when adding
        protected void AddProficientSavingThrows(IEnumerable<Ability> abilities)
        {
            _proficientSavingThrows.AddRange(abilities.Where(a => !_proficientSavingThrows.Contains(a)));
        }
        protected void AddProficientSavingThrow(Ability ability)
        {
            if (!_proficientSavingThrows.Contains(ability))
            {
                _proficientSavingThrows.Add(ability);
            }
        }
        protected void RemoveProficientSavingThrow(Ability ability)
        {
            _proficientSavingThrows.Remove(ability);
        }


        // Add or remove  a damage resistance to the creature, avoiding duplicates when adding
        protected void AddDamageResistances(IEnumerable<DamageType> damageTypes)
        {
            _damageResistances.AddRange(damageTypes.Where(dt => !_damageResistances.Contains(dt)));
            _damageAdjustmentRules.AddRange(damageTypes
                .Where(dt => !_damageResistances.Contains(dt))
                .Select(dt => new SimpleDamageResisistanceRule(dt))
            );
        }
        protected void AddDamageResistance(DamageType damageType)
        {
            if (!_damageResistances.Contains(damageType))
            {
                _damageResistances.Add(damageType);
                _damageAdjustmentRules.Add(new SimpleDamageResisistanceRule(damageType));
            }
        }
        protected void RemoveDamageResistance(DamageType damageType)
        {
            _damageResistances.Remove(damageType);
            _damageAdjustmentRules.RemoveAll(rule => rule.GetDamageType() == damageType && rule is SimpleDamageResisistanceRule);
        }


        // Add or remove a damage immunity to the creature, avoiding duplicates when adding
        protected void AddDamageImmunities(IEnumerable<DamageType> damageTypes)
        {
            _damageImmunities.AddRange(damageTypes.Where(dt => !_damageImmunities.Contains(dt)));
            _damageAdjustmentRules.AddRange(damageTypes
                .Where(dt => !_damageImmunities.Contains(dt))
                .Select(dt => new SimpleDamageImmunityRule(dt))
            );
        }
        protected void AddDamageImmunity(DamageType damageType)
        {
            if (!_damageImmunities.Contains(damageType))
            {
                _damageImmunities.Add(damageType);
                _damageAdjustmentRules.Add(new SimpleDamageImmunityRule(damageType));
            }
        }
        protected void RemoveDamageImmunity(DamageType damageType)
        {
            _damageImmunities.Remove(damageType);
            _damageAdjustmentRules.RemoveAll(rule => rule.GetDamageType() == damageType && rule is SimpleDamageImmunityRule);
        }


        // Add or remove a damage vulnerability to the creature, avoiding duplicates when adding
        protected void AddDamageVulnerabilities(IEnumerable<DamageType> damageTypes)
        {
            _damageVulnerabilities.AddRange(damageTypes.Where(dt => !_damageVulnerabilities.Contains(dt)));
            _damageAdjustmentRules.AddRange(damageTypes
                .Where(dt => !_damageVulnerabilities.Contains(dt))
                .Select(dt => new SimpleDamageVulnerabilityRule(dt))
            );
        }
        protected void AddDamageVulnerability(DamageType damageType)
        {
            if (!_damageVulnerabilities.Contains(damageType))
            {
                _damageVulnerabilities.Add(damageType);
                _damageAdjustmentRules.Add(new SimpleDamageVulnerabilityRule(damageType));
            }
        }
        protected void RemoveDamageVulnerability(DamageType damageType)
        {
            _damageVulnerabilities.Remove(damageType);
            _damageAdjustmentRules.RemoveAll(rule => rule.GetDamageType() == damageType && rule is SimpleDamageVulnerabilityRule);
        }

        // Add or remove a special damage rule
        protected void AddSpecialDamageRule(IDamageAdjustmentRule rule)
        {
            _damageAdjustmentRules.RemoveAll(r => r.Name == rule.Name);
            _damageAdjustmentRules.Add(rule);
        }

        /// <summary>
        /// Applies the unconscious condition to the creature if it's not dead
        /// When a creature is unconscious, it automatically lose concentation, falls prone and drops any held items.
        /// Usually a creature becomes unconscious when its CurrentHitPoints drop to 0 or below but are greater than 
        /// -MaxHitPoints, in the most cases the creature is also dying thus applying the dying condition as well.
        /// If the creature is a character or an important NPC, the application layer should handle death saving throws.
        /// There can be exceptions to this rule, for example when a creature is rendered unconscious by a spell or
        /// effect while still having positive hit points. In that case the parameter isDying should be set to false.
        /// Called by the Event Handler that has already verified that CurrentHitPoints <= 0 but > -MaxHitPoints or
        /// the Event Handler that manages creature's conditions by spells or effects.
        /// </summary>
        public void ApplyUnconsciousness(bool isDying = true)
        {
            if (IsDead) return;

            if (!IsUnconscious)
            {
                AddCondition(Condition.Unconscious);
                AddCondition(Condition.Prone);

                // Drop concentration if unconscious
                IsConcentrating = false;

                //TODO: Drop held items

                if (isDying)
                {
                    AddCondition(Condition.Dying);
                }

                // TODO: Trigger a domain event for the creature unconscious state
            }
        }

        /// <summary>
        /// Applies the dead condition to the creature, removing unconscious and dying conditions if present.
        /// The dead creature automatically falls prone and drops any held items.
        /// Called by the Event Handler that has already verified that CurrentHitPoints <= -MaxHitPoints.
        /// </summary>
        public void ApplyDeath()
        {
            if (!IsDead)
            {
                AddCondition(Condition.Dead);
                AddCondition(Condition.Prone);

                // Remove conditions that are no longer relevant
                RemoveCondition(Condition.Unconscious);
                RemoveCondition(Condition.Dying);

                // Drop concentration if dead
                IsConcentrating = false;

                //TODO: Drop held items

                // Ensure CurrentHitPoints is not positive
                CurrentHitPoints = Math.Min(0, CurrentHitPoints);

                // TODO: Trigger a domain event for the creature death
            }
        }

        /// <summary>
        /// Revive the creature from unconscious or dead state, setting CurrentHitPoints to 1 if it was 0 or negative.
        /// </summary>
        public void Revive()
        {
            if (IsDead || IsUnconscious)
            {
                RemoveCondition(Condition.Dead);
                RemoveCondition(Condition.Unconscious);

                // Revive with 1 HP if dead or unconscious with 0 or negative HP
                if (CurrentHitPoints <= 0)
                {
                    CurrentHitPoints = 1;
                }

                // TODO: Trigger a domain event for the creature revival
            }
        }

        // Apply temporary damage modification (used by ApplicationService)
        public virtual void ApplyTemporaryDamageModification(TemporaryDamageModification modification)
        {
            // TODO: Add logic for stacking rules

            _temporaryDamageModifications.Add(modification);

            //TODO: Add domainEvent for tracking by the combat service
        }

        // Remove temporary damage modifications (used by the CombatService)
        public virtual void RemoveTempoDamageModification(Guid sourceId, DamageType damageType)
        {
            _temporaryDamageModifications.RemoveAll(mod => mod.SourceId == sourceId && mod.TypeOfDamage == damageType);

            // TODO: Add domainEvent to notify when the effect is gone
        }
    }
}
