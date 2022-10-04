// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using Swopblock;
using SimulationUnitTesting;
using Swopblock.Intentions;
using Swopblock.Intentions.Utilities;
using System.Globalization;
using Swopblock.Demo;
using swop.Demo;
using System.Data.Common;
using System.Diagnostics.Contracts;
using static Swopblock.Program;
using System.Runtime.CompilerServices;

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

        public static void Main(string[] args)
        {
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
public record DigitalEntry(decimal Supply, decimal Demand, decimal Lock)
{ 
}

public record DigitalCash(decimal Supply, decimal Demand, decimal Lock) : DigitalEntry(Supply, Demand, Lock)
{
}

public record DigitalAsset(decimal Supply, decimal Demand, decimal Lock) : DigitalEntry(Supply, Demand, Lock)
{
}

public record DigitalValue(DigitalCash cash, DigitalAsset asset) : DigitalEntry(cash.Supply, cash.Demand, cash.Lock)
{
}







public class SimulationStates
{
    public MainStates MainStreamState;

    public BranchStates BranchStreamState;

    public ContractStates ContractStreamState;

    public TransferStates SignatureStreamTransfer;

    public ConsensusStates ConsensusState;

    public static int NumberOfLiquityStreams = 1;

    public static int NumberOfAssetStreams = 2;

    public static int NumberOfContractStreamStates = 2;

    public static int NumberOfContractTransferStates = 2;

    public static int NumberOfConsensusStates = 10;

    public static Random R = new Random();

    public static SimulationStates Empty
    {
        get
        {
            return new SimulationStates(
                MainStates.Empty,
                BranchStates.Empty,
                ContractStates.Empty,
                TransferStates.Empty,
                ConsensusStates.Empty);
        }
    }

    public SimulationStates()
    {

    }

    public SimulationStates(MainStates liquidityStreamState, BranchStates assetStreamState, ContractStates contractStreamState, TransferStates liquidityTransferState, ConsensusStates consensusState)
    {
        MainStreamState = liquidityStreamState;
        BranchStreamState = assetStreamState;
        ContractStreamState = contractStreamState;
        SignatureStreamTransfer = liquidityTransferState;
        ConsensusState = consensusState;
    }

    public static bool CheckTabbedLineFormat(string line)
    {
        var tabs = line.Split('\t');

        return line.Split('\t').Length == 28;
    }

    public bool IsEqual(SimulationStates one)
    {
        return this.MainStreamState.IsEqual(one.MainStreamState) &&
            this.BranchStreamState.IsEqual(one.BranchStreamState) &&
            this.ContractStreamState.IsEqual(one.ContractStreamState) &&
            this.SignatureStreamTransfer.IsEqual(one.SignatureStreamTransfer) &&
            this.ConsensusState.IsEqual(one.ConsensusState);
    }

    public SimulationStates Add(SimulationStates state)
    {
        SimulationStates nState = new SimulationStates();

        nState.BranchStreamState = this.BranchStreamState.Add(state.BranchStreamState);
        nState.ContractStreamState = this.ContractStreamState.Add(state.ContractStreamState);
        nState.MainStreamState = this.MainStreamState.Add(state.MainStreamState);
        nState.ConsensusState = this.ConsensusState.Add(state.ConsensusState);
        nState.SignatureStreamTransfer = this.SignatureStreamTransfer.Add(state.SignatureStreamTransfer);

        return nState;
    }

    public static SimulationStates FromTest()
    {
        var LiquidityStreamState = new MainStates(1, 2, 3, 4);

        var AssetStreamState = new BranchStates(5, 6, 7, 8, 9, 10);

        var ContractStreamState = new ContractStates(11, 12, 13, 14, 15, 16);

        var LiquidityTransferState = new TransferStates(17, 18, 19, 20, 21, 22);

        var ConsensusState = new ConsensusStates(23, 24, 25, 26, 27, 28);

        var state = new SimulationStates(LiquidityStreamState, AssetStreamState, ContractStreamState, LiquidityTransferState, ConsensusState);

        return state;
    }

    public static SimulationStates FromRandom()
    {
        var LiquidityStreamState = new MainStates(R.Next(NumberOfLiquityStreams), R.Next(), R.Next(), R.Next());

        var AssetStreamState = new BranchStates(R.Next(NumberOfAssetStreams), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var ContractStreamState = new ContractStates(R.Next(NumberOfContractStreamStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var LiquidityTransferState = new TransferStates(R.Next(NumberOfContractTransferStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var ConsensusState = new ConsensusStates(R.Next(NumberOfConsensusStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var state = new SimulationStates(LiquidityStreamState, AssetStreamState, ContractStreamState, LiquidityTransferState, ConsensusState);

        return state;
    }

    public static SimulationStates ParseFromTabbedLine(string line)
    {
        var state = new SimulationStates();

        state.MainStreamState = MainStates.ParseFromTabbedLine(ref line);

        state.BranchStreamState = BranchStates.ParseFromTabbedLine(ref line);

        state.ContractStreamState = ContractStates.ParseFromTabbedLine(ref line);

        state.SignatureStreamTransfer = TransferStates.ParseFromTabbedLine(ref line);

        state.ConsensusState = ConsensusStates.ParseFromTabbedLine(ref line);

        return state;
    }
    public string ParseToTabbedLine()
    {
        var line = "";

        line += MainStreamState.ParseToTabbedLine();

        line += BranchStreamState.ParseToTabbedLine();

        line += ContractStreamState.ParseToTabbedLine();

        line += SignatureStreamTransfer.ParseToTabbedLine();

        line += ConsensusState.ParseToTabbedLine();

        return line;
    }

    public static SimulationStates ParseFromIntention(string intention)
    {
        var state = new SimulationStates();

        state.MainStreamState = MainStates.Empty;

        state.BranchStreamState = BranchStates.ParseFromIntention(intention);

        state.ContractStreamState = ContractStates.ParseFromIntention(intention);

        state.SignatureStreamTransfer = TransferStates.ParseFromIntention(intention);

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
public record MainStates(int StreamId,  decimal CashSupply, decimal CashDemand, decimal CashLock)
{
    public static MainStates Empty { get { return new MainStates(0, 0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{StreamId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t";
    }

    public static MainStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 5);

        var i = int.Parse(fields[0]);

        var s = decimal.Parse(fields[1]);

        var d = decimal.Parse(fields[2]);

        var l = decimal.Parse(fields[3]);

        line = fields[4];

        return new MainStates(i, s, d, l);
    }

    public MainStates Add(MainStates state)
    {
        return new MainStates
            (
            state.StreamId + this.StreamId, 
            state.CashSupply + this.CashSupply, 
            state.CashDemand + this.CashDemand, 
            state.CashLock + this.CashLock
            );       
    }

    public bool IsEqual(MainStates state)
    {
        if (state.StreamId != StreamId) return false;
        if (state.CashSupply != CashSupply) return false;
        if (state.CashDemand != CashDemand) return false;
        if (state.CashLock != CashLock) return false;

        return true;
    }
}

public record BranchStates(int AssetId, decimal CashSupply, decimal CashDemand, decimal CashLock, decimal AssetSupply, decimal AssetDemand)
{
    public static BranchStates Empty { get { return new BranchStates(0, 0, 0, 0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{AssetId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t";
    }

    public static BranchStates ParseFromIntention(string intention)
    {
        DemoWeb.DataBag bag = DemoWeb.DefaultParse(intention);

        return new BranchStates(bag.assetID, 0, 0, 0, 0, 0);
    }

    public static BranchStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 7);

        var i = int.Parse(fields[0]);

        var cS = decimal.Parse(fields[1]);

        var cD = decimal.Parse(fields[2]);

        var cL = decimal.Parse(fields[3]);

        var aS = decimal.Parse(fields[4]);

        var aD = decimal.Parse(fields[5]);

        line = fields[6];

        return new BranchStates(i, cS, cD, cL, aS, aD);
    }

    public BranchStates Add(BranchStates state)
    {
        if (this.AssetId != state.AssetId) return this;

        // remember to check if cashlocks are compatable

        return new BranchStates
            (
                state.AssetId,
                state.CashSupply + this.CashSupply,
                state.CashDemand + this.CashDemand,
                state.CashLock,
                state.AssetSupply + this.AssetSupply,
                state.AssetDemand + this.AssetDemand
            );
    }

    public bool IsEqual(BranchStates state)
    {
        if (state.AssetId != AssetId) return false;
        if (state.AssetSupply != AssetSupply) return false;
        if (state.AssetDemand != AssetDemand) return false;
        if (state.CashSupply != CashSupply) return false;
        if (state.CashDemand != CashDemand) return false;
        if (state.CashLock != CashLock) return false;

        return true;
    }
}

public record ContractStates(int ContractId, decimal CashSupply, decimal CashDemand, decimal CashLock, decimal AssetSupply, decimal AssetDemand)
{
    public static ContractStates Empty { get { return new ContractStates(0, 0, 0, 0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{ContractId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t";
    }

    public static ContractStates ParseFromIntention(string intention)
    {
        DemoWeb.DataBag bag = DemoWeb.DefaultParse(intention);

        return new ContractStates(1, bag.cashSup, bag.cashDem, bag.cashLock, bag.assetSup, bag.assetDem);
    }

    public static ContractStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 7);

        var i = int.Parse(fields[0]);

        var cS = decimal.Parse(fields[1]);

        var cD = decimal.Parse(fields[2]);

        var cL = decimal.Parse(fields[3]);

        var aS = decimal.Parse(fields[4]);

        var aD = decimal.Parse(fields[5]);

        line = fields[6];

        return new ContractStates(i, cS, cD, cL, aS, aD);
    }

    public bool IsEqual(ContractStates state)
    {
        //if (state.ContractId != ContractId) return false;
        if (state.AssetSupply != AssetSupply) return false;
        if (state.AssetDemand != AssetDemand) return false;
        if (state.CashSupply != CashSupply) return false;
        if (state.CashDemand != CashDemand) return false;
        if (state.CashLock != CashLock) return false;

        return true;
    }

    public ContractStates Add(ContractStates state)
    {
        return new ContractStates
            (
            state.ContractId,
            state.CashSupply + this.CashSupply,
            state.CashDemand + this.CashDemand,
            state.CashLock + this.CashLock,
            state.AssetSupply + this.AssetSupply,
            state.AssetDemand + this.AssetDemand
            );
    }
}

public record TransferStates(int TransferId, decimal CashSupply, decimal CashDemand, decimal CashLock, decimal AssetSupply, decimal AssetDemand)
{
    public static TransferStates Empty { get { return new TransferStates(0, 0, 0, 0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{TransferId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t";
    }

    public static TransferStates ParseFromIntention(string intention)
    {


        DemoWeb.DataBag bag = DemoWeb.DefaultParse(intention);

        return new TransferStates(1, bag.cashSup, bag.cashDem, bag.cashLock, bag.assetSup, bag.assetDem);
    }
    public static TransferStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 7);

        var i = int.Parse(fields[0]);

        var cS = decimal.Parse(fields[1]);

        var cD = decimal.Parse(fields[2]);

        var cL = decimal.Parse(fields[3]);

        var aS = decimal.Parse(fields[4]);

        var aD = decimal.Parse(fields[5]);

        line = fields[6];

        return new TransferStates(i, cS, cD, cL, aS, aD);
    }

    public bool IsEqual(TransferStates state)
    {
        //if (state.TransferId != TransferId) return false;
        if (state.AssetSupply != AssetSupply) return false;
        if (state.AssetDemand != AssetDemand) return false;
        if (state.CashSupply != CashSupply) return false;
        if (state.CashDemand != CashDemand) return false;
        if (state.CashLock != CashLock) return false;

        return true;
    }

    public TransferStates Add(TransferStates state)
    {
        return new TransferStates
            (
                state.TransferId,
                state.CashSupply + this.CashSupply,
                state.CashDemand + this.CashDemand,
                state.CashLock,
                state.AssetSupply + this.AssetSupply,
                state.AssetDemand + this.AssetDemand
            );
    }
}

public record ConsensusStates(int ConsensusId, decimal Safety, Int64 ProofOfStake, Int64 ProofOfWork, int SuperConsensusId, int RelayConsensusId)
{    public static ConsensusStates Empty { get { return new ConsensusStates(0, 0, 0, 0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{ConsensusId}\t{Safety}\t{ProofOfStake}\t{ProofOfWork}\t{SuperConsensusId}\t{RelayConsensusId}\n";
    }
    public static ConsensusStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', '\n');

        var i = int.Parse(fields[0]);

        var S = decimal.Parse(fields[1]);

        var pS = Int64.Parse(fields[2]);

        var pW = Int64.Parse(fields[3]);

        var supperId = int.Parse(fields[4]);

        var relayId = int.Parse(fields[5]);

        line = "\n";

        return new ConsensusStates(i, S, pS, pW, supperId, relayId);
    }

    public bool IsEqual(ConsensusStates one)
    {
        // add consensus comparision
        return true;
    }

    public ConsensusStates Add(ConsensusStates state)
    {
        return this; // needs to be expanded
    }
}
#endregion
