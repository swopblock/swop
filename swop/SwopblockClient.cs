// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using Swopblock.Intentions;
using System.Net.Http.Headers;

namespace Swopblock
{
    public record Intention();

    public record Report();

    public interface NetworkServices
    {

    }

    public interface IntentionServices
    {
        void CommitIntention(string intention);



        Contract Convert(string intention);
    }

    public interface ReportServices
    {
        void SubmitReport(Report report);

        Report Convert(string report);
    }

    public class SwopblockClient
    {
        public SwopblockClient()
        {
            ConsensusProcessor = new Consensus();
            ExecutionProcessor = new Execution();
        }

        public void PokeInEntryInput(LiquidityStream Entry) { }

        public void PokeInConsensusInput(LiquidityStream ConsensusEntry) { }

        public void PokeInExecutionInput(LiquidityStream ExectionEntry) { }

        public void PokeInExitInput(LiquidityStream ExitEntry) { }

        public LiquidityStream PeekAtEntryOutput() { return LiquidityStream.Empty; }

        public LiquidityStream PeekAtConsensusOutput() { return LiquidityStream.Empty; ; }

        public LiquidityStream PeekAtExecutionOuput() { return LiquidityStream.Empty; ; }

        public LiquidityStream PeekAtExitOutput() { return LiquidityStream.Empty; ; }

        #region Non-Public Members


        ConsensusModule consensus;

        ExecutionModule execution;

        Consensus ConsensusProcessor;

        Execution ExecutionProcessor;


        bool CaptureIntention(string intention)
        {
            IntentionTree tree = DemoWeb.GetTree();

            if (tree.Validate(intention))
            {
                MatchResult mr = IntentionBranch.MatchesPattern(intention, DemoWeb.pattern);

                if (mr.EmbeddedValues != null)
                {
                    if (mr.EmbeddedValues.Count > 0)
                    {
                        ContractStream state = new ContractStream(0, 0, 0, 0, 0);

                        //if (WriteContract(state))
                        //{
                        //    if (SignContract(state))
                        //    {
                        //        return BroadcastContract(state);
                        //    }
                        //}
                    }
                }
            }

            return false;
        }

        Contract WriteContract(string intention)
        {
            //UserInput.CommitIntention(intention);

            //return UserInput.Convert(intention);

            return null;
        }

        void WriteBidContract(ContractStream contract)
        {

        }

        void WriteAskContract(ContractStream contract)
        {

        }

        void WriteSellContract(ContractStream contract)
        {

        }
        void WriteBuyContract(ContractStream contract)
        {

        }

        void WriteSellAndBuyContract(ContractStream contract)
        {

        }

        bool SignContract(ContractStream State)
        {
            return false;
        }

        bool BroadcastContract(ContractStream State)
        {
            return false;
        }

        Queue<string> intentionsQueue = new Queue<string>();

        //ConsensusModule ConsensusModule

        Queue<LiquidityStreams> ProcessStatesQueue;

        Queue<string> reportsQueue = new Queue<string>();

        LiquidityStreams ParseFromIntention(string intention)
        {
            return null;
        }
        LiquidityStreams ParseFromTabbedTextLine(string line)
        {
            string[] fields = line.Split('\t', 25);

            int i = 0;

            //Process States
            int StateId = int.Parse(fields[i++]);

            //CashStreams
            int StreamId = int.Parse(fields[i++]);
            decimal StreamCashVolume = decimal.Parse(fields[i++]);
            decimal StreamCashInventory = decimal.Parse(fields[i++]);
            LiquidityStream stream = new LiquidityStream(StreamId, StreamCashVolume, StreamCashInventory);

            //StreamAssets
            int AssetId = int.Parse(fields[i++]);
            decimal AssetCashVolume = decimal.Parse(fields[i++]);
            decimal AssetCashInventory = decimal.Parse(fields[i++]);
            decimal AssetAssetVolume = decimal.Parse(fields[i++]);
            decimal AssetAssetInventory = decimal.Parse(fields[i++]);
            AssetStream asset = new AssetStream(AssetId, AssetCashVolume, AssetCashInventory, AssetAssetVolume, AssetAssetInventory);

            //AssetContracts
            int ContractId = int.Parse(fields[i++]);
            decimal ContractCashVolume = decimal.Parse(fields[i++]);
            decimal ContractCashInventory = decimal.Parse(fields[i++]);
            decimal ContractAssetVolume = decimal.Parse(fields[i++]);
            decimal ContractAssetInventory = decimal.Parse(fields[i++]);
            ContractStream contract = new ContractStream(ContractId, ContractCashVolume, ContractCashInventory, ContractAssetVolume, ContractAssetInventory);

            //ContractTransfers
            int TransferId = int.Parse(fields[i++]);
            decimal TransferCashVolume = decimal.Parse(fields[i++]);
            decimal TransferCashInventory = decimal.Parse(fields[i++]);
            decimal TransferAssetVolume = decimal.Parse(fields[i++]);
            decimal TransferAssetInventory = decimal.Parse(fields[i++]);
            LiquidityTransfer transfer = new LiquidityTransfer(TransferId, TransferCashVolume, TransferCashInventory, TransferAssetVolume, TransferAssetInventory);

            //TransferProofs
            int ProofId = int.Parse(fields[i++]);
            decimal ProofDifficulty = decimal.Parse(fields[i++]);
            decimal ProofStake = decimal.Parse(fields[i++]);
            decimal ProofWork = decimal.Parse(fields[i++]);
            int ProofCandidateProofId = int.Parse(fields[i++]);
            int ProofRelayProofId = int.Parse(fields[i++]);
            Concessions proof = new Concessions(ProofId, ProofDifficulty, ProofStake, ProofWork, ProofCandidateProofId, ProofRelayProofId);

            //Process States
            return new LiquidityStreams(StateId, stream, asset, contract, transfer, proof);
        }

