namespace Swopblock.API
{
    //public interface IAppAPI { }
    public interface ILiveAPI
    {
        void Subscribe(ILiveAppAPI App);

        void Subscribe(ILiveCoreAPI Core);

        void Subscribe(ILiveCarrierAPI[] Carriers);
    }

    public class LiveAPI : ILiveAPI, IRunning
    {
        public ILiveAppAPI App;

        public ILiveCoreAPI Core;

        public ILiveCarrierAPI[] Carriers;

        public void Run()
        {
            App.Run();

            Core.Run();

            foreach (var carrier in Carriers)
            {
                carrier.Run();
            }
        }

        public void Subscribe(ILiveAppAPI App)
        {
            this.App = App;
        }

        public void Subscribe(ILiveCoreAPI Core)
        {
            this.Core = Core;
        }

        public void Subscribe(ILiveCarrierAPI[] Carriers)
        {
            this.Carriers = Carriers;
        }
    }

    public interface ILiveAppAPI : IRunning
    {
        void Subscribe(IAppAPI App);

        void Run();
    }

    public class LiveAppAPI : ILiveAppAPI
    {
        public IAppAPI App;

        public void Subscribe(IAppAPI App)
        {
            this.App = App;
        }

        public void Run() { }
    }

    public interface ILiveCoreAPI : IRunning
    {
        void Run();
    }

    public class LiveCoreAPI : ILiveCoreAPI
    {
        ICoreAPI Core;

        public void Subscribe(ICoreAPI Core)
        {
            this.Core = Core;
        }

        public void Run() { }
    }

    public interface ILiveCarrierAPI : IRunning
    {

    }

    public class LiveCarrierAPI : ILiveCarrierAPI
    {
        ICarrierAPI[] Carriers;

        public void Subscribe(ICarrierAPI[] Carriers)
        {
            this.Carriers = Carriers;
        }

        public void Run() { }
    }

    public interface IRunning
    {
        void Run();
    }
}
