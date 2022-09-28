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

        return line.Split('\t').Length == 28;
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

        var AssetStreamState = new AssetStreamStates(5, 6, 7, 8, 9, 10);

        var ContractStreamState = new ContractStreamStates(11, 12, 13, 14, 15, 16);

        var LiquidityTransferState = new ContractTransferStates(17, 18, 19, 20, 21, 22);

        var ConsensusState = new ConsensusStates(23, 24, 25, 26, 27, 28);

        var state = new SimulationStates(LiquidityStreamState, AssetStreamState, ContractStreamState, LiquidityTransferState, ConsensusState);

        return state;
    }

    public static SimulationStates FromRandom()
    {
        var LiquidityStreamState = new LiquidityStreamStates(R.Next(NumberOfLiquityStreams), R.Next(), R.Next(), R.Next());

        var AssetStreamState = new AssetStreamStates(R.Next(NumberOfAssetStreams), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var ContractStreamState = new ContractStreamStates(R.Next(NumberOfContractStreamStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        var LiquidityTransferState = new ContractTransferStates(R.Next(NumberOfContractTransferStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

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
        var state = new SimulationStates();

        state.LiquidityStreamState = new LiquidityStreamStates(0, 0, 0, 0);

        state.AssetStreamState = AssetStreamStates.ParseFromIntention(intention);

        state.ContractStreamState = ContractStreamStates.ParseFromIntention(intention);

        state.LiquidityTransferState = ContractTransferStates.ParseFromIntention(intention);

        state.ConsensusState = new ConsensusStates(0, 0, 0, 0, 0, 0);

        return state;
    }

    public static SimulationStates ParseFromLine(string line)
    {
        if (line[0].ToString().ToLower() == "i") return SimulationStates.ParseFromIntention(line);

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

public record AssetStreamStates(int AssetId, decimal CashSupply, decimal CashDemand, decimal CashLock, decimal AssetSupply, decimal AssetDemand)
{
    public string ParseToTabbedLine()
    {
        return $"{AssetId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t";
    }

    public static AssetStreamStates ParseFromIntention(string intention)
    {
        /*
         * I am [bidding] exactly [100] [SWOBL] of mine from my address [cid] 
         * in order to buy at least [1] [BTC] of yours from the market 
         * and my order is good until the market volume reaches [expirationVolume] SWOBL 
         * using my signature [transferId].
         */

        string assetTag = "";
        int assetID = 0;
        decimal cashSup = 0;
        decimal cashDem = 0;
        decimal assetSup = 0;
        decimal assetDem = 0;
        decimal cashLock = 0;

        MatchResult res = null;

        foreach (string pattern in DemoWeb.Patterns)
        {
            res = IntentionBranch.MatchesPattern(intention, pattern);

            if (res.Matches)
            {
                break;
            }
        }

        if(res != null)
        {
            if(res.EmbeddedValues != null)
            {
                if(res.EmbeddedValues.Count > 7)
                {
                    if (res.EmbeddedValues[0].ToLower() == "bidding")
                    {
                        if (res.EmbeddedValues[2].ToLower() == "swobl")
                        {
                            cashSup = decimal.Parse(res.EmbeddedValues[1]);
                            cashDem = 0;
                            assetSup = 0;
                            assetDem = decimal.Parse(res.EmbeddedValues[4]);
                            cashLock = decimal.Parse(res.EmbeddedValues[6]);

                            assetTag = res.EmbeddedValues[5].ToLower();
                        }
                    }
                    else if (res.EmbeddedValues[0].ToLower() == "asking")
                    {
                        assetTag = res.EmbeddedValues[2].ToLower();

                        cashDem = decimal.Parse(res.EmbeddedValues[3]);
                        cashSup = 0;
                        assetDem = 0;
                        assetSup = decimal.Parse(res.EmbeddedValues[1]);
                    }

                    for (int i = 0; i < ushort.MaxValue; i++)
                    {
                        Program.AssetTags tag = (Program.AssetTags)i;

                        if (tag.ToString().ToLower() == assetTag)
                        {
                            assetID = i;
                            break;
                        }

                        // break if tag no longer converts to text
                    }
                }
            }
        }

        

        return new AssetStreamStates(assetID, cashSup, cashDem, cashLock, assetSup, assetDem);
    }

    public static AssetStreamStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 7);

        var i = int.Parse(fields[0]);

        var cS = decimal.Parse(fields[1]);

        var cD = decimal.Parse(fields[2]);

        var cL = decimal.Parse(fields[3]);

        var aS = decimal.Parse(fields[4]);

        var aD = decimal.Parse(fields[5]);

        line = fields[6];

        return new AssetStreamStates(i, cS, cD, cL, aS, aD);
    }

    public bool IsEqual(AssetStreamStates state)
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
    public string ParseToTabbedLine()
    {
        return $"{ContractId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t";
    }

    public static ContractStreamStates ParseFromIntention(string intention)
    {
        return new ContractStreamStates(0, 0, 0, 0, 0, 0);
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
}


public record ContractTransferStates(int TransferId, decimal CashSupply, decimal CashDemand, decimal CashLock, decimal AssetSupply, decimal AssetDemand)
{
    public string ParseToTabbedLine()
    {
        return $"{TransferId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t";
    }


    public static ContractTransferStates ParseFromIntention(string intention)
    {
        // replace this line with correct code
        return new ContractTransferStates(0, 0, 0, 0, 0, 0);
    }
    public static ContractTransferStates ParseFromTabbedLine(ref string line)
    {
        var fields = line.Split('\t', 7);

        var i = int.Parse(fields[0]);

        var cS = decimal.Parse(fields[1]);

        var cD = decimal.Parse(fields[2]);

        var cL = decimal.Parse(fields[3]);

        var aS = decimal.Parse(fields[4]);

        var aD = decimal.Parse(fields[5]);

        line = fields[6];

        return new ContractTransferStates(i, cS, cD, cL, aS, aD);
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
