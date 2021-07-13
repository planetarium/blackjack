using System;
using System.Numerics;
using System.Collections.Immutable;
using System.Linq;
using Libplanet;
using Libplanet.Assets;

namespace Stellarium.Models.States
{
    public class Building : Tile
    {
        public Building(
            uint galaxyId,
            Address owner,
            Address address,
            Position position,
            Cost cost,
            Durability durability,
            Energy energy)
            : base(galaxyId, owner, address, position)
        {
            Cost = cost;
            Durability = durability;
            Energy = energy;
        }

        public Building Attacked(long damage)
        {
            long health = Energy.Health;
            long shield = Energy.Shield;
            if (damage >= shield + health)
            {
                shield = 0;
                health = 0;
            }
            else if (damage >= shield)
            {
                health -= damage - shield;
                shield = 0;
            }
            else
            {
                shield -= damage;
            }

            Energy energy = new Energy(health, shield);
            return new Building(GalaxyId, Owner, Address, Position, Cost, Durability, energy);
        }

        public Cost Cost { get; }
        public Durability Durability { get; }
        public Energy Energy { get; }
        // FIXME: Placeholder.
        public bool BuildComplete => true;
        public bool Destroyed => Energy.Health <= 0;
    }
}
