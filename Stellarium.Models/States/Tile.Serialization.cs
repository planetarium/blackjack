using Bencodex.Types;
using Libplanet;

namespace Stellarium.Models.States
{
    public abstract partial class Tile
    {
        protected Tile(Bencodex.Types.Dictionary serialized)
            : this(
                (Integer)serialized["galaxyId"],
                new Address((Binary)serialized["owner"]),
                new Address((Binary)serialized["address"]),
                new Position((List)serialized["position"])
            )
        {
        }

        public virtual Bencodex.Types.Dictionary Serialize() =>
            Bencodex.Types.Dictionary.Empty
                .Add("galaxyId", GalaxyId)
                .Add("owner", Owner.ByteArray)
                .Add("address", Address.ByteArray)
                .Add("position", (IValue)Position.Serialize());
    }
}
