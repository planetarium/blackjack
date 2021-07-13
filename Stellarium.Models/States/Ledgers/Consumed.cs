using Bencodex.Types;
using Libplanet;

namespace Stellarium.Models.States.Ledgers
{
    public sealed class Consumed : Ledger
    {
        public Consumed(Address owner, Address address, TotalCost totalCost)
            : base(owner, address)
        {
            TotalCost = totalCost;
        }

        public TotalCost TotalCost { get; }

        public bool Full => (
            Metal == TotalCost.MetalCost.Amount &&
            Gas == TotalCost.GasCost.Amount);

        public Consumed(Bencodex.Types.Dictionary serialized)
            : base(serialized)
        {
            TotalCost = new TotalCost((Dictionary)serialized["totalCost"]);
        }

        public override Bencodex.Types.Dictionary Serialize()
        {
            return base.Serialize()
                .Add("_type", nameof(Consumed))
                .Add("totalCost", TotalCost.Serialize());
        }
    }
}
