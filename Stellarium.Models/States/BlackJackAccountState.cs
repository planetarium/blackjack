using Bencodex.Types;
using Libplanet;

namespace Stellarium.Models.States
{
    public class BlackJackAccountState : IState
    {
        public BlackJackAccountState(
            Address address,
            AccountStatus status,
            long moneyBase,
            long standbyFrom,
            House classHouse = House.Hut,
            Doge classDoge = Doge.Poor,
            Launch classLaunch = Launch.Ground)
        {
            Address = address;
            Status = status;
            MoneyBase = moneyBase;
            StandbyFrom = standbyFrom;
            ClassHouse = classHouse;
            ClassDoge = classDoge;
            ClassLaunch = classLaunch;
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
            StandBy =0,
            InGame = 1
        }

        public Address Address { get; set; }

        public long MoneyBase { get; set; }

        public long StandbyFrom { get; set; }

        public AccountStatus Status { get; set; }

        public House ClassHouse { get; set; }

        public Doge ClassDoge { get; set; }

        public Launch ClassLaunch { get; set; }



        public IValue Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
