// See https://github.com/swopblock

Console.WriteLine("Hello, Swopblock World!");


int stateCount = args.Length > 0 ? int.Parse(args[0]) : 2;

int streamCount = args.Length > 1 ? int.Parse(args[0]) : 3;

int assetCount = args.Length > 2 ? int.Parse(args[1]) : 4;

int contractCount = args.Length > 3 ? int.Parse(args[2]) : 5;

int transferCount = args.Length > 4 ? int.Parse(args[4]) : 6;

int proofCount = args.Length > 5 ? int.Parse(args[5]) : 7;


public record ProcessStates(int StateId, CashStreams Stream, StreamAssets Asset, AssetContracts Contract, ContractTransfers Transfer, TransferProofs Proof);

public record CashStreams(int StreamId, decimal StreamCashVolume, decimal StreamCashInventory);

public record StreamAssets(int AssetId, decimal AssetCashVolume, decimal AssetCashInventory, decimal AssetAssetVolume, decimal AssetAssetInventory);

public record AssetContracts(int ContractId, decimal ContractCashVolume, decimal ContractCashInventory, decimal ContractAssetVolume, decimal ContractAssetInventory);

public record ContractTransfers(int TransferId, decimal TransferCashVolume, decimal TransferCashInventory, decimal TransferAssetVolume, decimal TransferAssetInventory);

public record TransferProofs(int ProofId, decimal ProofDifficulty, decimal ProofStake, decimal ProofWork, int ProofCandidateProofId, int ProofRelayProofId);


public class Swop
{
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
        CashStreams stream = new CashStreams(StreamId, StreamCashVolume, StreamCashInventory);

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
}

public class ClientNetworks
{
    public CashClients TheClients = new CashClients();
}

public class CashClients
{
    public CashStreams CashState;

    public AssetServers TheServers = new AssetServers();

}

public class AssetServers
{
    StreamAssets AssetState;

    public ServerContracts TheContracts = new ServerContracts();
}

public class ServerContracts
{
    public AssetContracts ContractState;

    public ContractTransfers TransferState;

    public TransferProofs ProofState;
}


