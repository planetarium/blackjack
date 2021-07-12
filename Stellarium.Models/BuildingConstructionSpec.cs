using System.Numerics;
using System.Collections.Immutable;
using Stellarium.Models.Resources;

namespace Stellarium.Models
{
    public sealed class BuildingConstructionSpec
    {
        public BuildingConstructionSpec(BigInteger requiredHeadquarterTier, IImmutableDictionary<Resource, BigInteger> requiredResources)
        {
            RequiredHeadquarterTier = requiredHeadquarterTier;
            RequiredResources = requiredResources;
        }

        public BigInteger RequiredHeadquarterTier { get; }

        public IImmutableDictionary<Resource, BigInteger> RequiredResources { get; }
    }
}
