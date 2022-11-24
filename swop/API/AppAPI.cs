using System.Runtime.InteropServices;

namespace Swopblock.API
{
    public interface IAppAPI
    {
        public static IAppAPI NewInterface()
        {
            return new AppAPI();
        }

        public ISigning Subsribe(ILiveAPI Client);

        public IConfirming Subscribe(ICoreAPI Client);
    }

    public class AppAPI : IAppAPI, ISigning, IConfirming
    {
        private ILiveAPI LiveAPI;

        private ICoreAPI CoreAPI;

        private IOrderAPI[] Signed;

        private IConfirmationAPI[] Confirmed;

        public void Confirm(IConfirmationAPI Confirmation)
        {
            Confirmed[0] = Confirmation;
        }

        public void Sign(string OrderIntention)
        {
            Signed[0] = IOrderAPI.Parse(OrderIntention);
        }

        public IConfirming Subscribe(ICoreAPI Client)
        {
            CoreAPI = Client;

            return (IConfirming)this;
        }

        public ISigning Subsribe(ILiveAPI Client)
        {
            LiveAPI = Client;

            return (ISigning)this;
        }
    }

    public interface ISigning
    {
        void Sign(string OrderIntention);
    }

    public interface IConfirming
    {
        void Confirm(IConfirmationAPI Confirmation);
    }
}
