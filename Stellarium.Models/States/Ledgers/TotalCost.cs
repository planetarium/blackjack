using Bencodex.Types;

namespace Stellarium.Models.States.Ledgers
{
    public sealed class TotalCost
    {
        public TotalCost(Cost metalCost, Cost gasCost)
        {
            MetalCost = metalCost;
            GasCost = gasCost;
        }

        public Cost MetalCost { get; }
        public Cost GasCost { get; }

        public TotalCost(Bencodex.Types.Dictionary serialized)
            : this(
                new Cost((Dictionary)serialized["metalCost"]),
                new Cost((Dictionary)serialized["gasCost"]))
        {
        }

        public Bencodex.Types.Dictionary Serialize()
        {
            return Bencodex.Types.Dictionary.Empty
                .Add("metalCost", MetalCost.Serialize())
                .Add("gasCost", GasCost.Serialize());
        }
    }
}
