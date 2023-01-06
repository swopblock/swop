// Copywrite (c) 2022 Swopblock LLC   (see https://github.com/swopblock)
// Created December 1, 2022 12:13 AM ET by Jeff Hilde, jeff@swopblock.org

using Swopblock.API.State;

namespace Swopblock.API.Data
{
    // Messaging is a base record for executing, ordering, invoicing and receipting
    //     in bidding, asking, buying, selling, paying, cashing, expencing and incoming transactions.
    public record Transaction(User Executive);


    // make a market order
    public record Executing(User Executive, Supply Cash, decimal Expiration) 
        
        : Transaction(Executive);

    public record Ordering(Executing Execution, decimal AtLeastAmountOfMarket, decimal AtMostAmountAmountOfMarket, Market Market, User Executive, Supply Cash, decimal Expiration) 
        
        : Executing(Executive, Cash, Expiration);

    // invoice matching market orders
    public record Invoicing(Ordering BidOrder, Ordering AskOrder, decimal BidAmount, decimal AskAmount, User Executive, Supply Cash, decimal Expiration) 
        
        : Executing(Executive, Cash, Expiration);

    // change custody in a invoiced matched market order
    public record Changing(Invoicing Invoice, decimal AssetAmount, User Executive, Supply Cash, decimal Expiration) 
        
        : Executing(Executive, Cash, Expiration);

    // receipt the change of custody in a matched market order
    public record Receipting(Changing Change, decimal CashAmount, User Executive, Supply Cash, decimal Expiration) 
        
        : Changing(Change.Invoice, Change.AssetAmount, Executive, Cash, Expiration);



    // bidding is a type of market order
    public record Bidding(Executing Execution, decimal AtLeastAmountOfMarket, decimal AtMostAmountAmountOfMarket, Market Market, User Executive, Supply Cash, decimal Expiration)

        : Ordering(Execution, AtLeastAmountOfMarket, AtMostAmountAmountOfMarket, Market, Executive, Cash, Expiration);

    // asking is a type of market order
    public record Asking(Executing Execution, decimal AtLeastAmountOfMarket, decimal AtMostAmountAmountOfMarket, Market Market, User Executive, Supply Cash, decimal Expiration)

        : Ordering(Execution, AtLeastAmountOfMarket, AtMostAmountAmountOfMarket, Market, Executive, Cash, Expiration);


    // buying is a type of invoice for a bidding market order
    public record Buying(Ordering BidOrder, Ordering AskOrder, decimal BidAmount, decimal AskAmount, User Executive, Supply Cash, decimal Expiration)

        : Invoicing(BidOrder, AskOrder, BidAmount, AskAmount, Executive, Cash, Expiration);

    // selling is a type of invoice for a asking market order
    public record Selling(Ordering BidOrder, Ordering AskOrder, decimal BidAmount, decimal AskAmount, User Executive, Supply Cash, decimal Expiration)

         : Invoicing(BidOrder, AskOrder, BidAmount, AskAmount, Executive, Cash, Expiration);


    // paying is a type of change in custody in a buying invoice for a bidding market order
    public record Paying(Invoicing Invoice, decimal AssetAmount, User Executive, Supply Cash, decimal Expiration)

        : Changing(Invoice, AssetAmount, Executive, Cash, Expiration);

    // cashing is a type of change in custody in a selling invoice for a asking market order
    public record Cashing(Invoicing Invoice, decimal AssetAmount, User Executive, Supply Cash, decimal Expiration)

        : Changing(Invoice, AssetAmount, Executive, Cash, Expiration);


    // expensing is a type of receipt in a paying change in custody in a buying invoice for a bidding market order
    public record Expensing(Changing Change, decimal CashAmount, User Executive, Supply Cash, decimal Expiration)

        : Receipting(Change, CashAmount, Executive, Cash, Expiration);

    // incoming is a type of receipt in a cashing change in custody in a selling invoice for an asking market order
    public record Incoming(Changing Change, decimal CashAmount, User Executive, Supply Cash, decimal Expiration)

        : Receipting(Change, CashAmount, Executive, Cash, Expiration);

 }
