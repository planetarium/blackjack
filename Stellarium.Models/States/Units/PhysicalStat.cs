using Bencodex.Types;

namespace Stellarium.Models.States.Units
{
    public sealed class PhysicalStat
    {
        public PhysicalStat(long maxHealth, long maxShield, long attackPower)
        {
            MaxHealth = maxHealth;
            MaxShield = maxShield;
            AttackPower = attackPower;
        }

        public long MaxHealth { get; }

        public long MaxShield { get; }

        public long AttackPower { get; }

        public PhysicalStat(Bencodex.Types.Dictionary serialized)
            : this(
                (Integer)serialized["mexHealth"],
                (Integer)serialized["maxShield"],
                (Integer)serialized["attackPower"])
        {
        }

        public Bencodex.Types.Dictionary Serialize()
        {
            return Bencodex.Types.Dictionary.Empty
                .Add("_type", nameof(PhysicalStat))
                .Add("maxHealth", MaxHealth)
                .Add("maxShield", MaxShield)
                .Add("attackPower", AttackPower);
        }
    }
}
