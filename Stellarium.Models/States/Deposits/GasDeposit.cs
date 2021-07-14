using Libplanet;
using Stellarium.Models.States.Ledgers;

namespace Stellarium.Models.States.Deposits
{
    /// <summary>
    /// Class for a gas deposit.
    /// </summary>
    public sealed class GasDeposit : Deposit
    {
        public GasDeposit(
            uint galaxyId,
            Address owner,
            Address address,
            Position position,
            GasReserve gasReserve)
                : base(galaxyId, owner, address, position, gasReserve)
        {
        }

        public override Bencodex.Types.Dictionary Serialize()
        {
            return base.Serialize()
                .Add("_type", nameof(GasDeposit));
        }
    }
}
