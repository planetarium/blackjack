using System;
using Libplanet;

namespace Stellarium.Models.States
{
    public abstract partial class Tile
    {
        public uint GalaxyId { get; }

        public Tile(uint galaxyId, Address owner, Address address, Position position)
        {
            GalaxyId = galaxyId;
            Owner = owner;
            Address = address;
            Position = position;
        }

        public Position Position { get; }

        public Address Address { get; }

        public Address Owner { get; }

        public int Distance(Tile other)
        {
            return Position.Distance(other.Position);
        }
    }
}
