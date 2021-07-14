using Bencodex.Types;
using Libplanet;
using System;

namespace Stellarium.Models.States.Ledgers
{
    /// <summary>
    /// Generic storage class representing a <see cref="Ledger"/> with
    /// a <see cref="Capacity"/>.
    /// </summary>
    public abstract class Storage : Ledger
    {
        public Storage(Address owner, Address address, Capacity capacity)
            : base(owner, address)
        {
            Capacity = capacity;
        }

        public Capacity Capacity { get; }

        public bool Full => (
            Metal == Capacity.Metal.Amount &&
            Gas == Capacity.Gas.Amount);

        protected Storage(Bencodex.Types.Dictionary serialized)
            : base(serialized)
        {
            Capacity = new Capacity((Dictionary)serialized["capacity"]);
        }

        public override Bencodex.Types.Dictionary Serialize()
        {
            return base.Serialize()
                .Add("capacity", Capacity.Serialize());
        }
    }
}
