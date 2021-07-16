using Bencodex.Types;
using Libplanet.Action;

namespace Blackjack.Models.Actions
{
    public abstract class BaseAction : IAction
    {
        public static readonly IValue MarkChanged = default(Null);
        public abstract IValue PlainValue { get; }

        public abstract IAccountStateDelta Execute(IActionContext context);

        public abstract void LoadPlainValue(IValue plainValue);
    }
}
