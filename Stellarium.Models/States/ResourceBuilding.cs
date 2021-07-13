using System.Collections.Immutable;
using System.Numerics;
using Libplanet;
using Libplanet.Assets;

namespace Stellarium.Models.States
{
    public sealed class ResourceBuilding : Building
    {
        public ResourceBuilding(
            uint galaxyId,
            Address owner,
            Address address,
            Position position,
            Cost cost,
            Durability durability,
            Energy energy,
            IImmutableDictionary<Currency, BigInteger> storage)
            : base(galaxyId, owner, address, position, cost, durability, energy)
        {
        }
    }
}
