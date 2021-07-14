using Bencodex.Types;
using Libplanet.Assets;
using System.Numerics;

namespace Stellarium.Models.States.Ledgers
{
    public sealed class Quantity
    {
        public Quantity(Currency resource, BigInteger amount)
        {
            Resource = resource;
            Amount = amount;
        }

        public Currency Resource { get; }
        public BigInteger Amount { get; }

        public Quantity(Bencodex.Types.Dictionary serialized)
            : this(
                new Currency(serialized["resource"]),
                (Integer)serialized["amount"])
        {
        }

        public Bencodex.Types.Dictionary Serialize()
        {
            return Bencodex.Types.Dictionary.Empty
                .Add("_type", nameof(Quantity))
                .Add("resource", Resource.Serialize())
                .Add("amount", (long)Amount);
        }
    }
}
