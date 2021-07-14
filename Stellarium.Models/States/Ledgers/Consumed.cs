using Bencodex.Types;
using Libplanet;

namespace Stellarium.Models.States.Ledgers
{
    public sealed class Consumed : Storage
    {
        public Consumed(Address owner, Address address, Capacity capacity)
            : base(owner, address, capacity)
        {
        }

        public Consumed(Bencodex.Types.Dictionary serialized)
            : base(serialized)
        {
        }

        public override Bencodex.Types.Dictionary Serialize()
        {
            return base.Serialize()
                .Add("_type", nameof(Consumed));
        }
    }
}
