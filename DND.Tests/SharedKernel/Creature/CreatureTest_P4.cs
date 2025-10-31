using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public partial class CreatureTest
    {
        // Test data for all senses
        public static TheoryData<Sense> AllSenses => [.. Enum.GetValues(typeof(Sense)).Cast<Sense>().ToArray()];

        // Test data for all languages
        public static TheoryData<Language> AllLanguages => [.. Enum.GetValues(typeof(Language)).Cast<Language>().ToArray()];


        [Theory]
        [MemberData(nameof(AllConditions))]
        public void AddConditionImmunities_WhenAddingConditionImmunities_ShouldAddThemWithoutDuplicates(Condition condition)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupConditionImmunities([condition, condition]);
            var conditionImmunities = sut.ConditionImmunities;

            // Assert
            Assert.Contains(condition, conditionImmunities);
            Assert.Equal(1, conditionImmunities.Count(c => c == condition));
        }

        [Theory]
        [MemberData(nameof(AllConditions))]
        public void AddConditionImmunity_WhenAddingConditionImmunity_ShouldAddItWithoutDuplicates(Condition condition)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupConditionImmunity(condition);
            sut.SetupConditionImmunity(condition);
            var conditionImmunities = sut.ConditionImmunities;

            // Assert
            Assert.Contains(condition, conditionImmunities);
            Assert.Equal(1, conditionImmunities.Count(c => c == condition));
        }

        [Theory]
        [MemberData(nameof(AllConditions))]
        public void AddConditions_WhenAddingConditions_ShouldAddItWithoutDuplicates(Condition condition)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupConditions([condition, condition]);
            var conditions = sut.Conditions;

            // Assert
            Assert.Contains(condition, conditions);
            Assert.Equal(1, conditions.Count(c => c == condition));
        }

        [Theory]
        [MemberData(nameof(AllConditions))]
        public void AddCondition_WhenAddingCondition_ShouldAddItWithoutDuplicates(Condition condition)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupCondition(condition);
            sut.SetupCondition(condition);
            var conditions = sut.Conditions;

            // Assert
            Assert.Contains(condition, conditions);
            Assert.Equal(1, conditions.Count(c => c == condition));
        }

        [Theory]
        [MemberData(nameof(AllSenses))]
        public void AddSenses_WhenAddingSenses_ShouldAddThemWithoutDuplicates(Sense sense)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupSenses([sense, sense]);
            var senses = sut.Senses;

            // Assert
            Assert.Contains(sense, senses);
            Assert.Equal(1, senses.Count(s => s == sense));
        }

        [Theory]
        [MemberData(nameof(AllSenses))]
        public void AddSense_WhenAddingSense_ShouldAddItWithoutDuplicates(Sense sense)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupSense(sense);
            sut.SetupSense(sense);
            var senses = sut.Senses;

            // Assert
            Assert.Contains(sense, senses);
            Assert.Equal(1, senses.Count(s => s == sense));
        }

        [Theory]
        [MemberData(nameof(AllLanguages))]
        public void AddLanguages_WhenAddingLanguages_ShouldAddThemWithoutDuplicates(Language language)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupLanguages([language, language]);
            var languages = sut.Languages;

            // Assert
            Assert.Contains(language, languages);
            Assert.Equal(1, languages.Count(l => l == language));
        }

        [Theory]
        [MemberData(nameof(AllLanguages))]
        public void AddLanguage_WhenAddingLanguage_ShouldAddItWithoutDuplicates(Language language)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupLanguage(language);
            sut.SetupLanguage(language);
            var languages = sut.Languages;

            // Assert
            Assert.Contains(language, languages);
            Assert.Equal(1, languages.Count(l => l == language));
        }

        [Theory]
        [MemberData(nameof(AllSkills))]
        public void AddProficientSkills_WhenAddingProficientSkills_ShouldAddThemWithoutDuplicates(Skill skill)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupProficientSkills([skill, skill]);
            var proficientSkills = sut.ProficientSkills;

            // Assert
            Assert.Contains(skill, proficientSkills);
            Assert.Equal(1, proficientSkills.Count(s => s == skill));

        }

        [Theory]
        [MemberData(nameof(AllSkills))]
        public void AddProficientSkill_WhenAddingProficientSkill_ShouldAddItWithoutDuplicates(Skill skill)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupProficientSkill(skill);
            sut.SetupProficientSkill(skill);
            var proficientSkills = sut.ProficientSkills;

            // Assert
            Assert.Contains(skill, proficientSkills);
            Assert.Equal(1, proficientSkills.Count(s => s == skill));
        }

        [Theory]
        [MemberData(nameof(AllSkills))]
        public void AddExpertSkills_WhenAddingExpertSkills_ShouldAddThemWithoutDuplicates(Skill skill)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupExpertSkills([skill, skill]);
            var expertiseSkills = sut.ExpertSkills;

            // Assert
            Assert.Contains(skill, expertiseSkills);
            Assert.Equal(1, expertiseSkills.Count(s => s == skill));
        }

        [Theory]
        [MemberData(nameof(AllSkills))]
        public void AddExpertSkill_WhenAddingExpertSkill_ShouldAddItWithoutDuplicates(Skill skill)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupExpertSkill(skill);
            sut.SetupExpertSkill(skill);
            var expertiseSkills = sut.ExpertSkills;

            // Assert
            Assert.Contains(skill, expertiseSkills);
            Assert.Equal(1, expertiseSkills.Count(s => s == skill));
        }

        [Theory]
        [MemberData(nameof(AllAbilities))]
        public void AddProficentSavingThrows_WhenAddingProficientSavingThrows_ShouldAddThemWithoutDuplicates(Ability ability)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupProficientSavingThrows([ability, ability]);
            var proficientSavingThrows = sut.ProficientSavingThrows;

            // Assert
            Assert.Contains(ability, proficientSavingThrows);
            Assert.Equal(1, proficientSavingThrows.Count(a => a == ability));
        }

        [Theory]
        [MemberData(nameof(AllAbilities))]
        public void AddProficentSavingThrow_WhenAddingProficientSavingThrow_ShouldAddItWithoutDuplicates(Ability ability)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupProficientSavingThrow(ability);
            sut.SetupProficientSavingThrow(ability);
            var proficientSavingThrows = sut.ProficientSavingThrows;

            // Assert
            Assert.Contains(ability, proficientSavingThrows);
            Assert.Equal(1, proficientSavingThrows.Count(a => a == ability));
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void AddDamageImmunities_WhenAddingDamageImmunities_ShouldAddThemWithoutDuplicates(DamageType damageType)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupDamageImmunities([damageType, damageType]);
            var damageImmunities = sut.DamageImmunities;

            // Assert
            Assert.Contains(damageType, damageImmunities);
            Assert.Equal(1, damageImmunities.Count(d => d == damageType));

        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void AddDamageImmunity_WhenAddingDamageImmunity_ShouldAddItWithoutDuplicates(DamageType damageType)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupDamageImmunity(damageType);
            sut.SetupDamageImmunity(damageType);
            var damageImmunities = sut.DamageImmunities;

            // Assert
            Assert.Contains(damageType, damageImmunities);
            Assert.Equal(1, damageImmunities.Count(d => d == damageType));
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void AddDamageResistances_WhenAddingDamageResistances_ShouldAddThemWithoutDuplicates(DamageType damageType)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupDamageResistances([damageType, damageType]);
            var damageResistances = sut.DamageResistances;

            // Assert
            Assert.Contains(damageType, damageResistances);
            Assert.Equal(1, damageResistances.Count(d => d == damageType));
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void AddDamageResistance_WhenAddingDamageResistance_ShouldAddItWithoutDuplicates(DamageType damageType)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupDamageResistance(damageType);
            sut.SetupDamageResistance(damageType);
            var damageResistances = sut.DamageResistances;

            // Assert
            Assert.Contains(damageType, damageResistances);
            Assert.Equal(1, damageResistances.Count(d => d == damageType));
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void AddDamageVulnerabilities_WhenAddingDamageVulnerabilities_ShouldAddThemWithoutDuplicates(DamageType damageType)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupDamageVulnerabilities([damageType, damageType]);
            var damageVulnerabilities = sut.DamageVulnerabilities;

            // Assert
            Assert.Contains(damageType, damageVulnerabilities);
            Assert.Equal(1, damageVulnerabilities.Count(d => d == damageType));
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void AddDamageVulnerability_WhenAddingDamageVulnerability_ShouldAddItWithoutDuplicates(DamageType damageType)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupDamageVulnerability(damageType);
            sut.SetupDamageVulnerability(damageType);
            var damageVulnerabilities = sut.DamageVulnerabilities;

            // Assert
            Assert.Contains(damageType, damageVulnerabilities);
            Assert.Equal(1, damageVulnerabilities.Count(d => d == damageType));
        }


    }
}

