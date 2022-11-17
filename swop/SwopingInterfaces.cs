// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

namespace Swopblock.Swopping
{
    public interface IConsenting : ISwopping
    {
        public bool SignOrder(IOrder Order);

        public bool BroadcastOrder(IOrder Order);

        public bool ConfirmOrder(IOrder Order);

    }

    public interface IExecuting : ISwopping
    {
        public bool SignBlock(IBlock IBlock);

        public bool BroadcastBlock(IBlock IBlock);

        public bool ConfirmBlock(IBlock IBlock);
    }

    public interface ISettling : ISwopping
    {
        public bool SignFill(IFill Fill);

        public bool BroadcastFill(IFill Fill);

        public bool ConfirmFill(IFill fill);
    }

    public interface ISwopping { }

    public interface IOrder 
    { 
        public string Intention { get; set; }
    }

    public interface IBlock { }

    public interface IFill { }

    public interface IReport 
    {
        public string Report { get; set; }
    }
}
