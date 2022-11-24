namespace Swopblock.API
{
    public interface ILiveAPI
    {

    }

    public class LiveAPI : ILiveAPI
    {
        IAppAPI AppAPI;

        ICoreAPI CoreAPI;

        ICarrierAPI[] CarrierAPI;
    }
}
