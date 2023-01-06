// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using Swopblock;
//using SimulationUnitTesting;
using Swopblock.Intentions;
using Swopblock.Intentions.Utilities;
using System.Globalization;
using Swopblock.Demo;
using swop.Demo;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Swopblock.Stack.IncentiveLayer;
using Swopblock.Simulation;
//using Swopblock.API;
using Swopblock.API.Process;
using Swopblock.API.Data;
using Swopblock.API.Application;

// Put Simulation Here
// Put Simulation Here
// Put Simulation Here

var sim = new Sim();

string order, invoice, change, receipt;

Console.WriteLine("Hello Swopblock Sim!");

while (true)
{
    Console.WriteLine("We are confirming 123 BTC in your account 2387 and pending 73 SWOBL in your account 2387.");

    order = Console.ReadLine();

    order = "I am bidding at most 10 SWOBL in my supply 1028943 in order for at least 1 BTC in the market at market volume 293847 SWOBL" +
                "and the order is good until the maket volume reaches 128923 SWOBL using my signature 192837.";

    Console.WriteLine("Please commit your order:");
    Console.WriteLine("    " + order);

    invoice = "We are invoicing 10 SWOBL in your supply 1028943 in order for 2 BTC in their supply 40293 at market volume 209834 SWOBL.";

    change = "We are changing 10 SWOBL in your supply 1028942 in demand for 2 BTC in their supply 40293 at market volume 309843 SWOBL.";

    receipt = "We are receipting 10 SWOBL in your supply 1028942 in demand for 2 BTC in their supply 40293 at market volume 309843 SWOBL."
}

Console.WriteLine("Goodbye Swopblock Sim!");

// Put Simulation Here
// Put Simulation Here
// Put Simulation Here

public class Sim
{
    APP[] apps;

    CARRIER[] carrier;
    public Sim()
    {
        apps = new APP[1000];

        for (int i = 0; i < apps.Length; i++)
        {
            apps[i] = new APP();

            apps[i].CORE = new CORE();

            apps[i].CORE.APP = apps[i];

            for (int j = 0)
        }
    }

    public void MakeRandomOrder()
    {
        for (int i = 0; i < orderCount; i++)
        {

        }
    }
}


namespace Swopblock
{
      public class Program 
    {
        public static string[] programAgrs;

        public static string[] simulationArgs;

        public static string[] consensusArgs;

        public static string[] executionArgs;

        public enum AssetTags { SWOBL = 0, BTC = 1, ETH = 2 };

        /* **************************************************************** */
        //   Step 1: Get interpretation working
        //   Step 2: Get simulation states updating 
        //   Step 3: Show user updated simulation state
        /* **************************************************************** */

        public static void MainTBD(string[] args)
        {
            TestStream();

            static void TestStream()
            {
                Streams stream = new Streams((ExecutionModule)null);

                string line;

                while((line = Console.ReadLine()) != null)
                {
                    stream.SetState(StreamStates.ParseFromTabbedLine(ref line));

                    stream.UpdateState();

                    Console.WriteLine(stream.GetState());
                }
            }

            Console.WriteLine("Hello, Swopblock World!");

            programAgrs = args;

            GetModuleArgs(programAgrs);

            var simulation = new SimulationModule(simulationArgs, consensusArgs, executionArgs);

            simulation.BuildSimultion(1, 1, 1, 1, 1, 1);

            ///////////////////////////////////////////////////////////
            DemoPrompt dp = new DemoPrompt();
            dp.Run();
            ///////////////////////////////////////////////////////////
            

            SimulationStates simulationState;

            string line = Console.ReadLine();
            
            line = SimulationStates.FromTest().ParseToTabbedLine();

            /* **************************************************************** */
            /* **************************************************************** */
            /* **************************************************************** */

            while (line != null)
            {
                simulationState = SimulationStates.ParseFromLine(line);

                simulation.PokeInEntryInput(simulationState);

                while ((simulationState = simulation.PeekAtExitOutput()) != null)
                {
                    Console.WriteLine(simulationState.ParseToTabbedLine());
                }

                line = Console.ReadLine();

                line = SimulationStates.FromTest().ParseToTabbedLine(); 
            }

            /* **************************************************************** */
            /* **************************************************************** */
            /* **************************************************************** */

            IntentionDemonstrationRegion();

            static void IntentionDemonstrationRegion()
            {
                #region Intention Demonstration

                IntentionTree Tree = DemoWeb.GetTree();

                SwopblockModule client = new SwopblockModule(null, null);

                string errMessage = "Error";

                string userInput = Console.ReadLine();

                if (userInput != null)
                {
                    if (Tree.Validate(userInput))
                    {
                        //client.PokeInEntryInput(LiquidityStreamStates.ParseFromIntention(userInput));
                        client.PokeInEntryInput(SimulationStates.ParseFromIntention(userInput));
                        var output = client.PeekAtExitOutput();
                        Console.WriteLine(output.ParseToTabbedLine());
                    }
                    else
                    {
                        Console.WriteLine(errMessage);
                    }
                }
                else
                {
                    Console.WriteLine(errMessage);
                }

                // start network state
                //ContractStreamStates NetworkContractState = new ContractStreamStates(0, null, null, null);
                // get user contract
                //ContractStream state = DemoPrompt.Run();
                // update network state
                //NetworkContractState.Add(state);

                #endregion
            }
        }

