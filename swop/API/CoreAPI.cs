namespace Swopblock.API
{
    public interface ICoreAPI
    {

    }

    public class CoreAPI
    {
        IAppAPI AppAPI;

        ICarrierAPI[] CarrierAPI;
    }

    public interface IOrderAPI
    {
        static IOrderAPI Parse(string OrderIntention) { return null; }
    }

    public interface OrderAPI
    {

    }

    public interface IItemAPI
    {

    }

    public class ItemAPI
    {

    }

    public interface IConfirmationAPI
    {

    }

    public class ConfirmationAPI
    {

    }
}
