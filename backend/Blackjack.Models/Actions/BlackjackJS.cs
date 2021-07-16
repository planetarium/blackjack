using System;
using Jint;
using Jint.Native;
using Serilog;

namespace Blackjack.Models.Actions
{
    public class BlackjackJS
    {
        private static readonly string jscode = @"
    function hello() {
        log('Hello World');
    };

    hello();
    var shuffle = function(arr,seed){
      var tem, j;

      for(var i=0; i < arr.length; i++){
          j = (seed % (i+1) + i) % arr.length;
          x = arr[j];
          y = arr[i];
          arr[i] = x;
          arr[j] = y;
      }
      return arr;
  }

    var getDeck = function(seed) {

      var result = [];

      ['♦', '♠', '♥', '♣'].forEach(function(symbol) {
        for(var i = 1; i <= 13; i++) {
          var no = i;
          switch(no) {
            case  1: no = 'A'; break;
            case 11: no = 'J'; break;
            case 12: no = 'Q'; break;
            case 13: no = 'K'; break;
          }

          result.push(symbol + no);
        }
      });

      return shuffle(result, seed);
    }
    var getScore = function(deck) {
      var newScore, currentScore = 0, numberOfA = 0;
      deck.forEach(function(card) {
        switch(card.slice(1, 3)) {
          case 'A': newScore = 11; numberOfA++; break;
          case 'J':
          case 'Q':
          case 'K': newScore = 10; break;
          default : newScore = Number(card.slice(1, 3)); break;
        }
        currentScore += newScore;
      });
      while(currentScore > 21 && numberOfA > 0) {
        numberOfA--;
        currentScore -= 10;
      }
      return currentScore;
    }

    var getWinRate = function(upcard, playerScore) {
      var dealerTable = {
          'A': [0.126128, 0.131003, 0.129486, 0.131553, 0.0515646, 0.313726, 0.11654],
          '2': [0.138976, 0.131762, 0.131815, 0.123948, 0.120526, 0, 0.352973],
          '3': [0.130313, 0.130946, 0.123761, 0.123345, 0.116047, 0, 0.375588],
          '4': [0.130973, 0.114163, 0.120679, 0.116286, 0.115096, 0, 0.402803],
          '5': [0.119687, 0.123483, 0.116909, 0.104694, 0.106321, 0, 0.428905],
          '6': [0.166948, 0.106454, 0.107192, 0.100705, 0.0978785, 0, 0.420823],
          '7': [0.372345, 0.138583, 0.0773344, 0.0788967, 0.072987, 0, 0.259854],
          '8': [0.130857, 0.362989, 0.129445, 0.0682898, 0.0697914, 0, 0.238627],
          '9': [0.121886, 0.103921, 0.357391, 0.12225, 0.0611088, 0, 0.233442],
          '10': [0.114418, 0.112879, 0.114662, 0.328879, 0.0364661, 0.0784314, 0.214264],
          'J': [0.114418, 0.112879, 0.114662, 0.328879, 0.0364661, 0.0784314, 0.214264],
          'Q': [0.114418, 0.112879, 0.114662, 0.328879, 0.0364661, 0.0784314, 0.214264],
          'K': [0.114418, 0.112879, 0.114662, 0.328879, 0.0364661, 0.0784314, 0.214264]
      };

      if(playerScore > 21) return 0;
      else if(playerScore == 21) return 1;
      else if(playerScore >= 17) {
        var result = 1;
        for(var i = playerScore+1; i <= 22; i++)
          result -= dealerTable[upcard.slice(1, 3)][i-17];
        return result;
      }
      else return dealerTable[upcard.slice(1, 3)][6];
    }

   var playGame = function(seed, stayed) {
      var result = {
        'deck': getDeck(seed),
        'house': [],
        'houseScore': 0,
        'player': [],
        'playerScore': 0,
        'stayed': stayed,
        'seed': seed,
        'blackjack': false
      };

      var putHouseCard = function(card) {
        result.house.push(card);
        result.houseScore = getScore(result.house);
      }

      var putPlayerCard = function(card) {
        result.player.push(card);
        result.playerScore = getScore(result.player);
      }

      putHouseCard(result.deck[0]);
      putHouseCard(result.deck[1]);
      putPlayerCard(result.deck[2]);
      putPlayerCard(result.deck[3]);
      result.winRate = getWinRate(result.house[0], result.playerScore);

      if(result.playerScore == 21) {
        result.win = true;
        result.blackjack = true;
        return result;
      }

      for(var i = 4; i < 4+stayed; i++) {
        putPlayerCard(result.deck[i]);
        result.winRate = getWinRate(result.house[0], result.playerScore);
        if(result.playerScore > 21) {
          result.win = false;
          return result;
        }
      }

      result.hitExpectation = 0;
      result.hitSuccessRate = 0;
      for(var i = 4+stayed; i < 52; i++) {
        var newScore = getScore(result.player.concat(result.deck[i]));
        if(newScore <= 21)
          result.hitSuccessRate += 1;
        result.hitExpectation += newScore;
      }
      result.hitSuccessRate /= 52 - 4 - stayed;
      result.hitExpectation = result.hitExpectation / (52 - 4 - stayed);

      for(var i = 4+stayed;result.houseScore < 17;i++) {
        putHouseCard(result.deck[i]);
        if(result.houseScore > 21) {
          result.win = true;
          return result;
        }
      }

      result.win = result.playerScore >= result.houseScore;
      return result;
    }
";

        private readonly Engine _engine;
        private JsValue? PlayGameInner(long randomSeed, long stayed)
        {
            return _engine.GetValue("playGame")
                .Invoke(randomSeed, stayed);
        }

        public GameResult PlayGame(long randomSeed, long stayed)
        {
            var jsValue = PlayGameInner(randomSeed, stayed);
            if (jsValue is { })
            {
                return new GameResult
                {
                    PlayerScore = (long)jsValue.AsObject().Get("playerScore").AsNumber(),
                    Stayed = (long)jsValue.AsObject().Get("stayed").AsNumber(),
                    Seed = (long)jsValue.AsObject().Get("seed").AsNumber(),
                    Blackjack = jsValue.AsObject().Get("blackjack").AsBoolean(),
                    Win = jsValue.AsObject().Get("win").AsBoolean()
                };
            }

            return new GameResult();
        }

        public struct GameResult
        {
            public long PlayerScore;
            public long Stayed;
            public long Seed;
            public bool Blackjack;
            public bool Win;
        }


        public BlackjackJS()
        {
            _engine = new Engine().SetValue("log", new Action<object>(Console.WriteLine));
            _engine.Execute(jscode);
        }
    }
}
