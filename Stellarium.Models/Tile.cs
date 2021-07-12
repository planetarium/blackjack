using System;

namespace Stellarium.Models
{
    public abstract partial class Tile
    {
        public uint GalaxyId { get; }
        public Position Position;

        public Tile(uint galaxyId, Position position)
        {
            GalaxyId = galaxyId;
            Position = position;
        }

        public int Distance(Tile other)
        {
            return Position.Distance(other.Position);
        }
    }
}
