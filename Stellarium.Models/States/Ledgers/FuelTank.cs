using Libplanet;

namespace Stellarium.Models.States.Ledgers
{
    public sealed class FuelTank : Storage
    {
        public FuelTank(Address owner, Address address, Capacity capacity)
            : base(owner, address, capacity)
        {
        }

        public FuelTank(Bencodex.Types.Dictionary serialized)
            : base(serialized)
        {
        }

        public override Bencodex.Types.Dictionary Serialize()
        {
            return base.Serialize()
                .Add("_type", nameof(FuelTank));
        }
    }
}
