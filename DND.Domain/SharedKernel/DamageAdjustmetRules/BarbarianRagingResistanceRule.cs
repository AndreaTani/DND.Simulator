namespace DND.Domain.SharedKernel
{
    public class BarbarianRagingResistanceRule : IModificationRule
    {
        public DamageType TypeOfDamage { get; protected set; }

        public string Name => "BarbarianRage Resistance";

        public DamageType GetDamageType()
        {
            return TypeOfDamage;
        }

        public float GetModificationFactor(DamageType damageType, DamageSource damageSource = DamageSource.Mundane, bool isSilvered = false)
        {
            if ((damageType == DamageType.Bludgeoning ||
                 damageType == DamageType.Piercing ||
                 damageType == DamageType.Slashing))
            {
                TypeOfDamage = damageType;
                return 0.5f;
            }

            return 1.0f;
        }

    }
}
