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

namespace Swopblock
{
    public class Program
    {
        public static string[] programAgrs;

        public static string[] simulationArgs;

        public static string[] consensusArgs;

        public static string[] executionArgs;

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, Swopblock World!");

            programAgrs = args;

            GetModuleArgs(programAgrs);

            var simulation = new SimulationModule(simulationArgs, consensusArgs, executionArgs);

            simulation.BuildSimultion(1, 1, 1, 1, 1, 1);

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
    public LiquidityStreamStates LiquidityStreamState;

    public AssetStreamStates AssetStreamState;

    public ContractStreamStates ContractStreamState;

    public ContractTransferStates LiquidityTransferState;

    public ConsensusStates ConsensusState;

    public static int NumberOfLiquityStreams = 1;

    public static int NumberOfAssetStreams = 2;

    public static int NumberOfContractStreamStates = 2;

    public static int NumberOfContractTransferStates = 2;

    public static int NumberOfConsensusStates = 10;

    public static Random R = new Random();

    public SimulationStates()
    {

    }

    public SimulationStates(LiquidityStreamStates liquidityStreamState, AssetStreamStates assetStreamState, ContractStreamStates contractStreamState, ContractTransferStates liquidityTransferState, ConsensusStates consensusState)
    {
        LiquidityStreamState = liquidityStreamState;
        AssetStreamState = assetStreamState;
        ContractStreamState = contractStreamState;
        LiquidityTransferState = liquidityTransferState;
        ConsensusState = consensusState;
    }

    public static bool CheckTabbedLineFormat(string line)
    {
        var tabs = line.Split('\t');

        return line.Split('\t').Length == 31;
    }

    public bool IsEqual(SimulationStates one)
    {
        if (this.LiquidityTransferState != one.LiquidityTransferState) return false;
        if (this.AssetStreamState != one.AssetStreamState) return false;
        if (this.ContractStreamState != one.ContractStreamState) return false;
        if (this.LiquidityTransferState != one.LiquidityTransferState) return false;
        if (this.ConsensusState != one.ConsensusState) return false;

        return true;
    }

    public static SimulationStates Empty
    {
        get
        {
            return new SimulationStates(null, null, null, null, null);
        }
    }

    public static SimulationStates FromTest()
    {
        var LiquidityStreamState = new LiquidityStreamStates(1, 2, 3, 4);

        var AssetStreamState = new AssetStreamStates(5, 6, 7, 8, 9, 10, 11);

        var ContractStreamState = new ContractStreamStates(12, 13, 14, 15, 16, 17, 18);

        var LiquidityTransferState = new ContractTransferStates(19, 20, 21, 22, 23, 24, 25);

        var ConsensusState = new ConsensusStates(26, 27, 28, 29, 30, 31);

        var state = new SimulationStates(LiquidityStreamState, AssetStreamState, ContractStreamState, LiquidityTransferState, ConsensusState);

        return state;
    }

