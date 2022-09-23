using Swopblock.Intentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swopblock
{
    public class SwopblockClient
    {
        public bool CaptureIntention(string intention)
        {
            IntentionTree tree = DemoWeb.GetTree();

            if (tree.Validate(intention))
            {
                MatchResult mr = IntentionBranch.MatchesPattern(intention, DemoWeb.pattern);

                if (mr.EmbeddedValues != null)
                {
                    if (mr.EmbeddedValues.Count > 0)
                    {
                        ContractState state = new ContractState(0, 0, 0, 0, 0);

                        if (WriteContract(state))
                        {
                            if (SignContract(state))
                            {
                                return BroadcastContract(state);
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool WriteContract(ContractState State)
        {

            return false;
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

        public bool SignContract(ContractState State)
        {
            return false;
        }

        public bool BroadcastContract(ContractState State)
        {
            return false;
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
}
