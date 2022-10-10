// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using System.Globalization;

namespace Swopblock
{
    public class ExecutionModule
    {
        //**********************************//
        //* execution structure ************//

        RelayChains ProcessingRelayChain = new RelayChains();

        BlockChains ProcessingBlockChain = new BlockChains();

        Addresses ProcessingAddress = new Addresses();

        Transfers ProcessingTransfer = new Transfers();

        //* execution structure ************//
        //**********************************//

        public void SetTransferState(TransferStates transfer)
        {

        }

        public void SetContractState(OrderStates contract)
        {

        }

        public void SetBranchState(BranchStates branch)
        {

        }

        public void SetStreamState(StreamStates stream)
        {
            //Stream.SetState()
        }

        public void SetState(StreamStates streamState, BranchStates branchState, OrderStates orderState, TransferStates transferState)
        {
            ProcessingRelayChain.SetState(streamState);

            ProcessingBlockChain.SetState(branchState);

            ProcessingAddress.SetState(orderState);

            ProcessingTransfer.SetState(transferState);
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
        List<Addresses> Orders;

        public int Id;

        public static decimal GenesisCashSupply;
        public static decimal GenesisCashDemand;
        public static decimal GenesisCashLock;
    }

    public class RelayChains
    {
        //**********************************//
        //* execution structure ************//

        ExecutionModule OfExecutionModule;

        public List<BlockChains> Chains;

        //* execution structure ************//
        //**********************************//

        // Genesis
        public int startStreamId;
        public decimal startCashSupply;
        public decimal startCashDemand;
        public decimal startCashLock;
        
        //Current
        public int stopStreamId;
        public decimal stopCashSupply;
        public decimal stopCashDemand;
        public decimal stopCashLock;

        public StreamListOfSupers Suppers;

        public void SetState(StreamStates state)
        {
            startCashSupply = state.CashSupply;
            startCashDemand = state.CashDemand;
            startCashLock = state.CashLock;

            startStreamId = state.StreamId;
        }

        public void UpdateState()
        {
            foreach (var branch in Chains)
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

    public class BlockChains
    {
        //**********************************//
        //* execution structure ************//

        public BlockChains OfMainStream;

        public List<Addresses> Contracts;

        //* execution structure ************//
        //**********************************//

        // Start
        public int startAssetId;
        public decimal startCashSupply;
        public decimal startCashDemand;
        public decimal startCashLock;

        public decimal startAssetSupply;
        public decimal startAssetDemand;

        // Stop
        public int stopAssetId;
        public decimal stopCashSupply;
        public decimal stopCashDemand;
        public decimal stopCashLock;
        public Addresses stopOrder;


        public void SetState(BranchStates state)
        {
            startAssetId = state.AssetId;

            startCashSupply = state.CashSupply;
            startCashDemand = state.CashDemand;
            startCashLock = state.CashLock;

            startAssetSupply = state.AssetSupply;
            startAssetDemand = state.AssetDemand;
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

    public class BTC : BlockChains { }

    public class ETH : BlockChains { }


    public class Addresses
    {
        //**********************************//
        //* execution structure ************//

        public BlockChains OfBranchStream;

        public List<Transfers> Transfers;

        //* execution structure ************//
        //**********************************//

        public int startAssetId;
        public decimal startCashSupply;
        public decimal startCashDemand;
        public decimal startCashLock;

        public decimal startAssetSupply;
        public decimal startAssetDemand;

        public Addresses CurrentOrder;

        public void SetState(OrderStates state)
        {
            //startAssetId = ?
            //
            startAssetSupply = state.AssetSupply;
        }
    }

    public class Transfers
    {
        //**********************************//
        //* execution structure ************//

        public Addresses InputContract, TransferContract, OutputContract;

        //* execution structure ************//
        //**********************************//


        // start
        public int inputContractId;
        public decimal inputCashSupplyTransfer;
        public decimal inputCashDemandTransfer;
        public decimal inputCashExpiration;

        public decimal inputAssetSupplyTransfer;
        public decimal inputAssetDemandTransfer;

        // stop
        public int outputContractId;
        public decimal outputCashSupplyTransfer;
        public decimal outputCashDemandTransfer;
        public decimal outputCashExpiration;

        public decimal outputAssetSupplyTransfer;
        public decimal outputAssetDemandTransfer;

        public void SetState(TransferStates state)
        {

        }

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
