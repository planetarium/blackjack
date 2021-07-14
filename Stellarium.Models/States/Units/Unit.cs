using Bencodex.Types;
using Libplanet;
using Stellarium.Models.States.Ledgers;

namespace Stellarium.Models.States.Units
{
    /// <summary>
    /// Generic class for all buildable units.
    /// </summary>
    public abstract class Unit : Tile
    {
        public Unit(
            uint galaxyId,
            Address owner,
            Address address,
            Position position,
            Consumed consumed,
            Requirement requirement,
            PhysicalStat physicalStat,
            Energy energy) : base(galaxyId, owner, address, position)
        {
            Consumed = consumed;
            Requirement = requirement;
            PhysicalStat = physicalStat;
            Energy = energy;
        }

        public Consumed Consumed { get; }

        public Requirement Requirement { get; }

        public PhysicalStat PhysicalStat { get; }

        public Energy Energy { get; }

        // FIXME: Placeholder.
        public bool ReadyToBuild => Consumed.Full;

        public bool BuildInProgress => Consumed.Full;

        public bool BuildComplete => Consumed.Full;

        public bool Destroyed => Energy.Health <= 0;

        public Unit(Bencodex.Types.Dictionary serialized)
            : base(serialized)
        {
            Consumed = new Consumed((Dictionary)serialized["consumed"]);
            Requirement = new Requirement((Dictionary)serialized["requirement"]);
            PhysicalStat = new PhysicalStat((Dictionary)serialized["physicalSat"]);
            Energy = new Energy((Dictionary)serialized["energy"]);
        }

        public override Bencodex.Types.Dictionary Serialize()
        {
            return base.Serialize()
                .Add("consumed", Consumed.Serialize())
                .Add("requirement", Requirement.Serialize())
                .Add("physicalStat", PhysicalStat.Serialize())
                .Add("energy", Energy.Serialize());
        }
    }
}
