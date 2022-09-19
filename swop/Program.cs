// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock
using Swopblock.Intentions;
using Swopblock.Intentions.Utilities;
using System.Globalization;
using swop;
Console.WriteLine("Hello, Swopblock World!");

int simulationArgsIndex = 0;
int consensusArgsIndex = 0;
#region Intention Demonstration


IntentionTree Tree = swop.DemoWeb.GetTree();

string userInput = Console.ReadLine();

byte[] serByte = Tree.Serializer.Serialize(userInput);

string check = Tree.Serializer.Deserialize(serByte);

if(check.ToLower() == userInput.ToLower())

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


public record ProcessStates(int StateId, CashState Stream, AssetState Asset, ContractState Contract, TransferState Transfer, ProofState Proof);

public record CashState(int CashId, decimal StreamCashVolume, decimal StreamCashInventory);

public record AssetState(int AssetId, decimal AssetCashVolume, decimal AssetCashInventory, decimal AssetAssetVolume, decimal AssetAssetInventory);

public record ContractState(int ContractId, decimal ContractCashVolume, decimal ContractCashInventory, decimal ContractAssetVolume, decimal ContractAssetInventory);

public record TransferState(int TransferId, decimal TransferCashVolume, decimal TransferCashInventory, decimal TransferAssetVolume, decimal TransferAssetInventory);

public record ProofState(int ProofId, decimal ProofDifficulty, decimal ProofStake, decimal ProofWork, int ProofCandidateProofId, int ProofRelayProofId);


public sealed class SimulationModule
{
    int stateId = 0;
    int cashIdCount = 1;
    int assetIdCount = 2;
    int contractIdCount = 1;
    int transferIdCount = 2;
    int proofIdCount = 1;

    int proofStateCount = 0;

    Random random = new Random();

    public SimulationModule(string[] simulationArgs)
    {

    }

