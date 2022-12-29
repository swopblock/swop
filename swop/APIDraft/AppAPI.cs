using System.Runtime.InteropServices;

namespace Swopblock.API
{
    /*
    public interface IAppAPI
    {
        public IMessage Subsribe(IOrderAPI Order);

        public IConfirming Subscribe(ICoreAPI LiveCore);
    }

    public class AppAPI : IAppAPI, IMessage, IConfirming
    {
        private IOrderAPI[] Input, InputOutput;

        private ILiveAPI LiveAPI;

        private ICoreAPI CoreAPI;

        private IOrderAPI[] Signed = new IOrderAPI[1024];

        private IConfirmationAPI[] Confirmed = new IConfirmationAPI[1024];

        public void Confirm(IConfirmationAPI Confirmation)
        {
            Confirmed[0] = Confirmation;
        }

        public void Sign(string OrderIntention)
        {
            Signed[0] = IOrderAPI.Parse(OrderIntention);

        }

        public IConfirming Subscribe(ILiveCoreAPI Client)
        {
            //CoreAPI = Client;

            return (IConfirming)this;
        }

        public IConfirming Subscribe(ICoreAPI LiveCore)
        {
            throw new NotImplementedException();
        }

        public IMessage Subsribe(ILiveAPI Client)
        {
            LiveAPI = Client;

            return (IMessage)this;
        }

        public IMessage Subsribe(IOrderAPI Order)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMessage
    {
        void Sign(string OrderIntention);
    }

    public interface IConfirming
    {
        void Confirm(IConfirmationAPI Confirmation);
    }
    */
}
