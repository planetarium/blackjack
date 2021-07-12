using System;
using System.Numerics;
using System.Collections.Immutable;
using Stellarium.Models.Resources;
using Libplanet.Assets;

namespace Stellarium.Models
{
    public sealed class Building : Tile
    {
        public Building(uint galaxyId, Position position, BuildingSpec spec, IImmutableDictionary<Currency, BigInteger> storage)
            : base(galaxyId, position)
        {
            Spec = spec;
            Storage = storage;
        }

        public BuildingSpec Spec { get; }

        public IImmutableDictionary<Currency, BigInteger> Storage { get; }

        public Building Attacked(BigInteger strikingPower)
        {
            BigInteger health = Spec.Health, shield = Spec.Shield;
            if (strikingPower >= shield + health)
            {
                shield = 0;
                health = 0;
            }
            else if (strikingPower >= shield)
            {
                health -= strikingPower - shield;
                shield = 0;
            }
            else
            {
                shield -= strikingPower;
            }

            BuildingSpec spec = new BuildingSpec(
                health,
                shield,
                Spec.ProductionResource,
                Spec.CarriableResources);

            return new Building(GalaxyId, Position, spec, Storage);
        }
    }
}
