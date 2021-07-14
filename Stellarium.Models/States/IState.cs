using Bencodex.Types;

namespace Stellarium.Models.States
{
    public interface IState
    {
        IValue Serialize();
    }
}
