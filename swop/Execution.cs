// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

namespace Swopblock
{
    public class ExecutionModule
    {
        //**********************************//
        //* execution structure ************//

        public ConsensusModule MyConsensusModule;

        public Streams MyStream;

        public List<Streams> MyStreams;

        //* execution structure ************//
        //**********************************//

        public ExecutionModule(ConsensusModule myConsensusModule)
        {
            MyConsensusModule = myConsensusModule;

            MyStream = new Streams(this);
            
            MyStreams = new List<Streams>();    

            MyStreams.Add(MyStream);
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

        public Branches MyBranch;

        public List<Branches> MyBranches;

        //* execution structure ************//
        //**********************************//

        public Streams(ExecutionModule myExecutionModule)
        {
            MyExecutionModule = myExecutionModule;

            MyBranch = new Branches(this);

            MyBranches = new List<Branches>();

            MyBranches.Add(MyBranch);
        }

        // start state
        public StreamStates Start, Stop;

        public void SetState(StreamStates state)
        {
            Start = state;
        }

        public void UpdateState()
        {
            MyBranch.UpdateState();
        }

        public StreamStates GetState()
        {
            return Stop;
        }
    }
    
    public class Branches
    {
        //**********************************//
        //* execution structure ************//

        public Streams MyStream;

        public Addresses MyAddress;

        public List<Addresses> MyAddresses;

        //* execution structure ************//
        //**********************************//

        public Branches(Streams myStream)
        {
            MyStream = myStream;

            MyAddress = new Addresses(this);

            MyAddresses = new List<Addresses>();

            MyAddresses.Add(MyAddress);
        }

        // Start
        public BranchStates Start, Stop;

        public void SetState(BranchStates state)
        {
            Start = state;
        }

        public void UpdateState()
        {
            MyAddress.UpdateState();
        }

        public BranchStates GetState()
        {
            return Stop;
        }
    }

    public class Addresses
    {
        //**********************************//
        //* execution structure ************//

        public Branches MyBranch;

        public Transfers MyTransfer;

        public List<Transfers> MyTransfers;

        //* execution structure ************//
        //**********************************//

        public Addresses(Branches myBranch)
        {
            MyBranch = myBranch;

            MyTransfer = new Transfers(this);

            MyTransfers = new List<Transfers>();

            MyTransfers.Add(MyTransfer);
        }

        public AddressStates Start, Stop;

        public void SetState(AddressStates state)
        {
            Start = state;
        }

        public void UpdateState()
        {
            MyTransfer.UpdateState();
        }

        public AddressStates GetState()
        {
            return Stop;
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


        TransferStates Start, Stop;

        public void SetState(TransferStates state)
        {
            Start = state;
        }

        public TransferStates GetState()
        {
            return Stop;
        }

        public void UpdateState()
        {
            int ErrorCode = 0;

            // determine expiration
            if (MyAddress.Start.CashLockExpiration < (MyAddress.MyBranch.MyStream.Start.CashVolume + MyAddress.Start.CashLocked))
            {
                ErrorCode |= 1;
            }

            // determine cash imbalances
            if (MyAddress.Start.CashBalance > MyAddress.Start.CashLocked)
            {
                ErrorCode |= 2;
            }

            // determine asset imbalances
            if (MyAddress.Start.AssetBalance > MyAddress.Start.AssetReserved)
            {
                ErrorCode |= 4;
            }

            // determine cash transfer imbalance
            if (MyAddress.Start.CashLocked < Start.CashUnlocked)
            {
                ErrorCode |= 8;
            }

            // determine asset transfer imbalance
            if (MyAddress.Start.AssetReserved < Start.AssetRelease)
            {
                ErrorCode |= 16;
            }

            // determine stream cash imbalance
            if (MyAddress.MyBranch.MyStream.Start.CashBalance < 1)
            {
                ErrorCode |= 32;
            }

            // determine branch asset imbalance
            if (MyAddress.MyBranch.Start.AssetBalance < 1)
            {
                ErrorCode |= 32;
            }

            // determine cash available
            var C = MyAddress.MyBranch.MyStream.Start.CashBalance;

            // determine cash locked
            var cI = MyAddress.Start.CashLocked;

            // determine cash unlocked
            var cO = Start.CashUnlocked;

            // determine asset available
            var A = MyAddress.MyBranch.Start.AssetBalance;

            // determine asset reserve
            var aI = MyAddress.Start.AssetReserved;

            // determine asset release
            var aO = Start.AssetRelease;

            // determine valid cash unlocked and asset released 
            if ((C * cI) * (A * aI) != (C * cO) * (A * aO))
            {
                ErrorCode |= 64;
            }

            Stop = Start with { ErrorCode = ErrorCode };

            return;
        }
    }
}
