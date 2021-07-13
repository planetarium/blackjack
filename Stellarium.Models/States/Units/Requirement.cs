using Bencodex.Types;

namespace Stellarium.Models.States.Units
{
    /// <summary>
    /// Class representing non-resource related requirements.
    /// </summary>
    public class Requirement
    {
        public Requirement(long requiredHQTier)
        {
            RequiredHQTier = requiredHQTier;
        }

        public long RequiredHQTier { get; }

        public Requirement(Bencodex.Types.Dictionary serialized)
            : this((Integer)serialized["requiredHQTier"])
        {
        }

        public Bencodex.Types.Dictionary Serialize()
        {
            return Bencodex.Types.Dictionary.Empty
                .Add("requiredHQTier", RequiredHQTier);
        }
    }
}
