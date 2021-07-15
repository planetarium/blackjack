using System.Collections.Generic;
using Bencodex.Types;
using Libplanet;

namespace Blackjack.Models.States
{
    public class BlackJackAccountState : IState
    {
        public BlackJackAccountState(
            Address address,
            AccountStatus status,
            long cumulatingSince,
            long moneyEarned = 0,
            long moneyCumulated = 0,
            long moneyBase = 10,
            House classHouse = House.Hut,
            Doge classDoge = Doge.Poor,
            Launch classLaunch = Launch.Ground,
            long randomSeed = 0)
        {
            Address = address;
            Status = status;
            CumulatingSince = cumulatingSince;
            MoneyEarned = moneyEarned;
            MoneyCumulated = moneyCumulated;
            MoneyBase = moneyBase;
            ClassHouse = classHouse;
            ClassDoge = classDoge;
            ClassLaunch = classLaunch;
            RandomSeed = randomSeed;
        }

        public BlackJackAccountState(Dictionary serialized)
        {
            Address = serialized[nameof(Address)].ToAddress();
            Status = (AccountStatus) (long) (Integer) serialized[nameof(Status)];
            CumulatingSince = (Integer) serialized[nameof(CumulatingSince)];
            MoneyEarned = (Integer) serialized[nameof(MoneyEarned)];
            MoneyCumulated = (Integer) serialized[nameof(MoneyCumulated)];
            MoneyBase = (Integer) serialized[nameof(MoneyBase)];
            ClassHouse = (House) (long) (Integer) serialized[nameof(ClassHouse)];
            ClassDoge = (Doge) (long) (Integer) serialized[nameof(ClassDoge)];
            ClassLaunch = (Launch) (long) (Integer) serialized[nameof(ClassLaunch)];
            RandomSeed = (Integer)serialized[nameof(RandomSeed)];
        }

        public enum Doge
        {
            Poor = 100,
            WellFed = 1011,
            Muscle = 102
        }

        public enum House
        {
            Hut = 200,
            Brick = 201,
            Penthouse = 202
        }

        public enum Launch
        {
            Ground = 300,
            Cloud = 301,
            Moon = 302
        }

        public enum AccountStatus
        {
            StandBy = 0,
            InGame = 1
        }

        public Address Address { get; private set; }

        public long MoneyEarned { get; set; }

        public long MoneyCumulated { get; set; }
        public long MoneyBase { get; set; }

        public long CumulatingSince { get; set; }

        public AccountStatus Status { get; set; }

        public House ClassHouse { get; set; }

        public Doge ClassDoge { get; set; }

        public Launch ClassLaunch { get; set; }

        public long RandomSeed { get; set; }


        public IValue Serialize()
        {
            return new Dictionary(new Dictionary<IKey, IValue>
            {
                [(Text) nameof(Address)] = Address.Serialize(),
                [(Text) nameof(Status)] = new Integer((long) Status),
                [(Text) nameof(CumulatingSince)] = new Integer(CumulatingSince),
                [(Text) nameof(MoneyEarned)] = new Integer(MoneyEarned),
                [(Text) nameof(MoneyCumulated)] = new Integer(MoneyCumulated),
                [(Text) nameof(MoneyBase)] = new Integer(MoneyBase),
                [(Text) nameof(ClassHouse)] = new Integer((long) ClassHouse),
                [(Text) nameof(ClassDoge)] = new Integer((long) ClassDoge),
                [(Text) nameof(ClassLaunch)] = new Integer((long) ClassLaunch),
                [(Text) nameof(RandomSeed)] = new Integer(RandomSeed)
            });
        }
    }
}
