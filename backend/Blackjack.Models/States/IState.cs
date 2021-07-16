using Bencodex.Types;

namespace Blackjack.Models.States
{
    public interface IState
    {
        IValue Serialize();
    }
}
