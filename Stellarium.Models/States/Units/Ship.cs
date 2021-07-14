using Libplanet;
using Stellarium.Models.States.Ledgers;

namespace Stellarium.Models.States.Units
{
    /// <summary>
    /// Class representing a movable ship.
    /// </summary>
    public sealed class Ship : Unit
    {
        // FIXME: Placeholder.
        public readonly ulong FuelsPerTile = 1;

        public Ship(
            uint galaxyId,
            Address owner,
            Address address,
            Position position,
            Consumed consumed,
            Requirement requirement,
            PhysicalStat physicalStat,
            Energy energy)
            : base(galaxyId, owner, address, position, consumed, requirement, physicalStat, energy)
        {
        }

        public Ship Move(Position position)
        {
            return new Ship(GalaxyId, Owner, Address, position, Consumed, Requirement, PhysicalStat, Energy);
        }
    }
}
