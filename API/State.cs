// Copywrite (c) 2022 Swopblock LLC   (see https://github.com/swopblock)
// Created December 29, 2022 4:52 PM ET by Jeff Hilde, jeff@swopblock.org

using Swopblock.API.Data;

namespace Swopblock.API.State
{
    public class Main : Ledger<Branch>
    {
        public decimal CashReserve;
        
        public Branch[] Branches;
    }

    public class Branch : Ledger<User>
    {
        public Main MainOfThisBranch;

        public decimal CashReserve;

        public decimal AssetReserve;

        public User[] Users;
    }

    public class User : Ledger<Account>
    {
        public Branch BranchOfThisUser;

        public decimal CashReserve;

        public decimal AssetReserve;

        public Account[] Accounts;
    }

    public class Account : Ledger<Address>
    {
        public User UserOfThisAccount;

        public decimal CashReserve;

        public decimal AssetReserve;

        public Address[] Addresses;
    }

    public class Address : Ledger<Output>
    {
        public Account AccountOfThisAddress;

        public decimal CashReserve;

        public decimal AssetReserve;

        public Output[] Outputs;
    }

    public class Output
    {
        public Address AddressOfThisOutput;

        public decimal CashReserve;

        public decimal AssetReserve;

        public Trade Trade;
    }

    public class Trade
    {
        public Output ReservingOutputOfThisTrade;

        public Output ConfirmingOutputOfThisTrade;
    }

    public class Ledger<A> 
    {
        public A[] Accounts;
    }
}