        public static void GetModuleArgs(string[] args)
        {
            int simulationArgsIndex = 0, consensusArgsIndex = 0, executionArgsIndex = 0;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "simulation")
                {
                    simulationArgsIndex = i;
                }

                if (args[i] == "consensus")
                {
                    consensusArgsIndex = i;
                }

                if (args[i] == "execution")
                {
                    executionArgsIndex = i;
                }
            }

            simulationArgs = args.Skip(simulationArgsIndex).ToArray().Take(consensusArgsIndex - simulationArgsIndex).ToArray();

            consensusArgs = args.Skip(consensusArgsIndex).ToArray().Take(executionArgsIndex - consensusArgsIndex).ToArray();

            executionArgs = args.Skip(executionArgsIndex).ToArray().Take(args.Length - executionArgsIndex).ToArray();

        }
    }
}







#region Input Output Data Types

public class SimulationStates
{
    public StreamStates StreamState;

    public BranchStates BranchState;

    public AddressStates AddressState;

    public TransferStates TransferState;

    public ConsensusStates ConsensusState;

    public static Random R = new Random();

    public static SimulationStates Empty
    {
        get
        {
            return new SimulationStates(
                StreamStates.Empty,
                BranchStates.Empty,
                AddressStates.Empty,
                TransferStates.Empty,
                ConsensusStates.Empty);
        }
    }

    public SimulationStates()
    {

    }

    public SimulationStates(StreamStates liquidityStreamState, BranchStates assetStreamState, AddressStates contractStreamState, TransferStates liquidityTransferState, ConsensusStates consensusState)
    {
        StreamState = liquidityStreamState;
        BranchState = assetStreamState;
        AddressState = contractStreamState;
        TransferState = liquidityTransferState;
        ConsensusState = consensusState;
    }

    public static bool CheckTabbedLineFormat(string line)
    {
        var tabs = line.Split('\t');

        return line.Split('\t').Length == 28;
    }

    public bool IsEqual(SimulationStates one)
    {
        return this.StreamState.IsEqual(one.StreamState) &&
            this.BranchState.IsEqual(one.BranchState) &&
            this.AddressState.IsEqual(one.AddressState) &&
            this.TransferState.IsEqual(one.TransferState) &&
            this.ConsensusState.IsEqual(one.ConsensusState);
    }

    public SimulationStates Add(SimulationStates state)
    {
        Report rpt = new Report();

        SimulationStates nState = new SimulationStates();

        nState.BranchState = this.BranchState.Add(state.BranchState);
        nState.AddressState = this.AddressState.Add(state.AddressState);
        nState.StreamState = this.StreamState.Add(state.StreamState);
        //nState.ConsensusState = this.ConsensusState.Add(state.ConsensusState, state.ContractState.CashSupply);
        nState.TransferState = this.TransferState.Add(state.TransferState);

        //return new DemoWeb.StateBag
        //{
        //    Report = rpt,
        //    nState = nState
        //};

        return nState;
    }

    public static SimulationStates FromTest()
    {
        //var LiquidityStreamState = new StreamStates(1, 2, 3, 4, 5);

        //var AssetStreamState = new BranchStates(5, 6, 7, 8, 9, 10, 11);

        //var ContractStreamState = new AddressStates(11, 12, 13, 14, 15, 16, 17);

        //var LiquidityTransferState = new TransferStates(17, (KindsOfTranfer)18, 19, 20);

        var ConsensusState = new ConsensusStates(23, 24, 25, 26, 27, 28);

        //var state = new SimulationStates(LiquidityStreamState, AssetStreamState, ContractStreamState, LiquidityTransferState, ConsensusState);

        //return state;

        return null;
    }

    public static SimulationStates FromRandom()
    {
        //var LiquidityStreamState = new StreamStates(R.Next(NumberOfLiquityStreams), R.Next(), R.Next(), R.Next());

        //var AssetStreamState = new BranchStates(R.Next(NumberOfAssetStreams), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        //var ContractStreamState = new AddressStates(R.Next(NumberOfContractStreamStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        //var LiquidityTransferState = new TransferStates(R.Next(NumberOfContractTransferStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var ConsensusState = new ConsensusStates(R.Next(), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        //var state = new SimulationStates(LiquidityStreamState, AssetStreamState, ContractStreamState, LiquidityTransferState, ConsensusState);

        //return state;

        return null;
    }

