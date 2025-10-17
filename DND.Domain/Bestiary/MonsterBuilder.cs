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
        private readonly int _maxHitPoints;
        private readonly int _armorClass;
        private readonly Speed _speed;

        // Optional fields with default values
        private readonly List<DamageType> _immunities = [];
        private readonly List<DamageType> _resistances = [];
        private readonly List<DamageType> _vulnerabilities = [];
        private readonly List<Condition> _initialConditions = [];
        private readonly List<Condition> _conditionImmunities = [];
        private readonly List<Sense> _senses = [];
        private readonly List<Language> _languages = [];

        // --- Mandatory first step ---
        // Constructor with only required parameters
        public MonsterBuilder(string name, int proficiencyBonus, CreatureType creatureType, Size size, AbilityScores abilityScores, int maxHitPoints, int armorClass, Speed speed)
        {
            _name = name;
            _proficiencyBonus = proficiencyBonus;
            _creatureType = creatureType;
            _size = size;
            _abilityScores = abilityScores;
            _maxHitPoints = maxHitPoints;
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

        public MonsterBuilder WithStandardSenses()
        {
            _senses.AddRange([Sense.NormalVision, Sense.Hearing, Sense.Speaking]);
            return this;
        }

        public MonsterBuilder WithStandardLanguage()
        {
            _languages.Add(Language.Common);
            return this;
        }

        // Build method to create the Monster instance
        public Monster Build()
        {
            return new Monster(
                name: _name,
                proficiencyBonus: _proficiencyBonus,
                creatureType: _creatureType,
                size: _size,
                abilityScores: _abilityScores,
                maxHitPoints: _maxHitPoints,
                armorClass: _armorClass,
                speed: _speed,
                immunities: _immunities,
                resistances: _resistances,
                vulnerabilities: _vulnerabilities,
                initialConditions: _initialConditions,
                conditionImmunities: _conditionImmunities,
                senses: _senses,
                languages: _languages
            );
        }
    }
}
