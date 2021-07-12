using Bencodex.Types;
using Libplanet.Assets;

namespace Stellarium.Models
{
    public sealed class Reserve : Tile
    {
        public Reserve(
            uint galaxyId,
            Position position,
            Currency currency
        )
            : base(galaxyId, position)
        {
            Currency = currency;
        }

        public Reserve(Bencodex.Types.Dictionary serialized)
            : base(serialized)
        {
            Currency = new Currency(serialized["currency"]);
        }

        public Currency Currency { get; }

        public override Bencodex.Types.Dictionary Serialize() => base.Serialize()
            .Add("currency", Currency.Serialize());
    }
}
