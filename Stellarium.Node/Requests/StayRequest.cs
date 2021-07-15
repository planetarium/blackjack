namespace Stellarium.Node.Requests
{
    public class StayRequest
    {
        public StayRequest(string privateKey, int stayed)
        {
            PrivateKey = privateKey;
            Stayed = stayed;
        }

        public string PrivateKey { set; get; }

        public int Stayed { set; get; }
    }
}
