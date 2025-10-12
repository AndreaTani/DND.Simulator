using DND.Domain.SharedKernel.Enum;

namespace DND.Domain.SharedKernel.ValueObjects
{
    public class AbilityScore
    {
        // Map of {Ability type, scores}
        public IReadOnlyDictionary<AbilityType, int> Scores { get; private set; }

        // Method for computing modifier in D&D
        public int GetModifier(AbilityType ability)
        {
            // Logic: (Score - 10) / 2, rounded down
            if (Scores.TryGetValue(ability, out int score))
            {
                return (score - 10) / 2;
            }
            return 0;
        }

        // Constructor, initiazlizes all scores to 10
        public AbilityScore()
        {
            Scores = new Dictionary<AbilityType, int>
            {
                { AbilityType.Strength, 10 },
                { AbilityType.Dexterity, 10 },
                { AbilityType.Constitution, 10 },
                { AbilityType.Intelligence, 10 },
                { AbilityType.Wisdom, 10 },
                { AbilityType.Charisma, 10 }
            };
        }

        // Constructor that accepts a dictionary of scores
        // if some ability types are missing, assumes 10 as default
        public AbilityScore(Dictionary<AbilityType, int> scores)
        {
            var defaultScores = new Dictionary<AbilityType, int>
            {
                { AbilityType.Strength, 10 },
                { AbilityType.Dexterity, 10 },
                { AbilityType.Constitution, 10 },
                { AbilityType.Intelligence, 10 },
                { AbilityType.Wisdom, 10 },
                { AbilityType.Charisma, 10 }
            };

            foreach (var ability in defaultScores.Keys)
            {
                if (scores.TryGetValue(ability, out int value))
                {
                    defaultScores[ability] = value;
                }
            }

            Scores = defaultScores;
        }

        // Method to update a specific score, returns true if the update was successful
        public bool UpdateScore(AbilityType ability, int newScore)
        {
            if (newScore < 1 || newScore > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(newScore), "Ability score must be between 1 and 20.");
            }
            var scoresCopy = Scores.ToDictionary(entry => entry.Key, entry => entry.Value);
            scoresCopy[ability] = newScore;
            Scores = scoresCopy;
            return true;
        }
    }
}
