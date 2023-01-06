// Copywrite (c) 2022 Swopblock LLC   (see https://github.com/swopblock)
// Created December 29, 2022 4:52 PM ET by Jeff Hilde, jeff@swopblock.org

using Swopblock.API.Data;

namespace Swopblock.API.State
{
    public class Exchange : LedgerAccount<Exchange, Market[]>
    {
    }

    public class Market : LedgerAccount<Exchange, User[]>
    {
    }

    public class User : LedgerAccount<Market, Account[]>
    {
    }

    public class Account : LedgerAccount<User, Address[]>
    {
    }

    public class Address : LedgerAccount<Account, Supply[]>
    {
    }

    public class Supply : LedgerAccount<Address, Trade[]>
    {
    }

    public class Demand : LedgerAccount<Address, Trade[]>
    {
    }

    public class Trade : LedgerAccount<Supply, Demand>
    {
    }

    public class LedgerAccount<SupplyAccounts, DemandAccounts> 
    {
        SupplyAccounts Suppling;

        public decimal CashSupply;

        public decimal AssetSupply;

        public decimal CashDemand;

        public decimal AssetDemand;

        public DemandAccounts Demanding;

        public decimal CashReserve
        {
            get { return CashSupply - CashDemand; }

            init { CashSupply = value; }
        }

        public decimal AssetReserve
        {
            get { return AssetSupply - AssetDemand; }

            init { AssetSupply = value; }
        }
    }
}

