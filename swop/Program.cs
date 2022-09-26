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
                ContractStreamStates NetworkContractState = new ContractStreamStates(0, null, null, null);
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
public record StreamLocks(decimal Volume)
{
    public string ParseToTabbed()
    {
        return $"{Volume}";
    }
}

public record DigitalEntry(decimal Supply, decimal Demand, StreamLocks Lock)
{
    public string ParseToTabbed()
    {
        return $"{Supply}\t{Demand}\t{Lock.ParseToTabbed()}";
    }
}


public record DigitalCash(decimal Supply, decimal Demand, StreamLocks Lock) : DigitalEntry(Supply, Demand, Lock)
{
}

public record DigitalAsset(decimal Supply, decimal Demand, StreamLocks Lock) : DigitalEntry(Supply, Demand, Lock)
{
}

public record DigitalValue(DigitalCash cash, DigitalAsset asset)
{
    public string ParseToTabbed()
    {
        return $"{cash.ParseToTabbed()}\t{asset.ParseToTabbed()}";
    }
}







//public record SimulationStates(int SimId, LiquidityStreamStates Stream)
//{
//    public static SimulationStates ParseFromTabbedLine(string line)
//    {
//        return null;
//    }
//    public string ParseToTabbedLine()
//    {
//        return $"{SimId}\t{Stream.ParseToTabbedLine()}";
//    }
//}

// top level
public record LiquidityStreamStates(int StreamId,  DigitalCash Cash, AssetStreamStates State)
{
    public string ParseToTabbedLine()
    {
        return $"{StreamId}\t{Cash.ParseToTabbed()}\t{State.ParseToTabbedLine()}";
    }

    public static LiquidityStreamStates ParseFromIntention(string intention)
    {

        return null;
    }

    public static LiquidityStreamStates operator +(LiquidityStreamStates one, LiquidityStreamStates two) { return null; }

    public static LiquidityStreamStates Empty { get { return new LiquidityStreamStates(0, null, null); } }
}

public record AssetStreamStates(int AssetId, DigitalCash Cash, DigitalAsset Asset, ContractStreamStates State)
{
    public string ParseToTabbedLine()
    {
        return $"{AssetId}\t{Cash.ParseToTabbed()}\t{Asset.ParseToTabbed()}\t{State.ParseToTabbedLine()}";
    }
}

public record ContractStreamStates(int ContractId, DigitalCash Cash, DigitalAsset Asset, LiquidityTransferStates Transfer)
{
  
    public string ParseToTabbedLine()
    {
        return $"{ContractId}\t{Cash.ParseToTabbed()}\t{Asset.ParseToTabbed()}\t{Transfer}";
    }

    public static LiquidityStreamStates ParseFromIntention(string intention)
    {
        // bid
        return null; // new LiquidityStreamStates(1, new DigitalCash(1, 0), )
    }
}


public record LiquidityTransferStates(int TransferId, DigitalCash Cash, DigitalAsset Asset, ConsensusStates State)
{
    public string ParseToTabbedLine()
    {
        return $"{TransferId}\t{Cash.ParseToTabbed()}\t{Asset.ParseToTabbed()}\t{State.ParseToTabbedLine()}";
    }
}

public record ConsensusStates(int ConsensusId, decimal Safety, Int64 ProofOfStake, Int64 ProofOfWork, int SuperConsensusId, int RelayConsensusId)
{
    public string ParseToTabbedLine()
    {
        return $"{ConsensusId}\t{Safety}\t{ProofOfStake}\t{ProofOfWork}\t{SuperConsensusId}\t{RelayConsensusId}\n";
    }
}
#endregion
