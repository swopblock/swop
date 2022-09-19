// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock
using Swopblock.Intentions;
using Swopblock.Intentions.Utilities;
using System.Globalization;

Console.WriteLine("Hello, Swopblock World!");

int simulationArgsIndex = 0;

IntentionTree Tree = swop.DemoWeb.GetTree();

string userInput = Console.ReadLine();

byte[] serByte = Tree.Serializer.Serialize(userInput);

string check = Tree.Serializer.Deserialize(serByte);

if (check.ToLower() == userInput.ToLower())
{
    Console.WriteLine("input is valid");
    //          [0] [1] [2]     [3]
    MatchResult mr = IntentionBranch.MatchesPattern(userInput, "i want to * * * * * * for * *");
}
else
{
    Console.WriteLine("input error");
}
#region Intention Demonstration


IntentionTree Tree = swop.DemoWeb.GetTree();

string userInput = Console.ReadLine();

byte[] serByte = Tree.Serializer.Serialize(userInput);

string check = Tree.Serializer.Deserialize(serByte);

if(check.ToLower() == userInput.ToLower())
int consensusArgsIndex = 0;
{
    Console.WriteLine("input is valid");
                                                              //          [0] [1] [2]     [3]
    MatchResult mr = IntentionBranch.MatchesPattern(userInput, "i want to * * * * * * for * *");
}
else
{
    Console.WriteLine("input error");
}

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


SimulationModule simulationModule = new SimulationModule(simulationArgs);

ConsensusModule consensusModule = new ConsensusModule(consensusArgs);

ExecutionModule executionModule = new ExecutionModule(executionArgs);


public record ProcessStates(int StateId, NetworkStreams Stream, StreamAssets Asset, AssetContracts Contract, ContractTransfers Transfer, TransferProofs Proof);

public record NetworkStreams(int StreamId, decimal StreamCashVolume, decimal StreamCashInventory);

public record StreamAssets(int AssetId, decimal AssetCashVolume, decimal AssetCashInventory, decimal AssetAssetVolume, decimal AssetAssetInventory);

public record AssetContracts(int ContractId, decimal ContractCashVolume, decimal ContractCashInventory, decimal ContractAssetVolume, decimal ContractAssetInventory);

public record ContractTransfers(int TransferId, decimal TransferCashVolume, decimal TransferCashInventory, decimal TransferAssetVolume, decimal TransferAssetInventory);

public record TransferProofs(int ProofId, decimal ProofDifficulty, decimal ProofStake, decimal ProofWork, int ProofCandidateProofId, int ProofRelayProofId);


public sealed class SimulationModule
{
    public SimulationModule(string[] simulationArgs)
    {

    }

    public ProcessStates CurrentState { get; set; }

    public ProcessStates ResetState()
    {
        return null;
    }

    public ProcessStates GetNextState()
    {
        return null;
    }
}

public sealed class ConsensusModule
{
    public ConsensusModule(string[] consensusArgs)
    {

    }


    public ProcessStates GetFirstContract()
    {
        return null;
    }

    public ProcessStates GetNextContract()
    {
        return null;
    }
}

public sealed class ExecutionModule
{
    public ExecutionModule(string[] executionArgs)
    {

    }

    public ProcessStates GetFirstContract()
    {
        return null;
    }

    public ProcessStates GetNextContract()
    {
        return null;
    }
}

public sealed class Swop
{
    public ProcessStates ParseFromIntention(string intention)
    {
        return null;
    }
    public ProcessStates ParseFromTabbedTextLine(string line)
    {
        string[] fields = line.Split('\t', 25);

        int i = 0;

        //Process States
        int StateId = int.Parse(fields[i++]);

        //CashStreams
        int StreamId = int.Parse(fields[i++]);
        decimal StreamCashVolume = decimal.Parse(fields[i++]);
        decimal StreamCashInventory = decimal.Parse(fields[i++]);
        NetworkStreams stream = new NetworkStreams(StreamId, StreamCashVolume, StreamCashInventory);

        //StreamAssets
        int AssetId = int.Parse(fields[i++]);
        decimal AssetCashVolume = decimal.Parse(fields[i++]);
        decimal AssetCashInventory = decimal.Parse(fields[i++]);
        decimal AssetAssetVolume = decimal.Parse(fields[i++]);
        decimal AssetAssetInventory = decimal.Parse(fields[i++]);
        StreamAssets asset = new StreamAssets(AssetId, AssetCashVolume, AssetCashInventory, AssetAssetVolume, AssetAssetInventory);

        //AssetContracts
        int ContractId = int.Parse(fields[i++]);
        decimal ContractCashVolume = decimal.Parse(fields[i++]);
        decimal ContractCashInventory = decimal.Parse(fields[i++]);
        decimal ContractAssetVolume = decimal.Parse(fields[i++]);
        decimal ContractAssetInventory = decimal.Parse(fields[i++]);
        AssetContracts contract = new AssetContracts(ContractId, ContractCashVolume, ContractCashInventory, ContractAssetVolume, ContractAssetInventory);

        //ContractTransfers
        int TransferId = int.Parse(fields[i++]);
        decimal TransferCashVolume = decimal.Parse(fields[i++]);
        decimal TransferCashInventory = decimal.Parse(fields[i++]);
        decimal TransferAssetVolume = decimal.Parse(fields[i++]);
        decimal TransferAssetInventory = decimal.Parse(fields[i++]);
        ContractTransfers transfer = new ContractTransfers(TransferId, TransferCashVolume, TransferCashInventory, TransferAssetVolume, TransferAssetInventory);

        //TransferProofs
        int ProofId = int.Parse(fields[i++]);
        decimal ProofDifficulty = decimal.Parse(fields[i++]);
        decimal ProofStake = decimal.Parse(fields[i++]);
        decimal ProofWork = decimal.Parse(fields[i++]);
        int ProofCandidateProofId = int.Parse(fields[i++]);
        int ProofRelayProofId = int.Parse(fields[i++]);
        TransferProofs proof = new TransferProofs(ProofId, ProofDifficulty, ProofStake, ProofWork, ProofCandidateProofId, ProofRelayProofId);

        //Process States
        return new ProcessStates(StateId, stream, asset, contract, transfer, proof);
    }

