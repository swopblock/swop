// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using Swopblock.Liquidity;
using Swopblock.Swopping;

namespace Swopblock.Application
{
    public interface APP : IApplication
    {
        public decimal CashBalance { get; set; }

        public decimal CashVolume { get; set; }

        public bool CaptureIntention(string intention)
        {
            Bidding(0, (IBranch)null, 0);

            Asking(0, (IBranch)null, 0);

            return false;
        }

        bool Bidding(decimal Bid, IBranch Buy, decimal Expiration);

        bool Asking(decimal Ask, IBranch Sell, decimal Expiration);

        public IOrder GetReport();

        public IAccount[] Accounts { get; set; }

        public void Save(Stream stream)
        {

        }

        public void Load(Stream stream)
        {

        }
    }

    public interface CORE : IApplication
    {
        public IMain Main { get; set; }

        public CARRIERS[] CARRIERS { get; set; }
    }

    public interface CARRIERS : IApplication
    {

    }

    public interface IApplication { }
}
