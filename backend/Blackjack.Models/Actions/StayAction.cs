using System;
using System.Collections.Generic;
using Bencodex.Types;
using Blackjack.Models.States;
using Jint;
using Libplanet;
using Libplanet.Action;
using Libplanet.Blockchain.Renderers.Debug;

namespace Blackjack.Models.Actions
{
    [Serializable]
    [ActionType("start")]
    public class StayAction : BaseAction
    {
        private Address _address;

        private long _stay;

        private BlackjackJS _blackjackEngine;

        public StayAction()
        {
            _blackjackEngine = new BlackjackJS();
        }

        public StayAction(Address address, long stay)
            :this()
        {
            _address = address;
            _stay = stay;
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

            if (!(stateValue is { }))
            {
                throw new NotExistingException(_address);
            }

            var accountState = new BlackJackAccountState((Dictionary)stateValue);

            if (accountState.Status != BlackJackAccountState.AccountStatus.InGame)
            {
                throw new InvalidOperationException();
            }

            accountState.MoneyEarned +=
                BlackJack(accountState.RandomSeed, _stay, accountState.MoneyCumulated);

            accountState.MoneyCumulated = 0;
            accountState.Status = BlackJackAccountState.AccountStatus.StandBy;
            accountState.CumulatingSince = context.BlockIndex;
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

        private long BlackJack(long randomSeed, long stay, long money)
        {
            var gameResult = _blackjackEngine.PlayGame(randomSeed, stay);
            if (gameResult.Win == false)
            {
                return 0;
            }

            while (stay-- <= 0)
            {
                money *= stay;
            }
            return money;
        }
    }
}
