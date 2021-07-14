using Libplanet;
using Stellarium.Models.States.Ledgers;

namespace Stellarium.Models.States.Units
{
    public abstract class Structure : Unit
    {
        public Structure(
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
    }
}
