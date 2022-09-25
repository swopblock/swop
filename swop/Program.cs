// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using Swopblock;
using Swopblock.Intentions;
using Swopblock.Intentions.Utilities;
using System.Globalization;
using Swopblock.Demo;
using swop.Demo;

Console.WriteLine("Hello, Swopblock World!");

LiquidityStreamStates GetNextEntry()
{
    return null;
}

int simulationArgsIndex = 0;
int consensusArgsIndex = 0;

#region Intention Demonstration

IntentionTree Tree = DemoWeb.GetTree();

SwopblockClient client = new SwopblockClient();

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
ContractStreamStates NetworkContractState = new ContractStreamStates(0, 0, 0, 0, 0);
// get user contract
//ContractStream state = DemoPrompt.Run();
// update network state
//NetworkContractState.Add(state);

#endregion


int stateCount = args.Length > 0 ? int.Parse(args[0]) : 2;

int executionArgsIndex = 0;

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

string[] simulationArgs = args.Skip(simulationArgsIndex).ToArray().Take(consensusArgsIndex - simulationArgsIndex).ToArray();

string[] consensusArgs = args.Skip(consensusArgsIndex).ToArray().Take(executionArgsIndex - consensusArgsIndex).ToArray();

string[] executionArgs = args.Skip(executionArgsIndex).ToArray().Take(args.Length - executionArgsIndex).ToArray();


//SimulationModule simulationModule = new SimulationModule(simulationArgs);

//ConsensusModule consensusModule = new ConsensusModule(consensusArgs);

//ExecutionModule executionModule = new ExecutionModule(executionArgs);

#region Input Output Data Types
public record StreamLocks(decimal Volume);
public record DigitalEntry(decimal Supply, decimal Demand, StreamLocks StreamLock) : StreamLocks(StreamLock.Volume);

public record DigitalCash(decimal Supply, decimal Demand, StreamLocks StreamLock) : DigitalEntry(Supply, Demand, StreamLock);

public record DigitalAsset(decimal Supply, decimal Demand, StreamLocks StreamLock) : DigitalEntry(Supply, Demand, StreamLock);

public record DigitalValue(DigitalCash cash, DigitalAsset asset) ;

public record SimulationStates(int StateId, LiquidityStreamStates Stream, AssetStreamStates Asset, ContractStreamStates Contract, LiquidityTransferStates Transfer, ConcessionStates Proof);

public record LiquidityStreamStates(int CashId, decimal StreamCashVolume, decimal StreamCashInventory)
{
    public static LiquidityStreamStates Empty { get { return new LiquidityStreamStates(0, 0, 0); } }    
}
public record AssetStreamStates(int AssetId, decimal AssetCashVolume, decimal AssetCashInventory, decimal AssetAssetVolume, decimal AssetAssetInventory);

public record ContractStreamStates(int ContractId, decimal ContractCashVolume, decimal ContractCashInventory, decimal ContractAssetVolume, decimal ContractAssetInventory);

public record LiquidityTransferStates(int TransferId, decimal TransferCashVolume, decimal TransferCashInventory, decimal TransferAssetVolume, decimal TransferAssetInventory);

public record ConcessionStates(int ProofId, decimal ProofDifficulty, decimal ProofStake, decimal ProofWork, int ProofSuperProofId, int ProofRelayProofId);

#endregion
