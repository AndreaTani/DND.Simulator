using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public partial class CreatureTest
    {
        // Test data for a fighter's ability scores
        private static readonly Dictionary<Ability, int> fighterScores = new Dictionary<Ability, int>
        {
            { Ability.Strength, 18 },
            { Ability.Dexterity, 12 },
            { Ability.Constitution, 16 },
            { Ability.Intelligence, 10 },
            { Ability.Wisdom, 8 },
            { Ability.Charisma, 8 }
        };

        // Test data for all damage types
        public static TheoryData<DamageType> AllDamageTypes => [.. Enum.GetValues(typeof(DamageType)).Cast<DamageType>().ToArray()];

        // Test data for all rules with damage types and rule names
        public static TheoryData<DamageType, string, string, string> DamageTypeRuleNames
        {
            get
            {
                var data = new TheoryData<DamageType, string, string, string>();
                foreach (var dt in Enum.GetValues(typeof(DamageType)).Cast<DamageType>())
                {
                    data.Add(dt, $"Simple Resistance {dt}", $"Simple Vulnerability {dt}", $"Simple Immunity {dt}");
                }
                return data;
            }
        }


        [Theory]
        [InlineData(DamageType.Slashing, 14, 14)]
        [InlineData(DamageType.Fire, 0, 14)]
        [InlineData(DamageType.Acid, 7, 14)]
        [InlineData(DamageType.Cold, 28, 14)]
        public void CalculateFinalDamageOnLoneFighter_WhenDamage_ReturnCorrectDamage(DamageType damageType, int expectedDamage, int baseDamage)
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

            sut.SetupDamageImmunity(DamageType.Fire);
            sut.SetupResistance(DamageType.Acid);
            sut.SetupVulnerability(DamageType.Cold);

            var damageSource = DamageSource.Mundane;
            bool isSilvered = false;

            // Act
            int finalDamage = sut.CalculateFinalDamage(baseDamage, damageType, damageSource, isSilvered);

            // Assert
            Assert.Equal(expectedDamage, finalDamage);
            Assert.True(sut.IsImmuneTo(DamageType.Fire));
            Assert.True(sut.IsResistantTo(DamageType.Acid));
            Assert.True(sut.IsVulnerableTo(DamageType.Cold));
            Assert.False(sut.IsImmuneTo(DamageType.Acid));
            Assert.False(sut.IsResistantTo(DamageType.Cold));
            Assert.False(sut.IsVulnerableTo(DamageType.Fire));
        }

        [Theory]
        [InlineData(DamageType.Slashing, DamageSource.Mundane, false, 5, 10)]
        [InlineData(DamageType.Bludgeoning, DamageSource.Mundane, false, 5, 10)]
        [InlineData(DamageType.Piercing, DamageSource.Mundane, false, 5, 10)]
        [InlineData(DamageType.Slashing, DamageSource.Magical, false, 10, 10)]
        [InlineData(DamageType.Bludgeoning, DamageSource.Magical, false, 10, 10)]
        [InlineData(DamageType.Piercing, DamageSource.Magical, false, 10, 10)]
        [InlineData(DamageType.Slashing, DamageSource.Natural, false, 5, 10)]
        [InlineData(DamageType.Bludgeoning, DamageSource.Natural, false, 5, 10)]
        [InlineData(DamageType.Piercing, DamageSource.Natural, false, 5, 10)]
        [InlineData(DamageType.Slashing, DamageSource.Mundane, true, 10, 10)]
        [InlineData(DamageType.Bludgeoning, DamageSource.Mundane, true, 10, 10)]
        [InlineData(DamageType.Piercing, DamageSource.Mundane, true, 10, 10)]
        [InlineData(DamageType.Slashing, DamageSource.Magical, true, 10, 10)]
        [InlineData(DamageType.Bludgeoning, DamageSource.Magical, true, 10, 10)]
        [InlineData(DamageType.Piercing, DamageSource.Magical, true, 10, 10)]
        [InlineData(DamageType.Slashing, DamageSource.Natural, true, 10, 10)]
        [InlineData(DamageType.Bludgeoning, DamageSource.Natural, true, 10, 10)]
        [InlineData(DamageType.Piercing, DamageSource.Natural, true, 10, 10)]
        [InlineData(DamageType.Cold, DamageSource.Mundane, false, 10, 10)]
        [InlineData(DamageType.Cold, DamageSource.Natural, false, 10, 10)]
        public void CalculateFinalDamageOnLoneFighter_WhenWerewolfStyleRuleApplies_ReturnCorrectDamage(DamageType damageType, DamageSource damageSource, bool isSilvered, int expectedDamage, int baseDamage)
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

            sut.SetupSpecialRule(new PhysicalDamageNonMagicalNonSilveredResistanceRule());

            // Act
            int finalDamage = sut.CalculateFinalDamage(baseDamage, damageType, damageSource, isSilvered);

            // Assert
            Assert.Equal(expectedDamage, finalDamage);
        }

        [Theory]
        [MemberData(nameof(DamageTypeRuleNames))]
        public void AddDamageImmunity_WhenAddingImmunity_RemovesResistanceAndVulnerabilityOfTheSameDamageType(DamageType type, string resistanceName, string vulnerabilityName, string immunityName)
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

            sut.SetupResistance(type);
            bool InitialIsResistant = sut.IsResistantTo(type);
            List<DamageType> InitialResistances = [.. sut.DamageResistances];
            List<IDamageAdjustmentRule> InitialResistanceAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<SimpleDamageResisistanceRule>().Where(rule => rule.GetDamageType().Equals(type))];

            sut.SetupVulnerability(type);
            bool InitialIsVulnerable = sut.IsVulnerableTo(type);
            List<DamageType> InitialVulnerabilities = [.. sut.DamageVulnerabilities];

            List<IDamageAdjustmentRule> InitialVulnerabilityAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<SimpleDamageVulnerabilityRule>().Where(rule => rule.GetDamageType().Equals(type))];


            // Act
            sut.SetupDamageImmunity(type);
            bool CurrentIsResistant = sut.IsResistantTo(type);
            bool CurrentIsVulnerable = sut.IsVulnerableTo(type);
            bool IsImmune = sut.IsImmuneTo(type);
            var Immunities = sut.DamageImmunities;
            var ImmunityRules = sut.DamageAdjustmentRules.OfType<IImmunityRule>().Where(rule => rule.GetDamageType() == type);
            List<DamageType> CurrentResistances = [.. sut.DamageResistances];
            List<DamageType> CurrentVulnerabilities = [.. sut.DamageVulnerabilities];
            List<IDamageAdjustmentRule> CurrentResistanceAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<SimpleDamageResisistanceRule>().Where(rule => rule.GetDamageType().Equals(type))];
            List<IDamageAdjustmentRule> CurrentVulnerabilityAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<SimpleDamageVulnerabilityRule>().Where(rule => rule.GetDamageType().Equals(type))];

            // Assert
            Assert.Empty(CurrentResistances);
            Assert.Empty(CurrentVulnerabilities);
            Assert.NotEqual(InitialResistances, CurrentResistances);
            Assert.NotEqual(InitialVulnerabilities, CurrentVulnerabilities);
            Assert.Contains(Immunities, item => item.Equals(type));
            Assert.DoesNotContain(CurrentResistances, item => item.Equals(type));
            Assert.DoesNotContain(CurrentVulnerabilities, item => item.Equals(type));
            Assert.NotEqual(InitialResistanceAdjustmetRules, CurrentResistanceAdjustmetRules);
            Assert.NotEqual(InitialVulnerabilityAdjustmetRules, CurrentVulnerabilityAdjustmetRules);
            Assert.Contains(ImmunityRules, item => item.Name == immunityName);
            Assert.Single(ImmunityRules);
            Assert.Contains(InitialResistanceAdjustmetRules, item => item.Name == resistanceName);
            Assert.Contains(InitialVulnerabilityAdjustmetRules, item => item.Name == vulnerabilityName);
            Assert.DoesNotContain(CurrentResistanceAdjustmetRules, item => item.Name == resistanceName);
            Assert.DoesNotContain(CurrentVulnerabilityAdjustmetRules, item => item.Name == vulnerabilityName);
            Assert.True(InitialIsResistant);
            Assert.True(InitialIsVulnerable);
            Assert.False(CurrentIsResistant);
            Assert.False(CurrentIsVulnerable);
            Assert.True(IsImmune);
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void AddDamageResistance_WhenAddingResistance_RemoveImmunityOfTHeSamaDamageType(DamageType damageType)
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

            sut.SetupDamageImmunity(damageType);
            bool InitialIsImmune = sut.IsImmuneTo(damageType);
            List<DamageType> InitialImmunities = [.. sut.DamageImmunities];
            List<IDamageAdjustmentRule> InitialImmunityAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<IImmunityRule>().Where(rule => rule.GetDamageType().Equals(damageType))];

            // Act
            sut.SetupResistance(damageType);
            bool CurrentIsImmune = sut.IsImmuneTo(damageType);
            var currentImmunities = sut.DamageImmunities;
            List<DamageType> CurrentImmunities = [.. sut.DamageImmunities];
            List<IDamageAdjustmentRule> CurrentImmunityAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<IImmunityRule>().Where(rule => rule.GetDamageType().Equals(damageType))];
            bool IsResistant = sut.IsResistantTo(damageType);
            List<DamageType> resistances = [.. sut.DamageResistances];
            List<IDamageAdjustmentRule> ResistanceAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<SimpleDamageResisistanceRule>().Where(rule => rule.GetDamageType().Equals(damageType))];

            // Assert
            Assert.Empty(CurrentImmunities);
            Assert.NotEqual(InitialImmunities, CurrentImmunities);
            Assert.DoesNotContain(CurrentImmunities, item => item.Equals(damageType));
            Assert.DoesNotContain(CurrentImmunityAdjustmetRules, item => item.GetDamageType().Equals(damageType));
            Assert.False(CurrentIsImmune);
            Assert.True(IsResistant);
            Assert.Contains(resistances, item => item.Equals(damageType));
            Assert.Contains(ResistanceAdjustmetRules, item => item.GetDamageType().Equals(damageType));
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void AddDamageVulnerability_WhenAddingVulnerability_RemoveImmunityOfTheSamaDamageType(DamageType damageType)
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

            sut.SetupDamageImmunity(damageType);
            bool InitialIsImmune = sut.IsImmuneTo(damageType);
            List<DamageType> InitialImmunities = [.. sut.DamageImmunities];
            List<IDamageAdjustmentRule> InitialImmunityAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<IImmunityRule>().Where(rule => rule.GetDamageType().Equals(damageType))];

            // Act
            sut.SetupVulnerability(damageType);
            bool CurrentIsImmune = sut.IsImmuneTo(damageType);
            var currentImmunities = sut.DamageImmunities;
            List<DamageType> CurrentImmunities = [.. sut.DamageImmunities];
            List<IDamageAdjustmentRule> CurrentImmunityAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<IImmunityRule>().Where(rule => rule.GetDamageType().Equals(damageType))];
            bool IsVulnerabile = sut.IsVulnerableTo(damageType);
            List<DamageType> vulnerabilities = [.. sut.DamageVulnerabilities];
            List<IDamageAdjustmentRule> VulnerabilityAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<SimpleDamageVulnerabilityRule>().Where(rule => rule.GetDamageType().Equals(damageType))];

            // Assert
            Assert.Empty(CurrentImmunities);
            Assert.NotEqual(InitialImmunities, CurrentImmunities);
            Assert.DoesNotContain(CurrentImmunities, item => item.Equals(damageType));
            Assert.DoesNotContain(CurrentImmunityAdjustmetRules, item => item.GetDamageType().Equals(damageType));
            Assert.False(CurrentIsImmune);
            Assert.True(IsVulnerabile);
            Assert.Contains(vulnerabilities, item => item.Equals(damageType));
            Assert.Contains(VulnerabilityAdjustmetRules, item => item.GetDamageType().Equals(damageType));
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void AddDamageResistance_WhenVulnerableAddingResistance_DoesntRemoveVulnerabilityOfTheSamaDamageType(DamageType damageType)
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

            sut.SetupVulnerability(damageType);

            bool InitialIsVulnerable = sut.IsVulnerableTo(damageType);
            List<DamageType> InitialVulnerabilities = [.. sut.DamageVulnerabilities];
            List<IDamageAdjustmentRule> InitialVulneraqbilityAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<SimpleDamageVulnerabilityRule>().Where(rule => rule.GetDamageType().Equals(damageType))];

            // Act
            sut.SetupResistance(damageType);

            bool CurrentIsVulnerable = sut.IsVulnerableTo(damageType);
            List<DamageType> CurrentVulnerabilities = [.. sut.DamageVulnerabilities];
            List<IDamageAdjustmentRule> CurrentVulneraqbilityAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<SimpleDamageVulnerabilityRule>().Where(rule => rule.GetDamageType().Equals(damageType))];

            bool IsResistant = sut.IsResistantTo(damageType);
            List<DamageType> resistances = [.. sut.DamageResistances];
            List<IDamageAdjustmentRule> ResistanceAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<SimpleDamageResisistanceRule>().Where(rule => rule.GetDamageType().Equals(damageType))];

            // Assert
            Assert.True(InitialIsVulnerable);
            Assert.True(CurrentIsVulnerable);
            Assert.Contains(InitialVulnerabilities, item => item.Equals(damageType));   
            Assert.Contains(CurrentVulnerabilities, item => item.Equals(damageType));
            Assert.Contains(InitialVulneraqbilityAdjustmetRules, item => item.GetDamageType().Equals(damageType));
            Assert.Contains(CurrentVulneraqbilityAdjustmetRules, item => item.GetDamageType().Equals(damageType));
            Assert.True(IsResistant);
            Assert.Contains(resistances, item => item.Equals(damageType));
            Assert.Contains(ResistanceAdjustmetRules, item => item.GetDamageType().Equals(damageType));
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void AddDamageVulnerability_WhenResistantAddingVulnerability_DoesntRemoveReisstanceOfTheSamaDamageType(DamageType damageType)
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
            
            sut.SetupResistance(damageType);

            bool InitialIsResistant = sut.IsResistantTo(damageType);
            List<DamageType> InitialResistances = [.. sut.DamageResistances];
            List<IDamageAdjustmentRule> InitialResistanceAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<SimpleDamageResisistanceRule>().Where(rule => rule.GetDamageType().Equals(damageType))];

            // Act
            sut.SetupVulnerability(damageType);

            bool CurrentIsResistant = sut.IsResistantTo(damageType);
            List<DamageType> CurrentResistances = [.. sut.DamageResistances];
            List<IDamageAdjustmentRule> CurrentResistanceAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<SimpleDamageResisistanceRule>().Where(rule => rule.GetDamageType().Equals(damageType))];

            bool IsVulnerabile = sut.IsVulnerableTo(damageType);
            List<DamageType> vulnerabilities = [.. sut.DamageVulnerabilities];
            List<IDamageAdjustmentRule> VulnerabilityAdjustmetRules = [.. sut.DamageAdjustmentRules.OfType<SimpleDamageVulnerabilityRule>().Where(rule => rule.GetDamageType().Equals(damageType))];

            // Assert
            Assert.True(InitialIsResistant);
            Assert.True(CurrentIsResistant);
            Assert.Contains(InitialResistances, item => item.Equals(damageType));
            Assert.Contains(CurrentResistances, item => item.Equals(damageType));
            Assert.Contains(InitialResistanceAdjustmetRules, item => item.GetDamageType().Equals(damageType));
            Assert.Contains(CurrentResistanceAdjustmetRules, item => item.GetDamageType().Equals(damageType));
            Assert.True(IsVulnerabile);
            Assert.Contains(vulnerabilities, item => item.Equals(damageType));
            Assert.Contains(VulnerabilityAdjustmetRules, item => item.GetDamageType().Equals(damageType));
        }

        [Theory]
        [InlineData(49, 15, 10, 49, 5)]
        [InlineData(49, 15, 15, 49, 0)]
        [InlineData(49, 15, 20, 44, 0)]
        public void TakeDamage_WhenCreatureHasTemporaryHitPoints_ReduceTemporaryHitPointsFirst(int assignedHitPoints, int addedTemporatyHitpoints, int damageTaken, int expectedHitPoints, int expectedTemporaryHitpoints)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: assignedHitPoints,
                currentHitPoints: assignedHitPoints,
                speed: new Speed(),
                level: 5
                );

            sut.AddTemporaryHitPoints(addedTemporatyHitpoints);

            int initialHitPoints = sut.CurrentHitPoints;
            int initialTemporaryHitPoints = sut.TemporaryHitPoints;

            // Act
            sut.TakeDamage(damageTaken, DamageType.Bludgeoning, DamageSource.Mundane, false);

            int currentHitPoints = sut.CurrentHitPoints;
            int currentTemporaryHitPoints = sut.TemporaryHitPoints;

            // Assert
            Assert.Equal(expectedHitPoints, currentHitPoints);
            Assert.Equal(expectedTemporaryHitpoints, currentTemporaryHitPoints);

        }

        [Theory]
        [InlineData(49, 20, 29)]
        [InlineData(39, 20, 19)]
        [InlineData(157, 38, 119)]
        [InlineData(50, 60, -10)]
        public void TakeDamage_WhenBasicDamage_ReduceHitPoints(int assignedHitPoints, int damageTaken, int expectedHitPoints)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: assignedHitPoints,
                currentHitPoints: assignedHitPoints,
                speed: new Speed(),
                level: 5
                );

            int initialHitPoints = sut.CurrentHitPoints;

            // Act
            sut.TakeDamage(damageTaken, DamageType.Bludgeoning, DamageSource.Mundane, false);
            int currentHitPoints = sut.CurrentHitPoints;

            // Assert
            Assert.Equal(initialHitPoints, assignedHitPoints);
            Assert.Equal(expectedHitPoints, currentHitPoints);

        }

        [Fact]
        public void TakeDamage_WhenBasicDamage_DamageApplyAndEventIsTriggered()
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 50,
                currentHitPoints: 50,
                speed: new Speed(),
                level: 5
                );

            int initialHitPoints = sut.CurrentHitPoints;

            // Act
            sut.TakeDamage(40, DamageType.Bludgeoning, DamageSource.Mundane, false);
            var currentHitPoints = sut.CurrentHitPoints;
            var eventType = sut.DomainEvents.OfType<CreatureHPChangedEvent>().FirstOrDefault();

            // Assert
            Assert.True(currentHitPoints < initialHitPoints);
            Assert.NotNull(eventType);
        }

        [Theory]
        [InlineData(50, 20, 20)]
        [InlineData(50, 20, 30)]
        [InlineData(50, 20, 40)]
        public void Heal_WhenBasicHeal_IncreaseHitPointsUntilMaxHitPoints(int maxHitPoints, int assignedHitPoints, int healPOints)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: maxHitPoints,
                currentHitPoints: assignedHitPoints,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.Heal(healPOints);
            int currentHitPoints = sut.CurrentHitPoints;

            // Assert
            Assert.True(currentHitPoints > assignedHitPoints);
            Assert.True(currentHitPoints <= maxHitPoints);

        }

        [Fact]
        public void Heal_WhenBasicHeal_HealAppliesAndEventTriggers()
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 50,
                currentHitPoints: 20,
                speed: new Speed(),
                level: 5
                );

            int assignedHitPoints = sut.CurrentHitPoints;

            // Act
            sut.Heal(14);
            int currentHitPoints = sut.CurrentHitPoints;
            var eventType = sut.DomainEvents.OfType<CreatureHPChangedEvent>().FirstOrDefault();

            // Assert
            Assert.True(currentHitPoints > assignedHitPoints);
            Assert.NotNull(eventType);
        }

        [Theory]
        [InlineData(10, 10)]
        public void CalculateFinalDamage_WhenCreatureIsBothResistantAndVulnerable_DamageCancelsToNormal(int expectedDamage, int baseDamage)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 50,
                currentHitPoints: 20,
                speed: new Speed(),
                level: 5
                );

            sut.SetupResistance(DamageType.Fire);
            sut.SetupVulnerability(DamageType.Fire);

            // Act
            int finalDamage = sut.CalculateFinalDamage(baseDamage, DamageType.Fire, DamageSource.Magical, false);

            // Assert
            Assert.Equal(expectedDamage, finalDamage);
            Assert.True(sut.IsResistantTo(DamageType.Fire));
            Assert.True(sut.IsVulnerableTo(DamageType.Fire));

        }

        [Theory]
        [InlineData(10, 10)]
        public void CalculateFinalDamage_WhenBarbarianRageResistanceConflictsWithSimpleVulnerability_TheyCancelOut(int expectedDamage, int baseDamage)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 50,
                currentHitPoints: 30,
                speed: new Speed(),
                level: 5
                );

            sut.SetupSpecialRule(new BarbarianRagingResistanceRule());
            sut.SetupVulnerability(DamageType.Slashing);

            // Act
            int finalDamage = sut.CalculateFinalDamage(baseDamage, DamageType.Slashing, DamageSource.Mundane, false);

            // Assert
            Assert.Equal(expectedDamage, finalDamage);
        }

        [Theory]
        [InlineData(5, 10, DamageType.Fire, 0.5f)]
        public void CalculateFinalDamage_WhenTempResistanceOverridesPermanentVulnerability_ResistanceWins(int expectedDamage, int baseDamage, DamageType damageType, float tempModifier)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 50,
                currentHitPoints: 30,
                speed: new Speed(),
                level: 5
                );

            sut.SetupVulnerability(damageType);
            var temporaryResistance = new TemporaryDamageModification(damageType, tempModifier, new Guid(), 47, ExpirationType.AtTheBeginning);
            sut.ApplyTemporaryDamageModification(temporaryResistance);

            // Act
            int finalDamage = sut.CalculateFinalDamage(baseDamage, damageType, DamageSource.Magical, false);

            // Assert
            Assert.True(sut.IsVulnerableTo(damageType));
            Assert.Equal(expectedDamage, finalDamage);
        }

        [Theory]
        [InlineData(20, 10, DamageType.Fire, 2f)]
        public void CalculateFinalDamage_WhenTempVulnmerabilityOverridesPermanentResistance_VulnerabilityWins(int expectedDamage, int baseDamage, DamageType damageType, float tempModifier)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 50,
                currentHitPoints: 30,
                speed: new Speed(),
                level: 5
                );

            sut.SetupResistance(damageType);
            var temporaryResistance = new TemporaryDamageModification(damageType, tempModifier, new Guid(), 47, ExpirationType.AtTheBeginning);
            sut.ApplyTemporaryDamageModification(temporaryResistance);

            // Act
            int finalDamage = sut.CalculateFinalDamage(baseDamage, damageType, DamageSource.Magical, false);

            // Assert
            Assert.True(sut.IsResistantTo(damageType));
            Assert.Equal(expectedDamage, finalDamage);
        }

        [Theory]
        [InlineData(50, 20, DamageType.Acid, 40)]
        public void TakeDamage_WhenCreatureIsResistant_AppliesCalculatedDamageToHitPoints(int initialHitPoints, int baseDamage, DamageType damageType, int expectedFinalHitPoints)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores), // Assuming fighterScores is available
                maxHitPoints: initialHitPoints,
                currentHitPoints: initialHitPoints,
                speed: new Speed(),
                level: 5
            );

            sut.SetupResistance(DamageType.Acid);

            // Act
            sut.TakeDamage(baseDamage, damageType, DamageSource.Magical, false);
            int currentHitPoints = sut.CurrentHitPoints;

            // Assert
            Assert.True(sut.IsResistantTo(DamageType.Acid));
            Assert.Equal(expectedFinalHitPoints, currentHitPoints);
        }
    }
}
