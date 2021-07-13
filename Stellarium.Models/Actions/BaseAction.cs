using Bencodex.Types;
using Libplanet.Action;

namespace Stellarium.Models.Actions
{
    public class BaseAction : IAction
    {
        IValue IAction.PlainValue => Null.Value;

        IAccountStateDelta IAction.Execute(IActionContext context)
        {
            return context.PreviousStates;
        }

        void IAction.LoadPlainValue(IValue plainValue)
        {
        }
    }
}
