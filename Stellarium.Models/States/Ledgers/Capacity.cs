using System.Numerics;
using Bencodex.Types;

namespace Stellarium.Models.States.Ledgers
{
    /// <summary>
    /// Class representing a limit on how much a <see cref="Storage"/> can
    /// hold.
    /// </summary>
    public sealed class Capacity
    {
        public Capacity(
            Quantity metal, Quantity gas, long weight)
        {
            Metal = metal;
            Gas = gas;
            Weight = weight;
        }

        public Quantity Metal { get; }
        public Quantity Gas { get; }
        public long Weight { get; }

        public Capacity(Bencodex.Types.Dictionary serialized)
            : this(
                new Quantity((Dictionary)serialized["metal"]),
                new Quantity((Dictionary)serialized["gas"]),
                (Integer)serialized["weight"])
        {
        }

        public Bencodex.Types.Dictionary Serialize()
        {
            return Bencodex.Types.Dictionary.Empty
                .Add("_type", nameof(Capacity))
                .Add("metal", Metal.Serialize())
                .Add("gas", Gas.Serialize())
                .Add("weight", Weight);
        }
    }
}
