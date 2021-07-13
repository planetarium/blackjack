using Bencodex.Types;

namespace Stellarium.Models.States.Units
{
    public class Energy
    {
        public Energy(long health, long shield)
        {
            Health = health;
            Shield = shield;
        }

        public long Health { get; }

        public long Shield { get; }

        public Energy(Bencodex.Types.Dictionary serialized)
            : this(
                (Integer)serialized["health"],
                (Integer)serialized["shield"])
        {
        }

        public Bencodex.Types.Dictionary Serialize() =>
            Bencodex.Types.Dictionary.Empty
                .Add("Health", Health)
                .Add("Shield", Shield);
    }
}
