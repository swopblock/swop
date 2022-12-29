namespace Swopblock.API
{
    public interface IAppAPI { }

    public interface IConfirming { }
    public interface ICoreAPI
    {
        public IOrderBroadcasting Subscribe(IAppAPI App);

        public IConfirming Subscribe(ICarrierAPI[] Carriers);
    }

    public class CoreAPI : ICoreAPI, IOrderBroadcasting
    {
        IAppAPI AppAPI;

        ICarrierAPI[] CarrierAPI = new ICarrierAPI[256];

        IOrderAPI[] Broadcasted = new IOrderAPI[1024];

        IConfirmationAPI[] Confirmed = new IConfirmationAPI[1024];

        public IOrderBroadcasting Subscribe(IAppAPI App)
        {
            AppAPI = App;

            return (IOrderBroadcasting)this;
        }

        public IConfirming Subscribe(ICarrierAPI[] Carriers)
        {
            CarrierAPI = Carriers;

            return (IConfirming)this;
        }

        public void Broadcast(IOrderAPI Order, ICarrierAPI Carrier)
        {
            Carrier.Broadcast(Order);
        }
    }

    public interface IOrderBroadcasting
    {
        void Broadcast(IOrderAPI Order, ICarrierAPI Carrier);
    }

    public interface IOrderAPI
    {
        IOrderAPI Source { get; set; }

        IOrderAPI Destination { get; set; }

        IOrderAPI Subscribe(IOrderAPI OrderAPI);

        void SendOrder(IOrderAPI Order);

        static IOrderAPI Parse(string OrderIntention) { return null; }
    }

    public class OrderAPI : IOrderAPI
    {
        IOrderAPI UseSource, UseDesination;

        public IOrderAPI Destination { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IOrderAPI Source { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void SendOrder(IOrderAPI Order)
        {
            throw new NotImplementedException();
        }

        public IOrderAPI Subscribe(IOrderAPI Desination)
        {
            //this.Desination = Desination;
            //this.Source = Desination.Sour

            return (IOrderAPI)this;
        }
    }


    public interface IItemAPI
    {

    }

    public class ItemAPI
    {

    }

    public interface IConfirmationAPI
    {
        IOrderAPI[] Orders { get; set; }
    }

    public class ConfirmationAPI
    {

    }
}
