using Bencodex.Types;
using Libplanet.Action;

namespace Stellarium.Models.Actions
{
    public abstract class BaseAction : IAction
    {
        public abstract IValue PlainValue { get; }

        public abstract IAccountStateDelta Execute(IActionContext context);

        public abstract void LoadPlainValue(IValue plainValue);
    }
}