    public static SimulationStates FromRandom()
    {
        var LiquidityStreamState = new LiquidityStreamStates(R.Next(NumberOfLiquityStreams), R.Next(), R.Next(), R.Next());

        var AssetStreamState = new AssetStreamStates(R.Next(NumberOfAssetStreams), R.Next(), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var ContractStreamState = new ContractStreamStates(R.Next(NumberOfContractStreamStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var LiquidityTransferState = new ContractTransferStates(R.Next(NumberOfContractTransferStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var ConsensusState = new ConsensusStates(R.Next(NumberOfConsensusStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var state = new SimulationStates(LiquidityStreamState, AssetStreamState, ContractStreamState, LiquidityTransferState, ConsensusState);

        return state;
    }

    public static SimulationStates ParseFromTabbedLine(string line)
    {
        var state = new SimulationStates();

        state.LiquidityStreamState = LiquidityStreamStates.ParseFromTabbedLine(ref line);

        state.AssetStreamState = AssetStreamStates.ParseFromTabbedLine(ref line);

        state.ContractStreamState = ContractStreamStates.ParseFromTabbedLine(ref line);

        state.LiquidityTransferState = ContractTransferStates.ParseFromTabbedLine(ref line);

        state.ConsensusState = ConsensusStates.ParseFromTabbedLine(ref line);

        return state;
    }
    public string ParseToTabbedLine()
    {
        var line = "";

        line += LiquidityStreamState.ParseToTabbedLine();

        line += AssetStreamState.ParseToTabbedLine();

        line += ContractStreamState.ParseToTabbedLine();

        line += LiquidityTransferState.ParseToTabbedLine();

        line += ConsensusState.ParseToTabbedLine();

        return line;
    }

    public static SimulationStates ParseFromIntention(string intention)
    {
        // figure out which patterns are matched

        foreach (string pattern in DemoWeb.Patterns)
        {
            MatchResult mr = IntentionBranch.MatchesPattern(intention, pattern);

            if (mr.Matches)
            {
                // analize mr.EmbeddedValues
                break;
            }
        }



        // create the translated state
        var state = new SimulationStates();

        state.LiquidityStreamState = new LiquidityStreamStates(0, 0, 0, 0);

        state.AssetStreamState = new AssetStreamStates(0, 0, 0, 0, 0, 0, 0);

        state.ContractStreamState = new ContractStreamStates(0, 0, 0, 0, 0, 0, 0);

        state.LiquidityTransferState = new ContractTransferStates(0, 0, 0, 0, 0, 0, 0);

        state.ConsensusState = new ConsensusStates(0, 0, 0, 0, 0, 0);

        return state;
    }

    public static SimulationStates ParseFromLine(string line)
    {
        if (line[0] == 'I') return SimulationStates.ParseFromIntention(line);

        return SimulationStates.ParseFromTabbedLine(line);
    }

}

// top level
public record LiquidityStreamStates(int StreamId,  decimal CashSupply, decimal CashDemand, decimal CashLock)
{
    public string ParseToTabbedLine()
    {
        return $"{StreamId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t";
    }

    public static LiquidityStreamStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 5);

        var i = int.Parse(fields[0]);

        var s = decimal.Parse(fields[1]);

        var d = decimal.Parse(fields[2]);

        var l = decimal.Parse(fields[3]);

        line = fields[4];

        return new LiquidityStreamStates(i, s, d, l);
    }

    public static LiquidityStreamStates operator +(LiquidityStreamStates one, LiquidityStreamStates two) { return null; }

    public static LiquidityStreamStates Empty { get { return new LiquidityStreamStates(0, 0, 0, 0); } }
}

public record AssetStreamStates(int AssetId, decimal CashSupply, decimal CashDemand, decimal CashLock, decimal AssetSupply, decimal AssetDemand, decimal AssetLock)
{
    public string ParseToTabbedLine()
    {
        return $"{AssetId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t{AssetLock}\t";
    }

    public static AssetStreamStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 8);

        var i = int.Parse(fields[0]);

        var cS = decimal.Parse(fields[1]);

        var cD = decimal.Parse(fields[2]);

        var cL = decimal.Parse(fields[3]);

        var aS = decimal.Parse(fields[4]);

        var aD = decimal.Parse(fields[5]);

        var aL = decimal.Parse(fields[6]);

        line = fields[7];

        return new AssetStreamStates(i, cS, cD, cL, aS, aD, aL);
    }
}

public record ContractStreamStates(int ContractId, decimal CashSupply, decimal CashDemand, decimal CashLock, decimal AssetSupply, decimal AssetDemand, decimal AssetLock)
{
    public string ParseToTabbedLine()
    {
        return $"{ContractId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t{AssetLock}\t";
    }

    public static ContractStreamStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 8);

        var i = int.Parse(fields[0]);

        var cS = decimal.Parse(fields[1]);

        var cD = decimal.Parse(fields[2]);

        var cL = decimal.Parse(fields[3]);

        var aS = decimal.Parse(fields[4]);

        var aD = decimal.Parse(fields[5]);

        var aL = decimal.Parse(fields[6]);

        line = fields[7];

        return new ContractStreamStates(i, cS, cD, cL, aS, aD, aL);
    }
}


public record ContractTransferStates(int TransferId, decimal CashSupply, decimal CashDemand, decimal CashLock, decimal AssetSupply, decimal AssetDemand, decimal AssetLock)
{
    public string ParseToTabbedLine()
    {
        return $"{TransferId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t{AssetLock}\t";
    }

    public static ContractTransferStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 8);

        var i = int.Parse(fields[0]);

        var cS = decimal.Parse(fields[1]);

        var cD = decimal.Parse(fields[2]);

        var cL = decimal.Parse(fields[3]);

        var aS = decimal.Parse(fields[4]);

        var aD = decimal.Parse(fields[5]);

        var aL = decimal.Parse(fields[6]);

        line = fields[7];

        return new ContractTransferStates(i, cS, cD, cL, aS, aD, aL);
    }
}

public record ConsensusStates(int ConsensusId, decimal Safety, Int64 ProofOfStake, Int64 ProofOfWork, int SuperConsensusId, int RelayConsensusId)
{
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
}
#endregion
