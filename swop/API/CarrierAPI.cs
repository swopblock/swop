namespace Swopblock.API
{
    public interface ICarrierAPI
    {
        public void Broadcast(IOrderAPI Order);
    }

    public class CarrierAPI : ICarrierAPI
    {
        ICoreAPI CoreAPI;

        void Broadcast(IItemAPI Item) { }

        public IItemBroadcasting Subscribe(ICarrierAPI[] Client) { return null; }

        public void Broadcast(IOrderAPI Order)
        {

        }

    }

    public class BTCCarrierAPI : CarrierAPI
    {

    }

    public class ETHCarrierAPI : CarrierAPI
    {

    }


    public interface IItemBroadcasting
    {
        void Broadcast(IItemAPI Item);
    }

}