        string ParseToTabbedTextLine(LiquidityStreams state)
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
            AssetStream asset = new AssetStream(AssetId, AssetCashVolume, AssetCashInventory, AssetAssetVolume, AssetAssetInventory);
            line += $"\t{AssetId}\t{AssetCashVolume}\t{AssetCashInventory}\t{AssetAssetVolume}\t{AssetAssetInventory}";

            //AssetContracts
            int ContractId = state.Contract.ContractId;
            decimal ContractCashVolume = state.Contract.ContractCashVolume;
            decimal ContractCashInventory = state.Contract.ContractCashInventory;
            decimal ContractAssetVolume = state.Contract.ContractAssetVolume;
            decimal ContractAssetInventory = state.Contract.ContractAssetInventory;
            ContractStream contract = new ContractStream(ContractId, ContractCashVolume, ContractCashInventory, ContractAssetVolume, ContractAssetInventory);
            line += $"\t{ContractId}\t{ContractCashVolume}\t{ContractCashInventory}\t{ContractAssetVolume}\t{ContractAssetInventory}";

            //ContractTransfers
            int TransferId = state.Transfer.TransferId;
            decimal TransferCashVolume = state.Transfer.TransferCashVolume;
            decimal TransferCashInventory = state.Transfer.TransferCashInventory;
            decimal TransferAssetVolume = state.Transfer.TransferAssetVolume;
            decimal TransferAssetInventory = state.Transfer.TransferAssetInventory;
            LiquidityTransfer transfer = new LiquidityTransfer(TransferId, TransferCashVolume, TransferCashInventory, TransferAssetVolume, TransferAssetInventory);
            line += $"\t{TransferId}\t{TransferCashVolume}\t{TransferCashInventory}\t{TransferAssetVolume}\t{TransferAssetInventory}";

            //TransferProofs
            int ProofId = state.Proof.ProofId;
            decimal ProofDifficulty = state.Proof.ProofDifficulty;
            decimal ProofStake = state.Proof.ProofStake;
            decimal ProofWork = state.Proof.ProofWork;
            int ProofCandidateProofId = state.Proof.ProofSuperProofId;
            int ProofRelayProofId = state.Proof.ProofRelayProofId;
            Concessions proof = new Concessions(ProofId, ProofDifficulty, ProofStake, ProofWork, ProofCandidateProofId, ProofRelayProofId);
            line += $"\t{ProofId}\t{ProofDifficulty}\t{ProofStake}\t{ProofWork}\t{ProofCandidateProofId}\t{ProofRelayProofId}";

            return line;
        }

        public void CommitIntention(string intention)
        {
            
        }

        public Contract Convert(string intention)
        {
            //return UserInput.Convert(intention);
            return null;
        }

        public sealed class Consensus
        {
            public void ParseInputFromArrivalMessage(string intention)
            {

            }

            public LiquidityStreams GetNetworkState()
            {
                return null;
            }
        }

        public sealed class Execution
        {
            public void ParseOutputToDepartureMessage()
            {

            }
        }

        #endregion

    }
}
