// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using System.Globalization;

namespace Swopblock
{
    public class ExecutionModule
    {
        Stream Stream;




        public ExecutionModule(params string[] args)
        {

        }

        public SimulationStates Run(SimulationStates state)
        {
            return state;
        }

        public int StepState(SimulationStates start, out SimulationStates stop)
        {
            stop = start;

            return 0;
        }
    }

    public class StreamListOfSupers
    {
        List<SuperListOfRelays> Supers;

        int StreamId;

        int CurrentSuperId;
        decimal GenesisCashSupply, CurrentCashSupply;
        decimal GenesisCashDemand, CurrentCashDemand;
        decimal GenesisCashLock, CurrentCashLock;
    }
    public class SuperListOfRelays
    {
        List<RelayListOfOrders> Relays;

        public int GenesisAssetId;

        public static decimal GenesisCashSupply;
        public static decimal GenesisCashDemand;
        public static decimal GenesisCashLock;
    }

    public class RelayListOfOrders
    {
        List<Orders> Orders;

        public int Id;

        public static decimal GenesisCashSupply;
        public static decimal GenesisCashDemand;
        public static decimal GenesisCashLock;
    }

    public class Stream
    {
        // Genesis
        public static int startStreamId;
        public static decimal startCashSupply;
        public static decimal startCashDemand;
        public static decimal startCashLock;
        
        //Current
        public int stopStreamId;
        public decimal stopCashSupply;
        public decimal stopCashDemand;
        public decimal stopCashLock;

        public Assets startAsset;

        public List<Assets> Assets; 

        public StreamListOfSupers Suppers;

        public void SetState(SimulationStates state)
        {
            startStreamId = state.LiquidityStreamState.StreamId;
            startCashSupply = state.LiquidityStreamState.CashSupply;
            startCashDemand = state.LiquidityStreamState.CashDemand;
            startCashLock = state.LiquidityStreamState.CashLock;

            startAsset = Assets[state.AssetStreamState.AssetId];

            // I AM HERE.

        }
    }

    public class Assets
    {
        // Start
        public static int startAssetId;
        public static decimal startCashSupply;
        public static decimal startCashDemand;
        public static decimal startCashLock;

        public static decimal startAssetSupply;
        public static decimal startAssetDemand;

        public Orders startOrder;

        // Stop
        public int stopAssetId;
        public decimal stopCashSupply;
        public decimal stopCashDemand;
        public decimal stopCashLock;
        public Orders stopOrder;


        public void SetState(AssetStreamStates state)
        {
            startAssetId = state.AssetId;
            startCashSupply = state.CashSupply;
            startCashDemand= state.CashDemand;
            
        }
    }

    public class BTC : Assets { }

    public class ETH : Assets { }


    public class Orders
    {
        // Genesis
        public static int GenesisAssetId;
        public static decimal GenesisCashSupply;
        public static decimal GenesisCashDemand;
        public static decimal GenesisCashLock;

        // Current
        public int CurrentAssetId;
        public decimal CurrentCashSupply;
        public decimal CurrentCashDemand;
        public decimal CurrentCashLock;

        public Orders CurrentOrder;
    }
}
