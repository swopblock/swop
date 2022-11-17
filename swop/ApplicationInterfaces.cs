// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using Swopblock.Liquidity;
using Swopblock.Swopping;

namespace Swopblock.Application
{
    public enum Offering
    {
        Cash,
        Asset,
    }

    public interface APP : IApplication
    {
        public decimal CashBalance { get; set; }

        public decimal CashVolume { get; set; }

        public IAccount[] Accounts { get; set; }

        public IReport SubmitIntention(IOrder Order);
    }

    public interface CORE : IApplication
    {
        public IMain Main { get; set; }

        IReport SubmitOrder(IOrder Order);
    }

    public interface CARRIERS : IApplication
    {
        IReport SubmitOrder(IOrder Order);
    }

    public interface IApplication { }

}
