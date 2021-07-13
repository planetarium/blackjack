using Bencodex.Types;
using Libplanet;
using System;

namespace Stellarium.Models.States.Ledgers
{
    public sealed class StockPile : Ledger
    {
        public StockPile(Address owner, Address address)
            : base(owner, address)
        {
        }

        public override Bencodex.Types.Dictionary Serialize()
        {
            return base.Serialize()
                .Add("_type", nameof(StockPile));
        }
    }
}
