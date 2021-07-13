using Libplanet;

namespace Stellarium.Models.States.Ledgers
{
    public abstract class Reserve : Ledger
    {
        public Reserve(Address owner, Address address)
            : base(owner, address)
        {
        }
    }
}
