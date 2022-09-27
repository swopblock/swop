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
            // begin temp test
            var testA = SimulationStates.FromRandom();

            var line = testA.ParseToTabbedLine();

            var testB = SimulationStates.ParseFromTabbedLine(line);

            if (testA == testB)
            {
                Console.WriteLine("=");
            }

            // end temp test
            Console.WriteLine("Hello, Swopblock World!");

            programAgrs = args;

            GetModuleArgs(programAgrs);

            var simulation = new SimulationModule(simulationArgs, consensusArgs, executionArgs);


            simulation.BuildSimultion(1, 1, 1, 1, 1, 1);

            IntentionDemonstrationRegion();

            static void IntentionDemonstrationRegion()
            {
                #region Intention Demonstration

                IntentionTree Tree = DemoWeb.GetTree();

                SwopblockModule client = new SwopblockModule(null, null);

                string userInput = Console.ReadLine();

                byte[] serByte = Tree.Serializer.Serialize(userInput);

                string check = Tree.Serializer.Deserialize(serByte);

                if (check.ToLower() == userInput.ToLower())
                {
                    if (false) // (client.CaptureIntention(userInput))
                    {
                        Console.WriteLine("Congratz! Your request has been accepted by the Swopblock Network!");
                    }
                }
                else
                {
                    Console.WriteLine("input error");
                }

                // start network state
                //ContractStreamStates NetworkContractState = new ContractStreamStates(0, null, null, null);
                // get user contract
                //ContractStream state = DemoPrompt.Run();
                // update network state
                //NetworkContractState.Add(state);

                #endregion
            }

            while (true)
            {
                var contract = Console.ReadLine();

                simulation.PokeInEntryInput(LiquidityStreamStates.Empty);

                var output = simulation.PeekAtExitOutput();

                Console.WriteLine(output.ParseToTabbedLine());
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
    public static SimulationStates FromRandom()
    {
        var state = new SimulationStates();

        state.LiquidityStreamState = new LiquidityStreamStates(R.Next(NumberOfLiquityStreams), R.Next(), R.Next(), R.Next());

        state.AssetStreamState = new AssetStreamStates(R.Next(NumberOfAssetStreams), R.Next(), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        state.ContractStreamState = new ContractStreamStates(R.Next(NumberOfContractStreamStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        state.LiquidityTransferState = new ContractTransferStates(R.Next(NumberOfContractTransferStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

        state.ConsensusState = new ConsensusStates(R.Next(NumberOfConsensusStates), R.Next(), R.Next(), R.Next(), R.Next(), R.Next());

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
}

// top level
public record LiquidityStreamStates(int StreamId,  decimal CashSupply, decimal CashDemand, decimal CashLock)
{
    public string ParseToTabbedLine()
    {
        return $"{StreamId}\t{CashSupply}\t{CashDemand}\t{CashLock}";
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

    public static LiquidityStreamStates ParseFromIntention(string intention)
    {

        return null;
    }

    public static LiquidityStreamStates operator +(LiquidityStreamStates one, LiquidityStreamStates two) { return null; }

    public static LiquidityStreamStates Empty { get { return new LiquidityStreamStates(0, 0, 0, 0); } }
}

public record AssetStreamStates(int AssetId, decimal CashSupply, decimal CashDemand, decimal CashLock, decimal AssetSupply, decimal AssetDemand, decimal AssetLock)
{
    public string ParseToTabbedLine()
    {
        return $"{AssetId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t{AssetLock}";
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
        return $"{ContractId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t{AssetLock}";
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
        return $"{TransferId}\t{CashSupply}\t{CashDemand}\t{CashLock}\t{AssetSupply}\t{AssetDemand}\t{AssetLock}";
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
        var fields = line.Split('\t', 7);

        var i = int.Parse(fields[0]);

        var S = decimal.Parse(fields[1]);

        var pS = Int64.Parse(fields[2]);

        var pW = Int64.Parse(fields[3]);

        var supperId = int.Parse(fields[4]);

        var relayId = int.Parse(fields[5]);

        line = fields[6];

        return new ConsensusStates(i, S, pS, pW, supperId, relayId);
    }
}
#endregion
