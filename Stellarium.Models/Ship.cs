using System;
using Bencodex.Types;

namespace Stellarium.Models
{
    public sealed class Ship : Tile
    {
        // FIXME: Placeholder.
        public readonly ulong FuelsPerTile = 1;

        public Ship(
            uint galaxyId,
            Position position,
            ulong health,
            ulong fuel
        ) : base(galaxyId, position)
        {
            Health = health;
            Fuel = fuel;
        }

        public Ship(Bencodex.Types.Dictionary serialized)
            : base(serialized)
        {
            if (!((Text)serialized["_type"]).Equals((Text)"ship"))
            {
                throw new ArgumentException("Expected a ship.", nameof(serialized));
            }
            Health = (Integer)serialized["health"];
            Fuel = (Integer)serialized["fuel"];
        }

        public ulong Health { get; }

        // public BigInteger Cargo { get; }

        public ulong Fuel { get; }

        public Ship Move(Position position)
        {
            return new Ship(GalaxyId, position, Health, Fuel);
        }

        public Ship Hit(ulong damage)
        {
            return new Ship(GalaxyId, Position, Health - damage, Fuel);
        }

        public bool Destroyed => Health <= 0;

        public override Bencodex.Types.Dictionary Serialize() =>
            base.Serialize()
                .Add("_type", nameof(Ship))
                .Add("health", Health)
                .Add("fuel", Fuel);
    }
}
