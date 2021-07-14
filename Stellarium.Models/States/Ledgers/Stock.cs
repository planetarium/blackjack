using Bencodex.Types;
using Libplanet;
using System;

namespace Stellarium.Models.States.Ledgers
{
    public sealed class Stock : Storage
    {
        public Stock(Address owner, Address address, Capacity capacity)
            : base(owner, address, capacity)
        {
        }

        public Stock(Bencodex.Types.Dictionary serialized)
            : base(serialized)
        {
        }

        public override Bencodex.Types.Dictionary Serialize()
        {
            return base.Serialize()
                .Add("_type", nameof(Stock));
        }
    }
}
