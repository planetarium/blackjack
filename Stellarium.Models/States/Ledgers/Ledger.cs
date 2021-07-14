using Bencodex.Types;
using Libplanet;
using Libplanet.Action;
using Libplanet.Assets;
using System.Numerics;
using Stellarium.Models.States.Deposits;

namespace Stellarium.Models.States.Ledgers
{
    /// <summary>
    /// Generic ledger class for keeping track of all resources.
    /// Used by <see cref="Deposit"/>s and <see cref="Unit"/>s.
    /// This is where <see cref="Resource"/>s are actually handled.
    /// </summary>
    public abstract class Ledger
    {
        public Ledger(Address owner, Address address)
        {
        }

        public Address Owner { get; }

        public Address Address { get; }

        // FIXME: Placeholder.
        public BigInteger Metal => GetMetal(
            (address, currency) => new FungibleAssetValue(currency));

        public BigInteger Gas => GetMetal(
            (address, currency) => new FungibleAssetValue(currency));

        public BigInteger GetMetal(AccountBalanceGetter balanceGetter)
        {
            return balanceGetter(Address, Resource.Metal).RawValue;
        }

        public BigInteger GetGas(AccountBalanceGetter balanceGetter)
        {
            return balanceGetter(Address, Resource.Gas).RawValue;
        }

        protected Ledger(Bencodex.Types.Dictionary serialized)
            : this(
                new Address((Binary)serialized["owner"]),
                new Address((Binary)serialized["address"]))
        {
        }

        public virtual Bencodex.Types.Dictionary Serialize()
        {
            return Bencodex.Types.Dictionary.Empty
                .Add("owner", Owner.ByteArray)
                .Add("address", Address.ByteArray);
        }
    }
}