    public static SimulationStates ParseFromTabbedLine(string line)
    {
        var state = new SimulationStates();

        state.StreamState = StreamStates.ParseFromTabbedLine(ref line);

        state.BranchState = BranchStates.ParseFromTabbedLine(ref line);

        state.AddressState = AddressStates.ParseFromTabbedLine(ref line);

        state.TransferState = TransferStates.ParseFromTabbedLine(ref line);

        state.ConsensusState = ConsensusStates.ParseFromTabbedLine(ref line);

        return state;
    }
    public string ParseToTabbedLine()
    {
        var line = "";

        line += StreamState.ParseToTabbedLine();

        line += BranchState.ParseToTabbedLine();

        line += AddressState.ParseToTabbedLine();

        line += TransferState.ParseToTabbedLine();

        line += ConsensusState.ParseToTabbedLine();

        return line;
    }

    public static SimulationStates ParseFromIntention(string intention)
    {
        var state = new SimulationStates();

        state.StreamState = StreamStates.Empty;

        state.BranchState = BranchStates.ParseFromIntention(intention);

        state.AddressState = AddressStates.ParseFromIntention(intention);

        state.TransferState = TransferStates.ParseFromIntention(intention);

        state.ConsensusState = ConsensusStates.Empty;

        return state;
    }

    public static SimulationStates ParseFromLine(string line)
    {
        if (line[0].ToString().ToLower() == "i") return SimulationStates.ParseFromIntention(line);

        return SimulationStates.ParseFromTabbedLine(line);
    }

}

// top level
public record StreamStates(int StreamId,  decimal CashBalance, decimal CashVolume)
{
    public static StreamStates Empty { get { return new StreamStates(0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{StreamId}\t{CashBalance}\t{CashVolume}\t";
    }

    public static StreamStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 4);

        line = fields[3];

        return new StreamStates
        (
            int.Parse(fields[0]),
            decimal.Parse(fields[1]),
            decimal.Parse(fields[2])
        );
    }

    public StreamStates Add(StreamStates state)
    {
        return null;
        //return new StreamStates
        //    (
        //    state.StreamId + this.StreamId, 
        //    state.CashInput + this.CashSupply, 
        //    state.CashDemand + this.CashDemand, 
        //    state.CashLock + this.CashLock
        //    );       
    }

    public bool IsEqual(StreamStates state)
    {
        //if (state.StreamId != StreamId) return false;
        //if (state.CashInput != CashInput) return false;
        //if (state.CashOutput != CashOutput) return false;
        //if (state.CashRetained != CashRetained) return false;
        //if (state.CashLock != CashLock) return false;

        return true;
    }
}

public record BranchStates(int BranchId, decimal AssetBalance, decimal AssetVolume)
{
    public static BranchStates Empty { get { return new BranchStates(0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{BranchId}\t{AssetBalance}\t{AssetVolume}";
    }

    public static BranchStates ParseFromIntention(string intention)
    {
        DemoWeb.DataBag bag = DemoWeb.DefaultParse(intention);

        return null; // new BranchStates(bag.assetID, 0, 0, 0, 0, 0);
    }

    public static BranchStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 3);

        line = fields[2];

        return new BranchStates
        (
            int.Parse(fields[0]),

            decimal.Parse(fields[1]),

            decimal.Parse(fields[2])
        );
    }

    public BranchStates Add(BranchStates state)
    {
        if (this.BranchId != state.BranchId) return this;

        // remember to check if cashlocks are compatable

        return null;
        //return new BranchStates
        //    (
        //        state.BranchId,
        //        state.CashSupply + this.CashSupply,
        //        state.CashDemand + this.CashDemand,
        //        state.CashLock,
        //        state.AssetSupply + this.AssetSupply,
        //        state.AssetDemand + this.AssetDemand
        //    );
    }

    public bool IsEqual(BranchStates state)
    {
        //if (state.BranchId != BranchId) return false;
        //if (state.CashInput != CashInput) return false;
        //if (state.CashOutput != CashOutput) return false;
        //if (state.CashRetained != CashRetained) return false;
        //if (state.AssetInput != AssetInput) return false;
        //if (state.AssetOutput != AssetOutput) return false;
        //if (state.AssetRetained != AssetRetained) return false;

        return true;
    }
}

