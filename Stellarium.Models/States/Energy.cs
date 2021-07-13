using Bencodex.Types;
using System.Numerics;
using System.Collections.Immutable;
using Libplanet.Assets;

namespace Stellarium.Models.States
{
    public class Energy
    {
        public Energy(long health, long shield)
        {
            Health = health;
            Shield = shield;
        }

        public long Health { get; }

        public long Shield { get; }

        public Energy(Bencodex.Types.Dictionary serialized)
        {

        }

        public Bencodex.Types.Dictionary Serialize() =>
            Bencodex.Types.Dictionary.Empty
                .Add("Health", Health)
                .Add("Shield", Shield);
    }
}
