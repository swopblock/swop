// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using Swopblock.API.Liquidity;
using Swopblock.API.Swopping;

namespace Swopblock.API.Application
{
    public interface IApp : IApplication
    {
        public decimal CashBalance { get; set; }

        public decimal CashVolume { get; set; }

        public IAccount[] Accounts { get; set; }

        public IReport SubmitIntention(IOrder Order);
    }

    public interface ICore : IApplication
    {
        public IMain Main { get; set; }

        IReport SubmitOrder(IOrder Order);
    }

    public interface ICarriers : IApplication
    {
        IReport SubmitOrder(IOrder Order);
    }

    public interface IApplication { }

}
