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
    public MainStreamStates MainStreamState;

    public BranchStreamStates BranchStreamState;

    public ContractStreamStates ContractStreamState;

    public SignatureStreamTransfers SignatureStreamTransfer;

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
                MainStreamStates.Empty,
                BranchStreamStates.Empty,
                ContractStreamStates.Empty,
                SignatureStreamTransfers.Empty,
                ConsensusStates.Empty);
        }
    }

    public SimulationStates()
    {

    }

    public SimulationStates(MainStreamStates liquidityStreamState, BranchStreamStates assetStreamState, ContractStreamStates contractStreamState, SignatureStreamTransfers liquidityTransferState, ConsensusStates consensusState)
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
        var LiquidityStreamState = new MainStreamStates(1, 2, 3, 4);

        var AssetStreamState = new BranchStreamStates(5, 6, 7, 8, 9, 10);

        var ContractStreamState = new ContractStreamStates(11, 12, 13, 14, 15, 16);

        var LiquidityTransferState = new SignatureStreamTransfers(17, 18, 19, 20, 21, 22);

        var ConsensusState = new ConsensusStates(23, 24, 25, 26, 27, 28);

        var state = new SimulationStates(LiquidityStreamState, AssetStreamState, ContractStreamState, LiquidityTransferState, ConsensusState);

        return state;
    }

    public static SimulationStates FromRandom()
    {
        var LiquidityStreamState = new MainStreamStates(R.Next(NumberOfLiquityStreams), R.Next(), R.Next(), R.Next());

        var AssetStreamState = new BranchStreamStates(R.Next(NumberOfAssetStreams), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var ContractStreamState = new ContractStreamStates(R.Next(NumberOfContractStreamStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var LiquidityTransferState = new SignatureStreamTransfers(R.Next(NumberOfContractTransferStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var ConsensusState = new ConsensusStates(R.Next(NumberOfConsensusStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var state = new SimulationStates(LiquidityStreamState, AssetStreamState, ContractStreamState, LiquidityTransferState, ConsensusState);

        return state;
    }

    public static SimulationStates ParseFromTabbedLine(string line)
    {
        var state = new SimulationStates();

        state.MainStreamState = MainStreamStates.ParseFromTabbedLine(ref line);

        state.BranchStreamState = BranchStreamStates.ParseFromTabbedLine(ref line);

        state.ContractStreamState = ContractStreamStates.ParseFromTabbedLine(ref line);

        state.SignatureStreamTransfer = SignatureStreamTransfers.ParseFromTabbedLine(ref line);

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

        state.MainStreamState = MainStreamStates.Empty;

        state.BranchStreamState = BranchStreamStates.ParseFromIntention(intention);

        state.ContractStreamState = ContractStreamStates.ParseFromIntention(intention);

        state.SignatureStreamTransfer = SignatureStreamTransfers.ParseFromIntention(intention);

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
public record MainStreamStates(int StreamId,  decimal CashSupply, decimal CashDemand, decimal CashLock)
{
    public static MainStreamStates Empty { get { return new MainStreamStates(0, 0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{StreamId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t";
    }

    public static MainStreamStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 5);

        var i = int.Parse(fields[0]);

        var s = decimal.Parse(fields[1]);

        var d = decimal.Parse(fields[2]);

        var l = decimal.Parse(fields[3]);

        line = fields[4];

        return new MainStreamStates(i, s, d, l);
    }

    public MainStreamStates Add(MainStreamStates state)
    {
        return new MainStreamStates
            (
            state.StreamId + this.StreamId, 
            state.CashSupply + this.CashSupply, 
            state.CashDemand + this.CashDemand, 
            state.CashLock + this.CashLock
            );       
    }

    public bool IsEqual(MainStreamStates state)
    {
        if (state.StreamId != StreamId) return false;
        if (state.CashSupply != CashSupply) return false;
        if (state.CashDemand != CashDemand) return false;
        if (state.CashLock != CashLock) return false;

        return true;
    }
}

public record BranchStreamStates(int AssetId, decimal CashSupply, decimal CashDemand, decimal CashLock, decimal AssetSupply, decimal AssetDemand)
{
    public static BranchStreamStates Empty { get { return new BranchStreamStates(0, 0, 0, 0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{AssetId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t";
    }

    public static BranchStreamStates ParseFromIntention(string intention)
    {
        DemoWeb.DataBag bag = DemoWeb.DefaultParse(intention);

        return new BranchStreamStates(bag.assetID, 0, 0, 0, 0, 0);
    }

    public static BranchStreamStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 7);

        var i = int.Parse(fields[0]);

        var cS = decimal.Parse(fields[1]);

        var cD = decimal.Parse(fields[2]);

        var cL = decimal.Parse(fields[3]);

        var aS = decimal.Parse(fields[4]);

        var aD = decimal.Parse(fields[5]);

        line = fields[6];

        return new BranchStreamStates(i, cS, cD, cL, aS, aD);
    }

    public BranchStreamStates Add(BranchStreamStates state)
    {
        if (this.AssetId != state.AssetId) return this;

        // remember to check if cashlocks are compatable

        return new BranchStreamStates
            (
                state.AssetId,
                state.CashSupply + this.CashSupply,
                state.CashDemand + this.CashDemand,
                state.CashLock,
                state.AssetSupply + this.AssetSupply,
                state.AssetDemand + this.AssetDemand
            );
    }

    public bool IsEqual(BranchStreamStates state)
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

public record ContractStreamStates(int ContractId, decimal CashSupply, decimal CashDemand, decimal CashLock, decimal AssetSupply, decimal AssetDemand)
{
    public static ContractStreamStates Empty { get { return new ContractStreamStates(0, 0, 0, 0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{ContractId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t";
    }

    public static ContractStreamStates ParseFromIntention(string intention)
    {
        DemoWeb.DataBag bag = DemoWeb.DefaultParse(intention);

        return new ContractStreamStates(1, bag.cashSup, bag.cashDem, bag.cashLock, bag.assetSup, bag.assetDem);
    }

    public static ContractStreamStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 7);

        var i = int.Parse(fields[0]);

        var cS = decimal.Parse(fields[1]);

        var cD = decimal.Parse(fields[2]);

        var cL = decimal.Parse(fields[3]);

        var aS = decimal.Parse(fields[4]);

        var aD = decimal.Parse(fields[5]);

        line = fields[6];

        return new ContractStreamStates(i, cS, cD, cL, aS, aD);
    }

    public bool IsEqual(ContractStreamStates state)
    {
        //if (state.ContractId != ContractId) return false;
        if (state.AssetSupply != AssetSupply) return false;
        if (state.AssetDemand != AssetDemand) return false;
        if (state.CashSupply != CashSupply) return false;
        if (state.CashDemand != CashDemand) return false;
        if (state.CashLock != CashLock) return false;

        return true;
    }

    public ContractStreamStates Add(ContractStreamStates state)
    {
        return new ContractStreamStates
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

public record SignatureStreamTransfers(int TransferId, decimal CashSupply, decimal CashDemand, decimal CashLock, decimal AssetSupply, decimal AssetDemand)
{
    public static SignatureStreamTransfers Empty { get { return new SignatureStreamTransfers(0, 0, 0, 0, 0, 0); } }
    public string ParseToTabbedLine()
    {
        return $"{TransferId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t";
    }

    public static SignatureStreamTransfers ParseFromIntention(string intention)
    {


        DemoWeb.DataBag bag = DemoWeb.DefaultParse(intention);

        return new SignatureStreamTransfers(1, bag.cashSup, bag.cashDem, bag.cashLock, bag.assetSup, bag.assetDem);
    }
    public static SignatureStreamTransfers ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 7);

        var i = int.Parse(fields[0]);

        var cS = decimal.Parse(fields[1]);

        var cD = decimal.Parse(fields[2]);

        var cL = decimal.Parse(fields[3]);

        var aS = decimal.Parse(fields[4]);

        var aD = decimal.Parse(fields[5]);

        line = fields[6];

        return new SignatureStreamTransfers(i, cS, cD, cL, aS, aD);
    }

    public bool IsEqual(SignatureStreamTransfers state)
    {
        //if (state.TransferId != TransferId) return false;
        if (state.AssetSupply != AssetSupply) return false;
        if (state.AssetDemand != AssetDemand) return false;
        if (state.CashSupply != CashSupply) return false;
        if (state.CashDemand != CashDemand) return false;
        if (state.CashLock != CashLock) return false;

        return true;
    }

    public SignatureStreamTransfers Add(SignatureStreamTransfers state)
    {
        return new SignatureStreamTransfers
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
