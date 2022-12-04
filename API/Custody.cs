using Swopblock.API.Data;
using Swopblock.API.Process;

namespace Swopblock.API
{

    public interface SWOBL : IAsset { }

    public interface BTC : IAsset { }

    public interface ETH : IAsset { }

    public interface ICash : IAsset
    {
        public SWOBL Base { get; set; }
    }

    public interface IAsset : IClaim
    {
        public IAsset[] Quits { get; set; }
    }

    public interface IClaim : IPatent
    {
        public decimal Sum { get; set; }
    }

    public interface IPatent
    {
        public static decimal Domain { get; set; }

        public IClaim[] Claims { get; set; }
    }

    public interface IAPP : IUser, IAuto
    {
        ICORE[] CORE { get; set; }
    }

    public interface ICORE : IUser, IAuto, ILayer
    {
        ICARRIER[] CARRIER { get; set; }
    }

    public interface ICARRIER : IUser, IAuto, ILayer
    {
        ILayer[] Layers { get; set; }
    }

    public interface IUser : IPatent
    {
        IOrderQueue OrderingQueue { get; }

        void Order(IOrdering order)
        {
            OrderingQueue.Enqueue(order);
        }
    }

    public interface IAuto : IUser
    {
        IOrderQueue ConfirmingQueue { get; }

        void Confirm(IOrdering order)
        {
            ConfirmingQueue.Enqueue(order);
        }
    }


}