    public ProcessStates GetNextRandomState()
    {
        return new ProcessStates(stateId++, null, null, null, null, new ProofState(proofIdCount++, 0, 0, 0, 0, 0));
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

public sealed class SwopClient
{
    //Look here Brandon********************************************************
    //Look here Brandon********************************************************
    //Look here Brandon********************************************************

    public void CaptureIntention(string intention)
    {
        //Brandon put code here to write contract 

        WriteBidContract(new ContractState(1, 2, 3, 4, 5));

        WriteBidContract(new ContractState(1, 2, 3, 4, 5));
        WriteAskContract(new ContractState(1, 2, 3, 4, 5));
        WriteSellContract(new ContractState(1, 2, 3, 4, 5));
        WriteBuyContract(new ContractState(1, 2, 3, 4, 5));
        WriteSellAndBuyContract(new ContractState(1, 2, 3, 4, 5));

        SignContract();

        BroadcastContract();
    }

    public void WriteBidContract(ContractState contract)
    {

    }

    public void WriteAskContract(ContractState contract)
    {

    }

    public void WriteSellContract(ContractState contract)
    {

    }
    public void WriteBuyContract(ContractState contract)
    {

    }

    public void WriteSellAndBuyContract(ContractState contract)
    {

    }

    public void SignContract()
    {

    }

    public void BroadcastContract()
    {

    }

    Queue<string> intentionsQueue = new Queue<string>();

    //ConsensusModule ConsensusModule

    Queue<ProcessStates> ProcessStatesQueue;

    Queue<string> reportsQueue = new Queue<string>();

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
        CashState stream = new CashState(StreamId, StreamCashVolume, StreamCashInventory);

        //StreamAssets
        int AssetId = int.Parse(fields[i++]);
        decimal AssetCashVolume = decimal.Parse(fields[i++]);
        decimal AssetCashInventory = decimal.Parse(fields[i++]);
        decimal AssetAssetVolume = decimal.Parse(fields[i++]);
        decimal AssetAssetInventory = decimal.Parse(fields[i++]);
        AssetState asset = new AssetState(AssetId, AssetCashVolume, AssetCashInventory, AssetAssetVolume, AssetAssetInventory);

        //AssetContracts
        int ContractId = int.Parse(fields[i++]);
        decimal ContractCashVolume = decimal.Parse(fields[i++]);
        decimal ContractCashInventory = decimal.Parse(fields[i++]);
        decimal ContractAssetVolume = decimal.Parse(fields[i++]);
        decimal ContractAssetInventory = decimal.Parse(fields[i++]);
        ContractState contract = new ContractState(ContractId, ContractCashVolume, ContractCashInventory, ContractAssetVolume, ContractAssetInventory);

        //ContractTransfers
        int TransferId = int.Parse(fields[i++]);
        decimal TransferCashVolume = decimal.Parse(fields[i++]);
        decimal TransferCashInventory = decimal.Parse(fields[i++]);
        decimal TransferAssetVolume = decimal.Parse(fields[i++]);
        decimal TransferAssetInventory = decimal.Parse(fields[i++]);
        TransferState transfer = new TransferState(TransferId, TransferCashVolume, TransferCashInventory, TransferAssetVolume, TransferAssetInventory);

        //TransferProofs
        int ProofId = int.Parse(fields[i++]);
        decimal ProofDifficulty = decimal.Parse(fields[i++]);
        decimal ProofStake = decimal.Parse(fields[i++]);
        decimal ProofWork = decimal.Parse(fields[i++]);
        int ProofCandidateProofId = int.Parse(fields[i++]);
        int ProofRelayProofId = int.Parse(fields[i++]);
        ProofState proof = new ProofState(ProofId, ProofDifficulty, ProofStake, ProofWork, ProofCandidateProofId, ProofRelayProofId);

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
        int CashId = state.Stream.CashId;
        decimal StreamCashVolume = state.Stream.StreamCashVolume;
        decimal StreamCashInventory = state.Stream.StreamCashInventory;
        line += $"\t{CashId}\t{StreamCashVolume}\t{StreamCashInventory}";

        //StreamAssets
        int AssetId = state.Asset.AssetId;
        decimal AssetCashVolume = state.Asset.AssetCashVolume;
        decimal AssetCashInventory = state.Asset.AssetCashInventory;
        decimal AssetAssetVolume = state.Asset.AssetAssetVolume;
        decimal AssetAssetInventory = state.Asset.AssetAssetInventory;
        AssetState asset = new AssetState(AssetId, AssetCashVolume, AssetCashInventory, AssetAssetVolume, AssetAssetInventory);
        line += $"\t{AssetId}\t{AssetCashVolume}\t{AssetCashInventory}\t{AssetAssetVolume}\t{AssetAssetInventory}";

        //AssetContracts
        int ContractId = state.Contract.ContractId;
        decimal ContractCashVolume = state.Contract.ContractCashVolume;
        decimal ContractCashInventory = state.Contract.ContractCashInventory;
        decimal ContractAssetVolume = state.Contract.ContractAssetVolume;
        decimal ContractAssetInventory = state.Contract.ContractAssetInventory;
        ContractState contract = new ContractState(ContractId, ContractCashVolume, ContractCashInventory, ContractAssetVolume, ContractAssetInventory);
        line += $"\t{ContractId}\t{ContractCashVolume}\t{ContractCashInventory}\t{ContractAssetVolume}\t{ContractAssetInventory}";

        //ContractTransfers
        int TransferId = state.Transfer.TransferId;
        decimal TransferCashVolume = state.Transfer.TransferCashVolume;
        decimal TransferCashInventory = state.Transfer.TransferCashInventory;
        decimal TransferAssetVolume = state.Transfer.TransferAssetVolume;
        decimal TransferAssetInventory = state.Transfer.TransferAssetInventory;
        TransferState transfer = new TransferState(TransferId, TransferCashVolume, TransferCashInventory, TransferAssetVolume, TransferAssetInventory);
        line += $"\t{TransferId}\t{TransferCashVolume}\t{TransferCashInventory}\t{TransferAssetVolume}\t{TransferAssetInventory}";

        //TransferProofs
        int ProofId = state.Proof.ProofId;
        decimal ProofDifficulty = state.Proof.ProofDifficulty;
        decimal ProofStake = state.Proof.ProofStake;
        decimal ProofWork = state.Proof.ProofWork;
        int ProofCandidateProofId = state.Proof.ProofCandidateProofId;
        int ProofRelayProofId = state.Proof.ProofRelayProofId;
        ProofState proof = new ProofState(ProofId, ProofDifficulty, ProofStake, ProofWork, ProofCandidateProofId, ProofRelayProofId);
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

public class TestSystem
{
    public TestSystemNetworks[] networks;

    public TestSystem(int networkCount, int clientCount, int serverCount, int contractCount, int transferCount, int proofCount)
    {
        networks = new TestSystemNetworks[networkCount];

        for (int i = 0; i < networkCount; i++)
        {
            networks[i] = new TestSystemNetworks(clientCount, serverCount, contractCount, transferCount, proofCount);
        }
    }   
}

public class TestSystemNetworks
{
    public TestSwopClients[] clients;

    public TestSystemNetworks(int clientCount, int serverCount, int contractCount, int transferCount, int proofCount)
    {
        clients = new TestSwopClients[clientCount];

        for (int i = 0; i < clientCount; i++)
        {
            clients[i] = new TestSwopClients(serverCount, contractCount, transferCount, proofCount);
        }
    }   
}

public class TestSwopClients
{
    public SwopClient SwopClient;

    public SimAssetServers[] servers;

    public TestSwopClients(int serverCount, int contractCount, int transferCount, int proofCount)
    {
        SwopClient = new SwopClient();

        servers = new SimAssetServers[serverCount];

        for (int i = 0; i < serverCount; i++)
        {
            servers[i] = new SimAssetServers(contractCount, transferCount, proofCount);
        }
    }
}

public class SimAssetServers
{
    public TestClientContracts[] contracts;

    public SimAssetServers(int contractCount, int transferCount, int proofCount)
    {
        contracts = new TestClientContracts[contractCount];

        for (int i = 0; i < contractCount; i++)
        {
            contracts[i] = new TestClientContracts(transferCount, proofCount);
        }
    }
}

public class TestClientContracts
{
    public TestContractTransfers[] transfers;

    public TestClientContracts(int transferCount, int proofCount)
    {
        transfers = new TestContractTransfers[transferCount];

        for (int i = 0; i < transferCount; i++)
        {
            transfers[i] = new TestContractTransfers(proofCount);
        }
    }
}

public class TestContractTransfers
{
    public TestTransferProofs[] proofs;

    public TestContractTransfers(int proofCount)
    {
        proofs = new TestTransferProofs[proofCount];

        for (int i = 0; i < proofCount; i++)
        {
            proofs[i] = new TestTransferProofs();
        }
    }
}

public class TestTransferProofs
{ 
}


