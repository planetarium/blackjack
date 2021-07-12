using System.Numerics;
using System.Collections.Immutable;
using Libplanet.Assets;
using Stellarium.Models.Resources;

namespace Stellarium.Models
{
    public sealed class BuildingSpec
    {
        public BuildingSpec(
            BigInteger health,
            BigInteger shield,
            (Currency Resource, BigInteger Amount)? productionResource,
            IImmutableDictionary<Currency, BigInteger> carriableResources)
        {
            Health = health;
            Shield = shield;
            ProductionResource = productionResource;
            CarriableResources = carriableResources;
        }

        public BigInteger Health { get; }

        public BigInteger Shield { get; }

        // 건물 자원 생산량(?)
        // null 일 경우 생산하지 않는 건물.
        public (Currency Resource, BigInteger Amount)? ProductionResource { get; }

        public IImmutableDictionary<Currency, BigInteger> CarriableResources { get; }
    }
}
