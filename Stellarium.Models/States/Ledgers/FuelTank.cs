using Libplanet;

namespace Stellarium.Models.States.Ledgers
{
    public sealed class FuelTank : Ledger
    {
        public FuelTank(Address owner, Address address)
            : base(owner, address)
        {
        }

        public override Bencodex.Types.Dictionary Serialize()
        {
            return base.Serialize()
                .Add("_type", nameof(FuelTank));
        }
    }
}
