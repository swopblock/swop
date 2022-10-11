// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace Swopblock
{
    public class ExecutionModule
    {
        //**********************************//
        //* execution structure ************//

        public ConsensusModule MyConsensusModule;

        public Streams GenesisStream, MyStream;

        public List<Streams> MyStreams;

        //* execution structure ************//
        //**********************************//

        public ExecutionModule(ConsensusModule myConsensusModule)
        {
            MyConsensusModule = myConsensusModule;

            GenesisStream = MyStream = new Streams(this);
            
            MyStreams = new List<Streams>();    

            MyStreams.Add(GenesisStream);
        }

        public void SetState(StreamStates streamState, BranchStates branchState, AddressStates addressState, TransferStates transferState)
        {
            var stream = MyStreams[streamState.StreamId];

            var branch = stream.MyBranches[branchState.BranchId];

            var address = branch.MyAddresses[addressState.AddressId];

            var transfer = address.MyTransfers[transferState.TransferId];

            
            stream.SetState(streamState);

            branch.SetState(branchState);

            address.SetState(addressState);

            transfer.SetState(transferState);
        }

        public void UpdateState()
        {
            MyStream.UpdateState();
        }

        //public void GetState(out StreamStates streamState, out BranchStates branchState, out AddressStates orderState, out TransferStates transferState)
        //{

        //}

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

    public class Streams
    {
        //**********************************//
        //* execution structure ************//

        public ExecutionModule MyExecutionModule;

        public Branches GenesisBranch, MyBranch;

        public List<Branches> MyBranches;

        //* execution structure ************//
        //**********************************//

        public Streams(ExecutionModule myExecutionModule)
        {
            MyExecutionModule = myExecutionModule;

            GenesisBranch = MyBranch = new Branches(this);

            MyBranches = new List<Branches>();

            MyBranches.Add(GenesisBranch);
        }

        // start state
        public int startStreamId;
        public decimal startCashSupply;
        public decimal startCashDemand;
        public decimal startCashLock;
        
        // stop state
        public int stopStreamId;
        public decimal stopCashSupply;
        public decimal stopCashDemand;
        public decimal stopCashLock;

        public void SetState(StreamStates state)
        {
            startCashSupply = state.CashSupply;
            startCashDemand = state.CashDemand;
            startCashLock = state.CashLock;

            startStreamId = state.StreamId;
        }

        public void UpdateState()
        {
            MyBranch.UpdateState();
        }
    }
    
    public class Branches
    {
        //**********************************//
        //* execution structure ************//

        public Streams MyStream;

        public Addresses GenesisAddress, MyAddress;

        public List<Addresses> MyAddresses;

        //* execution structure ************//
        //**********************************//

        public Branches(Streams myStream)
        {
            MyStream = myStream;

            GenesisAddress = MyAddress = new Addresses(this);

            MyAddresses = new List<Addresses>();

            MyAddresses.Add(GenesisAddress);
        }

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

        public decimal stopAssetSupply;
        public decimal stopAssetDemand;

        public void SetState(BranchStates state)
        {
            startAssetId = state.BranchId;

            startCashSupply = state.CashSupply;
            startCashDemand = state.CashDemand;
            startCashLock = state.CashLock;

            startAssetSupply = state.AssetSupply;
            startAssetDemand = state.AssetDemand;
        }

        public void UpdateState()
        {
            MyAddress.UpdateState();
        }

        public BranchStates GetState()
        {
            return null;  //stopState;
        }
    }

    public class Addresses
    {
        //**********************************//
        //* execution structure ************//

        public Branches MyBranch;

        public Transfers GenesisTransfer, MyTransfer;

        public List<Transfers> MyTransfers;

        //* execution structure ************//
        //**********************************//

        public Addresses(Branches myBranch)
        {
            MyBranch = myBranch;

            GenesisTransfer = MyTransfer = new Transfers(this);

            MyTransfers = new List<Transfers>();

            MyTransfers.Add(GenesisTransfer);
        }

        public int startAssetId;
        public decimal startCashSupply;
        public decimal startCashDemand;
        public decimal startCashLock;

        public decimal startAssetSupply;
        public decimal startAssetDemand;

        public int stopAssetId;
        public decimal stopCashSupply;
        public decimal stopCashDemand;
        public decimal stopCashLock;

        public decimal stopAssetSupply;
        public decimal stopAssetDemand;

        public void SetState(AddressStates state)
        {
            startAssetId = state.AddressId;

            startCashSupply = state.CashSupply;
            startCashDemand = state.CashDemand;
            startCashLock = state.CashLock;
            
            
            startAssetSupply = state.AssetSupply;
            startAssetDemand = state.AssetDemand;
        }

        public void UpdateState()
        {
            MyTransfer.UpdateState();
        }
    }

    public class Transfers
    {
        //**********************************//
        //* execution structure ************//

        public Addresses MyAddress;

        //* execution structure ************//
        //**********************************//

        public Transfers(Addresses myAddress)
        {
            MyAddress = myAddress;
        }

        // start state
        public int myAddressId, myTransferId;

        public decimal startCashLock;
        public decimal startCashSupply;
        public decimal startAssetSupply;

        public decimal stopAssetDemand;
        public decimal stopCashDemand;
        public decimal stopCashLock;

        public void SetState(TransferStates state)
        {
            myTransferId = state.TransferId;
            startCashLock = state.CashLock;
            startCashSupply = state.CashSupply;
            startAssetSupply = state.AssetSupply;
        }

        public void UpdateState()
        {
            var C = MyAddress.MyBranch.MyStream.startCashSupply + MyAddress.MyBranch.startCashSupply + MyAddress.startCashSupply + startCashSupply;

            var A = MyAddress.MyBranch.startAssetSupply + MyAddress.startAssetSupply + startAssetSupply;

            var dC = startCashSupply;

            var dA = A - (C - dC) * A / C;

            stopCashDemand = dC;
            stopAssetDemand = dA;

            MyAddress.stopCashSupply -= dC;
            MyAddress.stopAssetSupply -= dA;

            MyAddress.MyBranch.stopCashSupply -= dC;
            MyAddress.MyBranch.stopAssetSupply -= dA;

            MyAddress.MyBranch.MyStream.stopCashSupply -= dC;
            MyAddress.MyBranch.MyStream.stopCashSupply -= dA;

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



    //public class RelayChain : Streams { }

    //public class BlockChain : Branches { }

    //public class Orders { Addresses Address; Transfers Transfer; }

    //public class Filling : Transfers { }

    //public class Fills : Addresses { }

    //public class BTC : BlockChain { }

    //public class ETH : BlockChain { }

}
