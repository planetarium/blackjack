using Libplanet;
using Stellarium.Models.States.Ledgers;

namespace Stellarium.Models.States.Deposits
{
    /// <summary>
    /// Generic class for a resource deposit.
    /// </summary>
    public abstract class Deposit : Tile
    {
        public Deposit(
            uint galaxyId,
            Address owner,
            Address address,
            Position position,
            Reserve reserve)
                : base(galaxyId, owner, address, position)
        {
            Reserve = reserve;
        }

        public Reserve Reserve { get; }

        public override Bencodex.Types.Dictionary Serialize()
        {
            return base.Serialize()
                .Add("reserve", Reserve.Serialize());
        }
    }
}
