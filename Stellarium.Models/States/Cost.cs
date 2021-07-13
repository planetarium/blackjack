using Bencodex.Types;
using Libplanet.Assets;
using System.Collections.Immutable;
using System.Numerics;
using System.Linq;

namespace Stellarium.Models.States
{
    public sealed class Cost
    {
        public Cost(
            long requiredHQTier,
            IImmutableDictionary<Currency, BigInteger> requiredResources)
        {
            RequiredHQTier = requiredHQTier;
            RequiredResources = requiredResources;
        }

        public long RequiredHQTier { get; }

        public IImmutableDictionary<Currency, BigInteger> RequiredResources { get; }

        // FIXME: Placeholder.
        public Bencodex.Types.Dictionary Serialize() =>
            Bencodex.Types.Dictionary.Empty
                .Add("requiredHQTier", RequiredHQTier);
    }
}
