using Swopblock.API.Data;
using Swopblock.API.Process;

namespace Swopblock.API
{

    public interface ICustody : IUser, IAuto
    {
        public ICash[] Circulated { get; set; }

        public IAsset[] Uncirculated { get; set; }

    }

    public interface IAPP : ICustody
    {
        ICORE[] CORE { get; set; }
    }

    public interface ICORE : ILayer, ICustody
    {
        ICARRIER[] CARRIER { get; set; }
    }

    public interface ICARRIER : ILayer, ICustody
    {
        ILayer[] Layers { get; set; }
    }

    public interface IUser
    {
        IMessageQueue SigningQueue { get; }

        void Sign(IMessage message)
        {
            SigningQueue.Enqueue(message);
        }
    }

    public interface IAuto
    {
        IMessageQueue ConfirmingQueue { get; }

        void Confirm(IMessage message)
        {
            ConfirmingQueue.Enqueue(message);
        }
    }


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
        public IClaim[] Claims { get; set; }
    }

    public interface IPatent
    {
        public string Symbol { get; set; }

        public decimal Domain { get; set; }
    }

}
