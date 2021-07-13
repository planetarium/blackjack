using Libplanet;

namespace Stellarium.Models.States.Ledgers
{
    public sealed class MetalReserve : Reserve
    {
        public MetalReserve(Address owner, Address address)
            : base(owner, address)
        {
        }

        public override Bencodex.Types.Dictionary Serialize()
        {
            return base.Serialize()
                .Add("_type", nameof(MetalReserve));
        }
    }
}
