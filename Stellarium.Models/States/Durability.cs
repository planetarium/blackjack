using Bencodex.Types;
using System.Numerics;
using System.Collections.Immutable;
using Libplanet.Assets;

namespace Stellarium.Models.States
{
    public class Durability
    {
        public Durability(long maxHealth, long maxShield)
        {
            MaxHealth = maxHealth;
            MaxShield = maxShield;
        }

        public long MaxHealth { get; }

        public long MaxShield { get; }

        public Bencodex.Types.Dictionary Serialize() =>
            Bencodex.Types.Dictionary.Empty
                .Add("maxHealth", MaxHealth)
                .Add("maxShield", MaxShield);
    }
}
