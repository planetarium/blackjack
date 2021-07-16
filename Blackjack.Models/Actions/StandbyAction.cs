using System;
using System.Collections.Generic;
using Bencodex.Types;
using Blackjack.Models.States;
using Libplanet;
using Libplanet.Action;

namespace Blackjack.Models.Actions
{
    [Serializable]
    [ActionType("standby")]
    public class StandbyAction : BaseAction
    {
        private Address _address;

        public StandbyAction()
        {
        }

        public StandbyAction(Address address)
        {
            _address = address;
        }

        public override IAccountStateDelta Execute(IActionContext context)
        {
            if (context.Rehearsal)
            {
                return context.PreviousStates.SetState(_address, MarkChanged);
            }
            IAccountStateDelta state = context.PreviousStates;
            if (!_address.Equals(context.Signer))
            {
                throw new PermissionException(_address, context.Signer);
            }

            var stateValue = state.GetState(_address);

            if (stateValue is { })
            {
                throw new ExistingException(_address);
            }

            var accountState = new BlackJackAccountState(
                _address,
                BlackJackAccountState.AccountStatus.StandBy,
                context.BlockIndex
            );

            return state.SetState(_address, accountState.Serialize());
        }

        public override void LoadPlainValue(IValue plainValue)
        {
            var asDict = (Dictionary) plainValue;
            _address = asDict[nameof(_address)].ToAddress();
        }

        public override IValue PlainValue =>
            new Dictionary(
                new[]
                {
                    new KeyValuePair<IKey, IValue>((Text)nameof(_address), _address.Serialize())
                }
            );
    }
}
