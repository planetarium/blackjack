using System;
using Libplanet;
using Bencodex.Types;

namespace Stellarium.Models.States
{
    public sealed class Ship : Tile
    {
        // FIXME: Placeholder.
        public readonly ulong FuelsPerTile = 1;

        public Ship(
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


        public Ship Move(Position position)
        {
            return new Ship(GalaxyId, Owner, Address, position, Cost, Durability, Energy);
        }

        public Ship Attacked(long damage)
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
            return new Ship(GalaxyId, Owner, Address, Position, Cost, Durability, energy);
        }

        public Cost Cost { get; }
        public Durability Durability { get; }
        public Energy Energy { get; }
        public long Fuel { get; }

        public bool Destroyed => Energy.Health <= 0;

        //     public Ship(Bencodex.Types.Dictionary serialized)
        //         : base(serialized)
        //     {
        //         if (!((Text)serialized["_type"]).Equals((Text)"ship"))
        //         {
        //             throw new ArgumentException("Expected a ship.", nameof(serialized));
        //         }
        //         Energy = new Energy(
        //             (Integer)serialized["health"],
        //             (Integer)serialized["shield"]
        //         );
        //         Fuel = (Integer)serialized["fuel"];
        //     }


        //     public override Bencodex.Types.Dictionary Serialize() =>
        //         base.Serialize()
        //             .Add("_type", nameof(Ship))
        //             .Add("cost", Cost.Serialize())
        //             .Add("durability", Durability.Serialize())
        //             .Add("energy", Energy.Serialize())
        //             .Add("fuel", Fuel);
        // }

    }
}
