// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using System.Globalization;

namespace Swopblock
{
    public class ExecutionModule
    {
        //**********************************//
        //* execution structure ************//

        MainStreams MainStream;

        //* execution structure ************//
        //**********************************//

        public void AddTransfer(TransferStates transfer)
        {

        }

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

            //start.BranchStreamState

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
        List<Contracts> Orders;

        public int Id;

        public static decimal GenesisCashSupply;
        public static decimal GenesisCashDemand;
        public static decimal GenesisCashLock;
    }

    public class MainStreams
    {
        //**********************************//
        //* execution structure ************//

        ExecutionModule OfExecutionModule;

        public List<BranchStreams> Branches;

        //* execution structure ************//
        //**********************************//

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

        public BranchStreams startAsset;


        public StreamListOfSupers Suppers;

        public void SetState(SimulationStates state)
        {
            startStreamId = state.MainStreamState.StreamId;
            startCashSupply = state.MainStreamState.CashSupply;
            startCashDemand = state.MainStreamState.CashDemand;
            startCashLock = state.MainStreamState.CashLock;

            startAsset = Branches[state.BranchStreamState.AssetId];

            

        }

        public void UpdateState()
        {
            foreach (var branch in Branches)
            {
                foreach(var contract in branch.Contracts)
                {
                    foreach(var transfer in contract.Transfers)
                    {
                        ;
                    }
                }
            }
        }
    }

    public class BranchStreams
    {
        //**********************************//
        //* execution structure ************//

        public BranchStreams OfMainStream;

        public List<Contracts> Contracts;

        //* execution structure ************//
        //**********************************//

        // Start
        public static int startAssetId;
        public static decimal startCashSupply;
        public static decimal startCashDemand;
        public static decimal startCashLock;

        public static decimal startAssetSupply;
        public static decimal startAssetDemand;

        public Contracts startOrder;

        // Stop
        public int stopAssetId;
        public decimal stopCashSupply;
        public decimal stopCashDemand;
        public decimal stopCashLock;
        public Contracts stopOrder;


        public void SetState(BranchStates state)
        {
            startAssetId = state.AssetId;
            startCashSupply = state.CashSupply;
            startCashDemand= state.CashDemand;
            
        }

        public void UpdateState()
        {
            stopAssetId = startAssetId;
        }

        public BranchStates GetState()
        {
            return null;  //stopState;
        }
    }

    public class BTC : BranchStreams { }

    public class ETH : BranchStreams { }


    public class Contracts
    {
        //**********************************//
        //* execution structure ************//

        public BranchStreams OfBranchStream;

        public List<Transfers> Transfers;

        //* execution structure ************//
        //**********************************//

        public static int startAssetId;
        public static decimal startCashSupply;
        public static decimal startCashDemand;
        public static decimal startCashLock;

        public static decimal startAssetSupply;
        public static decimal startAssetDemand;

        public Contracts CurrentOrder;
    }

    public class Transfers
    {
        //**********************************//
        //* execution structure ************//

        public Contracts OfContract;

        //* execution structure ************//
        //**********************************//


        // start
        public static int sourceAssetId;
        public static decimal sourceCashSupplyTransfer;
        public static decimal sourceCashDemandTransfer;
        public static decimal sourceCashExpiration;

        public static decimal sourceAssetSupplyTransfer;
        public static decimal sourceAssetDemandTransfer;

        // stop
        public static int sinkAssetId;
        public static decimal sinkCashSupplyTransfer;
        public static decimal sinkCashDemandTransfer;
        public static decimal sinkCashExpiration;

        public static decimal sinkAssetSupplyTransfer;
        public static decimal sinkAssetDemandTransfer;

        public void UpdateState()
        {
            //if (sourceCashSupplyTransfer == )
        }

        public void BidUpdate()
        {

        }

        public bool IsBid()
        {
            return false;
        }

        public bool IsAsk()
        {
            return false;
        }

        public bool IsSell()
        {
            return false;
        }

        public bool IsBuy()
        {
            return false;
        }
        
        public bool IsInvest()
        {
            return false;
        }

        public bool IsDivest()
        {
            return true;
        }
    }
}
