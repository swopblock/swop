// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock
using Swopblock.Intentions;
using Swopblock.Intentions.Utilities;
using System.Globalization;
using swop.SwopCode;
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


