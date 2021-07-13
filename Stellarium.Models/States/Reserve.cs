using Libplanet;
using Libplanet.Assets;
using Bencodex.Types;

namespace Stellarium.Models.States
{
    public sealed class Reserve : Tile
    {
        public Reserve(
            uint galaxyId,
            Address owner,
            Address address,
            Position position,
            Currency currency
        )
            : base(galaxyId, owner, address, position)
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
