using System;
using System.Collections.Generic;
using Bencodex.Types;
using Blackjack.Models.States;
using Libplanet.Action;

namespace Blackjack.Models.Actions
{
    [Serializable]
    [ActionType("clicker")]
    public class ClickerAction : BaseAction
    {
        private long _click;

        public ClickerAction()
        {
        }

        public ClickerAction(long click)
        {
            _click = click;
        }

        public override IAccountStateDelta Execute(IActionContext context)
        {
            if (context.Rehearsal)
            {
                return context.PreviousStates.SetState(ClickerState.ClickerStateKey, MarkChanged);
            }
            IAccountStateDelta state = context.PreviousStates;
            var stateValue = state.GetState(ClickerState.ClickerStateKey);
            ClickerState clickerState;
            if (stateValue is { })
            {
                clickerState = new ClickerState((Dictionary) stateValue);
            }
            else
            {
                clickerState = new ClickerState(0);
            }
            clickerState.Count += _click;
            return state.SetState(ClickerState.ClickerStateKey, clickerState.Serialize());
        }

        public override void LoadPlainValue(IValue plainValue)
        {
            var asDict = (Dictionary) plainValue;
            _click = (Integer) asDict[nameof(_click)];
        }

        public override IValue PlainValue =>
            new Dictionary(
                new[]
                {
                    new KeyValuePair<IKey, IValue>((Text)nameof(_click), new Integer(_click))
                }
            );
    }
}
