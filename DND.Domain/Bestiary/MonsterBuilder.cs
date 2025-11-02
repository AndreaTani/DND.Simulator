using DND.Domain.SharedKernel;

namespace DND.Domain.Bestiary
{
    public sealed class MonsterBuilder
    {
        // Core requested fields
        private readonly string _name;
        private readonly int _proficiencyBonus;
        private readonly CreatureType _creatureType;
        private readonly Size _size;
        private readonly AbilityScores _abilityScores;
        private readonly int _armorClass;
        private readonly Speed _speed;
        private int _averageMaxHitPoints;
        private string? _hitDiceExpression;
        private int? _customMaxHitPoints;

        // Optional fields with default values
        private readonly List<DamageType> _immunities = [];
        private readonly List<DamageType> _resistances = [];
        private readonly List<DamageType> _vulnerabilities = [];
        private readonly List<Condition> _initialConditions = [];
        private readonly List<Condition> _conditionImmunities = [];
        private readonly List<Sense> _senses = [Sense.NormalVision, Sense.Hearing];
        private readonly List<Language> _languages = [];
        private readonly List<Skill> _proficientSkills = [];
        private readonly List<Skill> _expertSkills = [];
        private readonly List<Ability> _proficientSavingThrows = [];

        // Derived properties
        public bool IsBlind => !_senses.Contains(Sense.NormalVision);
        public bool IsDeaf => !_senses.Contains(Sense.Hearing);


        // --- Mandatory first step ---
        // Constructor with only required parameters
        public MonsterBuilder(string name, int proficiencyBonus, CreatureType creatureType, Size size, AbilityScores abilityScores, int averageMaxHitPoints, int armorClass, Speed speed)
        {
            _name = name;
            _proficiencyBonus = proficiencyBonus;
            _creatureType = creatureType;
            _size = size;
            _abilityScores = abilityScores;
            _averageMaxHitPoints = averageMaxHitPoints;
            _armorClass = armorClass;
            _speed = speed;
        }

        // --- Optional methods ---
        // Damage Type Immunities
        public MonsterBuilder WithImmunities(params DamageType[] immunities)
        {
            _immunities.AddRange(immunities);
            return this;
        }

        // Damage Type Resistances
        public MonsterBuilder WithResistances(params DamageType[] resistances)
        {
            _resistances.AddRange(resistances);
            return this;
        }

        // Damage Type Vulnerabilities
        public MonsterBuilder WithVulnerabilities(params DamageType[] vulnerabilities)
        {
            _vulnerabilities.AddRange(vulnerabilities);
            return this;
        }

        // Initial Conditions
        public MonsterBuilder WithInitialConditions(params Condition[] conditions)
        {
            _initialConditions.AddRange(conditions);
            return this;
        }

        // Condition Immunities
        public MonsterBuilder WithConditionImmunities(params Condition[] conditionImmunities)
        {
            _conditionImmunities.AddRange(conditionImmunities);
            return this;
        }

        // Senses
        public MonsterBuilder WithSenses(params Sense[] senses)
        {
            _senses.AddRange(senses);
            return this;
        }

        // Languages
        public MonsterBuilder WithLanguages(params Language[] languages)
        {
            _languages.AddRange(languages);
            return this;
        }

        public MonsterBuilder WithProficientSkills(params Skill[] skills)
        {
            _proficientSkills.AddRange(skills);
            return this;
        }

        public MonsterBuilder WithExpertSkills(params Skill[] skills)
        {
            _expertSkills.AddRange(skills);
            _proficientSkills.RemoveAll(s => skills.Contains(s));
            return this;
        }

        public MonsterBuilder WithProficentSavingThrows(params Ability[] abilities)
        {
            _proficientSavingThrows.AddRange(abilities);
            return this;
        }

        public MonsterBuilder WithAverageHitPoints(string hitdiceExpression, int averageMaxHp)
        {
            _hitDiceExpression = hitdiceExpression;
            _averageMaxHitPoints = averageMaxHp;
            return this;
        }

        public MonsterBuilder WithCustomMaxHitPoints(int customMaxHp)
        {
            _customMaxHitPoints = customMaxHp;
            return this;
        }


        // Build method to create the Monster instance
        public Monster Build()
        {
            // Immediately fail the build if a damage type is found in more
            // than one list between immunities, resistance, and vulnerability
            ValidateDamageExclusivity();

            int finalMaxHp;

            if (_customMaxHitPoints.HasValue)
            {
                // Use custom max hit points if provided
                finalMaxHp = _customMaxHitPoints.Value;
            }
            else
            {
                // Use average max hit points if no custom value is provided
                finalMaxHp = _averageMaxHitPoints;
            }

            // NOTE: Roling the hitdice expression will be a task for the
            // Application layer and the result will be passed to the Monster
            // constructor as custom max HP


            return new Monster(
                name: _name,
                proficiencyBonus: _proficiencyBonus,
                creatureType: _creatureType,
                size: _size,
                abilityScores: _abilityScores,
                maxHitPoints: finalMaxHp,
                currentHitPoints: finalMaxHp,
                hitDiceExpression: _hitDiceExpression,
                armorClass: _armorClass,
                speed: _speed,
                immunities: _immunities,
                resistances: _resistances,
                vulnerabilities: _vulnerabilities,
                initialConditions: _initialConditions,
                conditionImmunities: _conditionImmunities,
                senses: _senses,
                languages: _languages,
                proficientSkills: _proficientSkills,
                expertSkills: _expertSkills,
                proficientSavingThrows: _proficientSavingThrows
            );
        }

        /// <summary>
        /// Validate immunities, resistances and vulnerabilities to ensure that
        /// a damage type can be only added in one of these lists.
        /// Throws an exception if conflicting damage types are found.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        private void ValidateDamageExclusivity()
        {
            var conflictingImmunityResistances = _immunities.Intersect(_resistances);
            var conflictingImmunityVulnerabilities = _immunities.Intersect(_vulnerabilities);
            var conflictingResistanceVulnerabilities = _resistances.Intersect(_vulnerabilities);

            var allConflicts = conflictingImmunityResistances
                .Concat(conflictingImmunityVulnerabilities)
                .Concat(conflictingResistanceVulnerabilities)
                .Distinct()
                .ToList();

            if (allConflicts.Count > 0)
            {
                var conflictString = string.Join(", ", allConflicts);
                throw new InvalidOperationException(
                    $"Invalid Monster data: The following damage types are listed as conflicting (Immunity, Resistance, or Vulnerability): {conflictString}. A damage type can only be in one category."
                );
            }
        }
    }
}
