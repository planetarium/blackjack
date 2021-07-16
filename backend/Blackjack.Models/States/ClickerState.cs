using System.Collections.Generic;
using Bencodex.Types;
using Libplanet;

namespace Blackjack.Models.States
{
    public class ClickerState : IState
    {
        public static readonly Address ClickerStateKey = StateAddresses.Clicker;
        public long Count { get; set; }

        public ClickerState(long count=0)
        {
            Count = count;
        }

        public ClickerState(Dictionary serialized)
        {
            Count = (Integer) serialized[nameof(Count)];

        }
        public IValue Serialize()
        {
            return new Dictionary(new Dictionary<IKey, IValue>
            {
                [(Text) nameof(Count)] = new Integer(Count)
            });
        }
    }
}