    public string ParseToTabbedTextLine(ProcessStates state)
    {
        string line = string.Empty;

        //Process States
        int StateId = state.StateId;
        line += $"{StateId}";

        //CashStreams
        int StreamId = state.Stream.StreamId;
        decimal StreamCashVolume = state.Stream.StreamCashVolume;
        decimal StreamCashInventory = state.Stream.StreamCashInventory;
        line += $"\t{StreamId}\t{StreamCashVolume}\t{StreamCashInventory}";

        //StreamAssets
        int AssetId = state.Asset.AssetId;
        decimal AssetCashVolume = state.Asset.AssetCashVolume;
        decimal AssetCashInventory = state.Asset.AssetCashInventory;
        decimal AssetAssetVolume = state.Asset.AssetAssetVolume;
        decimal AssetAssetInventory = state.Asset.AssetAssetInventory;
        StreamAssets asset = new StreamAssets(AssetId, AssetCashVolume, AssetCashInventory, AssetAssetVolume, AssetAssetInventory);
        line += $"\t{AssetId}\t{AssetCashVolume}\t{AssetCashInventory}\t{AssetAssetVolume}\t{AssetAssetInventory}";

        //AssetContracts
        int ContractId = state.Contract.ContractId;
        decimal ContractCashVolume = state.Contract.ContractCashVolume;
        decimal ContractCashInventory = state.Contract.ContractCashInventory;
        decimal ContractAssetVolume = state.Contract.ContractAssetVolume;
        decimal ContractAssetInventory = state.Contract.ContractAssetInventory;
        AssetContracts contract = new AssetContracts(ContractId, ContractCashVolume, ContractCashInventory, ContractAssetVolume, ContractAssetInventory);
        line += $"\t{ContractId}\t{ContractCashVolume}\t{ContractCashInventory}\t{ContractAssetVolume}\t{ContractAssetInventory}";

        //ContractTransfers
        int TransferId = state.Transfer.TransferId;
        decimal TransferCashVolume = state.Transfer.TransferCashVolume;
        decimal TransferCashInventory = state.Transfer.TransferCashInventory;
        decimal TransferAssetVolume = state.Transfer.TransferAssetVolume;
        decimal TransferAssetInventory = state.Transfer.TransferAssetInventory;
        ContractTransfers transfer = new ContractTransfers(TransferId, TransferCashVolume, TransferCashInventory, TransferAssetVolume, TransferAssetInventory);
        line += $"\t{TransferId}\t{TransferCashVolume}\t{TransferCashInventory}\t{TransferAssetVolume}\t{TransferAssetInventory}";

        //TransferProofs
        int ProofId = state.Proof.ProofId;
        decimal ProofDifficulty = state.Proof.ProofDifficulty;
        decimal ProofStake = state.Proof.ProofStake;
        decimal ProofWork = state.Proof.ProofWork;
        int ProofCandidateProofId = state.Proof.ProofCandidateProofId;
        int ProofRelayProofId = state.Proof.ProofRelayProofId;
        TransferProofs proof = new TransferProofs(ProofId, ProofDifficulty, ProofStake, ProofWork, ProofCandidateProofId, ProofRelayProofId);
        line += $"\t{ProofId}\t{ProofDifficulty}\t{ProofStake}\t{ProofWork}\t{ProofCandidateProofId}\t{ProofRelayProofId}";

        return line;
    }

    public sealed class Consensus
    {
        public void ParseInputFromArrivalMessage(string intention)
        {

        }
    }

    public sealed class Execution
    {
        public void ParseOutputToDepartureMessage()
        {

        }
    }
    
}

public class ClientNetworks
{
    public CashClients TheClients = new CashClients();

    public void ParseIntentionsFromFile(string intentions)
    {

    }

    public void ParseIntentionFromConsole(string intention)
    {

    }

    public void ParseIntentionsFromStandardInput(string intentions)
    {
        //Console.R
    }
}

public class CashClients
{
    public NetworkStreams StreamState;

    public AssetServers TheServers = new AssetServers();

}

public class AssetServers
{
    StreamAssets AssetState;

    public ClientContracts TheContracts = new ClientContracts();
}

public class ClientContracts
{
    public AssetContracts ContractState;

    public ContractTransfers TransferState;

    public TransferProofs ProofState;
}


