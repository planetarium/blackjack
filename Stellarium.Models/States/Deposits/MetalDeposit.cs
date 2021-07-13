using Libplanet;
using Stellarium.Models.States.Ledgers;

namespace Stellarium.Models.States.Deposits
{
    /// <summary>
    /// Class for a metal deposit.
    /// </summary>
    public sealed class MetalDeposit : Deposit
    {
        public MetalDeposit(
            uint galaxyId,
            Address owner,
            Address address,
            Position position,
            MetalReserve metalReserve)
                : base(galaxyId, owner, address, position, metalReserve)
        {
        }

        public override Bencodex.Types.Dictionary Serialize()
        {
            return base.Serialize()
                .Add("_type", nameof(MetalDeposit));
        }
    }
}