public record AddressStates(int AddressId, decimal CashBalance, decimal CashLocked, decimal AssetBalance, decimal AssetReserved, decimal CashLockExpiration)
{
    public static AddressStates Empty { get { return new AddressStates(0, 0, 0, 0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{AddressId}\t{CashBalance}\t{CashLocked}\t{AssetBalance}\t{AssetReserved}\t{CashLockExpiration}\t";
    }

    public static AddressStates ParseFromIntention(string intention)
    {
        DemoWeb.DataBag bag = DemoWeb.DefaultParse(intention);

        return null;// new AddressStates(1, bag.cashSup, bag.cashDem, bag.cashLock, bag.assetSup, bag.assetDem);
    }

    public static AddressStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 6);

        line = fields[5];

        return new AddressStates
        (
            int.Parse(fields[0]),
            decimal.Parse(fields[1]),
            decimal.Parse(fields[2]),
            decimal.Parse(fields[3]),
            decimal.Parse(fields[4]),
            decimal.Parse(fields[5])
        );
    }

    public bool IsEqual(AddressStates state)
    {
        //if (state.AddressId != AddressId) return false;
        //if (state.CashInput != CashInput) return false;
        //if (state.CashOutput != CashOutput) return false;
        //if (state.CashRetained != CashRetained) return false;
        //if (state.AssetInput != AssetInput) return false;
        //if (state.AssetOutput != AssetOutput) return false;
        //if (state.AssetRetained != AssetRetained) return false;

        return true;
    }

    public AddressStates Add(AddressStates state)
    {
        return null; // new AddressStates
            //(
            //state.AddressId,
            //state.CashSupply + this.CashSupply,
            //state.CashDemand + this.CashDemand,
            //state.CashLock + this.CashLock,
            //state.AssetSupply + this.AssetSupply,
            //state.AssetDemand + this.AssetDemand
            //);
    }
}

public record TransferStates(int TransferId, decimal OutputCashBalance, decimal OutputCashVolume, decimal OutputAssetBalance, decimal OutputAssetVolume, int ErrorCode)
{
    public static TransferStates Empty { get { return new TransferStates(0, 0, 0, 0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{TransferId}\t{OutputCashBalance}\t{OutputCashVolume}\t{OutputAssetBalance}\t{OutputAssetVolume}\t{ErrorCode}\t";
    }

    public static TransferStates ParseFromIntention(string intention)
    {


        DemoWeb.DataBag bag = DemoWeb.DefaultParse(intention);

        return null;// new TransferStates(1, bag.cashSup, bag.cashDem, bag.cashLock, bag.assetSup);//, bag.assetDem);
    }
    public static TransferStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 5);

        line = fields[5];

        return new TransferStates
        (
            int.Parse(fields[0]),

            decimal.Parse(fields[1]),

            decimal.Parse(fields[2]),

            decimal.Parse(fields[3]),

            decimal.Parse(fields[4]),

            int.Parse(fields[5])
        );
    }

    public bool IsEqual(TransferStates state)
    {
        //if (state.TransferId != TransferId) return false;
        //if (state.AssetSupply != AssetSupply) return false;
        //if (state.AssetDemand != AssetDemand) return false;
        //if (state.CashSupply != CashSupply) return false;
        //if (state.CashDemand != CashDemand) return false;
        //if (state.CashLock != CashLock) return false;

        return true;
    }

    public TransferStates Add(TransferStates state)
    {
        return null;
        //return new TransferStates
        //    (
        //        state.TransferId,
        //        state.CashSupply + this.CashSupply,
        //        state.CashDemand + this.CashDemand,
        //        state.CashLock,
        //        state.AssetSupply + this.AssetSupply,
        //        state.AssetDemand + this.AssetDemand
        //    );
    }
}

public record ConsensusStates(int ConsensusId, decimal Safety, Int64 ProofOfStake, Int64 ProofOfWork, int SuperConsensusId, int RelayConsensusId)
{
    public static ConsensusStates Empty { get { return new ConsensusStates(0, 0, 0, 0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{ConsensusId}\t{Safety}\t{ProofOfStake}\t{ProofOfWork}\t{SuperConsensusId}\t{RelayConsensusId}\n";
    }
    public static ConsensusStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', '\n');

        line = "\n";

        return new ConsensusStates
        (
            int.Parse(fields[0]),
            decimal.Parse(fields[1]),
            Int64.Parse(fields[2]),
            Int64.Parse(fields[3]),
            int.Parse(fields[4]),
            int.Parse(fields[5])
        );
    }

    static Random r = new Random();
    public ConsensusStates RandomStream()
    {
        return new ConsensusStates(r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next());
    }

    public bool IsEqual(ConsensusStates one)
    {
        // add consensus comparision
        return true;
    }

    public ConsensusStates Add(ConsensusStates state, decimal volume)
    {
        return this; // needs to be expanded
    }
}
#endregion
