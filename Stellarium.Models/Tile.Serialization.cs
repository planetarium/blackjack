using Bencodex.Types;

namespace Stellarium.Models
{
    public abstract partial class Tile
    {
        protected Tile(Bencodex.Types.Dictionary serialized)
            : this(
                (Integer)serialized["galaxyId"],
                new Position((List)serialized["position"])
            )
        {
        }

        public virtual Bencodex.Types.Dictionary Serialize() =>
            Bencodex.Types.Dictionary.Empty
                .Add("galaxyId", GalaxyId)
                .Add("position", (IValue)Position.Serialize());
    }
}
