// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

namespace Swopblock.API.Liquidity
{
    public interface IMain : IBranches, ILiquidity
    {
        public decimal CashBalance { get; set; }

        public decimal CashVolume { get; set; }
    }

    public interface IBranches
    {
        public IBranch[] Branches { get; set; }
    }

    public interface IBranch : IAccounts, ILiquidity
    {
        public IMain Supply { get; set; }

        public IMain Demand { get; set; }

        public decimal CashBalance { get; set; }

        public decimal CashVolume { get; set; }

        public decimal AssetBalance { get; set; }

        public decimal AssetVolume { get; set; }
    }

    public interface IAccounts
    {
        public IAccount[] Accounts { get; set; }
    }

    public interface IAccount : IAddresses, ILiquidity
    {
        public IBranch Supply { get; set; }

        public IBranch Demand { get; set; }

        public decimal CashBalance { get; set; }

        public decimal CashVolume { get; set; }

        public decimal AssetBalance { get; set; }

        public decimal AssetVolume { get; set; }
    }

    public interface IAddresses
    {
        public IAddress[] Addresses { get; set; }
    }

    public interface IAddress : IOutputs, ILiquidity
    {
        public IAccount Supply { get; set; }

        public IAccount Demand { get; set; }

        public decimal CashBalance { get; set; }

        public decimal CashVolume { get; set; }

        public decimal AssetBalance { get; set; }

        public decimal AssetVolume { get; set; }
    }

    public interface IOutputs
    {
        public IOutput[] Outputs { get; set; }
    }

    public interface IOutput : Inputs, ILiquidity
    {
        public IAddress Supply { get; set; }

        public IAddress Demand { get; set; }

        public decimal CashBalance { get; set; }

        public decimal CashVolume { get; set; }

        public decimal AssetBalance { get; set; }

        public decimal AssetVolume { get; set; }
    }

    public interface Inputs
    {
        public IInput[] Inputs { get; set; }
    }

    public interface IInput : IMatches, ILiquidity
    {
        public IOutput Supply { get; set; }

        public IOutput Demand { get; set; }

        public decimal CashBalance { get; set; }

        public decimal CashVolume { get; set; }

        public decimal AssetBalance { get; set; }

        public decimal AssetVolume { get; set; }
    }

    public interface IMatches
    {
        public IMatch[] Matches { get; set; }
    }

    public interface IMatch : ILiquidity
    {
        public IInput Supply { get; set; }

        public IInput Demand { get; set; }

        public decimal CashBalance { get; set; }

        public decimal CashVolume { get; set; }

        public decimal AssetBalance { get; set; }

        public decimal AssetVolume { get; set; }

        public IMatch SwopEqualibrium()
        {
            // Swopblock LLC sofware license to be available. 

            return (IMatch)this;
        }

        public void SwopCustody(IMatch Match)
        {
            // Swopblock LLC sofware license to be available. 

            return;
        }

    }

    public interface ISupply : ILiquidity
    {
        public IMatch Supply { get; set; }

        public IMatch Demand { get; set; }
    }

    public interface IDemand : ILiquidity
    {
        public decimal CashBalance { get; set; }

        public decimal CashVolume { get; set; }

        public decimal AssetBalance { get; set; }

        public decimal AssetVolume { get; set; }
    }

    public interface ILiquidity { }
}
