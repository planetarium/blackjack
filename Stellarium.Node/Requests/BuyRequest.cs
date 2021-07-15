namespace Stellarium.Node.Requests
{
    public class BuyRequest
    {
        public BuyRequest(string privateKey, int item)
        {
            PrivateKey = privateKey;
            Item = item;
        }

        public string PrivateKey { set; get; }

        public int Item { set; get; }
    }
}
