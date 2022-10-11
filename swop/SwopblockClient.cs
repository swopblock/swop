// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using Swopblock;
using Swopblock.Intentions;
using System.Net.Http.Headers;

namespace Swopblock
{
    public record Intention();

    public record Report();

    /// <summary>
    /// This is the real swopblock protocol code driven by the Poke and Peek calls.
    /// </summary>
    public class SwopblockModule
    {
        public SimulationStates entry;

        public Swopblock.ConsensusModule consensus;

        public SimulationStates pipe;

        public Swopblock.ExecutionModule execution;

        public SimulationStates exit;

        public SwopblockModule(string[] consensusArgs, string[] executionArgs)
        {
            consensus = new ConsensusModule(consensusArgs);

            execution = new ExecutionModule(executionArgs);
        }

        public virtual void PokeInEntryInput(SimulationStates state)
        {
            entry = state;

            pipe = consensus.Run(entry);

            exit = execution.Run(pipe);
        }

        public virtual void PokeInConsensusInput(SimulationStates state)
        {
            pipe = consensus.Run(state);

            exit = execution.Run(pipe);
        }

        public virtual void PokeInExecutionInput(SimulationStates state)
        {
            exit = execution.Run(state);
        }

        public virtual void PokeInExitInput(SimulationStates state)
        {
            exit = state;
        }

        public virtual SimulationStates PeekAtEntryOutput()
        {
            return entry;
        }

        public virtual SimulationStates PeekAtConsensusOutput()
        {
            return pipe;
        }

        public virtual SimulationStates PeekAtExecutionOuput()
        {
            return exit;
        }

        public virtual SimulationStates PeekAtExitOutput()
        {
            return exit;
        }

        public int StepState(SimulationStates input, out SimulationStates output)
        {
            SimulationStates pipe;

            consensus.StepState(input, out pipe);

            execution.StepState(pipe, out output);

            return 0;
        }

        #region Non-Public Members



        bool CaptureIntention(string intention)
        {
            IntentionTree tree = DemoWeb.GetTree();

            if (tree.Validate(intention))
            {
                //MatchResult mr = IntentionBranch.MatchesPattern(intention, DemoWeb.pattern);

                //if (mr.EmbeddedValues != null)
                //{
                //    if (mr.EmbeddedValues.Count > 0)
                //    {
                //        //ContractStreamStates state = new ContractStreamStates(0, null, null, null);

                //        //if (WriteContract(state))
                //        //{
                //        //    if (SignContract(state))
                //        //    {
                //        //        return BroadcastContract(state);
                //        //    }
                //        //}
                //    }
                //}
            }

            return false;
        }

        AddressStates WriteContract(string intention)
        {
            //UserInput.CommitIntention(intention);

            //return UserInput.Convert(intention);

            return null;
        }

        void WriteBidContract(AddressStates contract)
        {

        }

        void WriteAskContract(AddressStates contract)
        {

        }

        void WriteSellContract(AddressStates contract)
        {

        }
        void WriteBuyContract(AddressStates contract)
        {

        }

        void WriteSellAndBuyContract(AddressStates contract)
        {

        }

        bool SignContract(AddressStates State)
        {
            return false;
        }

        bool BroadcastContract(AddressStates State)
        {
            return false;
        }

        Queue<string> intentionsQueue = new Queue<string>();

        //ConsensusModule ConsensusModule

        //Queue<SimulationStates> ProcessStatesQueue;

        Queue<string> reportsQueue = new Queue<string>();

        //SimulationStates ParseFromIntention(string intention)
        //{
        //    return null;
        //}
        ////SimulationStates ParseFromTabbedTextLine(string line)
        //{
        //    string[] fields = line.Split('\t', 25);

        //    int i = 0;

        //    //Process States
        //    int StateId = int.Parse(fields[i++]);

        //    //CashStreams
        //    int StreamId = int.Parse(fields[i++]);
        //    decimal StreamCashVolume = decimal.Parse(fields[i++]);
        //    decimal StreamCashInventory = decimal.Parse(fields[i++]);
        //    //LiquidityStreamStates stream = new LiquidityStreamStates(StreamId, StreamCashVolume, StreamCashInventory);

        //    //StreamAssets
        //    int AssetId = int.Parse(fields[i++]);
        //    decimal AssetCashVolume = decimal.Parse(fields[i++]);
        //    decimal AssetCashInventory = decimal.Parse(fields[i++]);
        //    decimal AssetAssetVolume = decimal.Parse(fields[i++]);
        //    decimal AssetAssetInventory = decimal.Parse(fields[i++]);
        //    //AssetStreamStates asset = new AssetStreamStates(AssetId, AssetCashVolume, AssetCashInventory, AssetAssetVolume, AssetAssetInventory);

        //    //AssetContracts
        //    int ContractId = int.Parse(fields[i++]);
        //    decimal ContractCashVolume = decimal.Parse(fields[i++]);
        //    decimal ContractCashInventory = decimal.Parse(fields[i++]);
        //    decimal ContractAssetVolume = decimal.Parse(fields[i++]);
        //    decimal ContractAssetInventory = decimal.Parse(fields[i++]);
        //    //ContractStreamStates contract = new ContractStreamStates(ContractId, ContractCashVolume, ContractCashInventory, ContractAssetVolume, ContractAssetInventory);

        //    //ContractTransfers
        //    int TransferId = int.Parse(fields[i++]);
        //    decimal TransferCashVolume = decimal.Parse(fields[i++]);
        //    decimal TransferCashInventory = decimal.Parse(fields[i++]);
        //    decimal TransferAssetVolume = decimal.Parse(fields[i++]);
        //    decimal TransferAssetInventory = decimal.Parse(fields[i++]);
        //    //LiquidityTransferStates transfer = new LiquidityTransferStates(TransferId, TransferCashVolume, TransferCashInventory, TransferAssetVolume, TransferAssetInventory);

        //    //TransferProofs
        //    int ProofId = int.Parse(fields[i++]);
        //    decimal ProofDifficulty = decimal.Parse(fields[i++]);
        //    decimal ProofStake = decimal.Parse(fields[i++]);
        //    decimal ProofWork = decimal.Parse(fields[i++]);
        //    int ProofCandidateProofId = int.Parse(fields[i++]);
        //    int ProofRelayProofId = int.Parse(fields[i++]);
        //    //ConsensusStates proof = new ConsensusStates(ProofId, ProofDifficulty, ProofStake, ProofWork, ProofCandidateProofId, ProofRelayProofId);

        //    //Process States
        //    return null;// new SimulationStates(StateId, stream, asset, contract, transfer, proof);
        //}

        //string ParseToTabbedTextLine(SimulationStates state)
        //{
            //string line = string.Empty;

            ////Process States
            //int StateId = state.StateId;
            //line += $"{StateId}";

            ////CashStreams
            //int CashId = state.Stream.CashId;
            //decimal StreamCashVolume = state.Stream.StreamCashVolume;
            //decimal StreamCashInventory = state.Stream.StreamCashInventory;
            //line += $"\t{CashId}\t{StreamCashVolume}\t{StreamCashInventory}";

            ////StreamAssets
            //int AssetId = state.Asset.AssetId;
            //decimal AssetCashVolume = state.Asset.AssetCashVolume;
            //decimal AssetCashInventory = state.Asset.AssetCashInventory;
            //decimal AssetAssetVolume = state.Asset.AssetAssetVolume;
            //decimal AssetAssetInventory = state.Asset.AssetAssetInventory;
            //AssetStreamStates asset = new AssetStreamStates(AssetId, AssetCashVolume, AssetCashInventory, AssetAssetVolume, AssetAssetInventory);
            //line += $"\t{AssetId}\t{AssetCashVolume}\t{AssetCashInventory}\t{AssetAssetVolume}\t{AssetAssetInventory}";

            ////AssetContracts
            //int ContractId = state.Contract.ContractId;
            //decimal ContractCashVolume = state.Contract.ContractCashVolume;
            //decimal ContractCashInventory = state.Contract.ContractCashInventory;
            //decimal ContractAssetVolume = state.Contract.ContractAssetVolume;
            //decimal ContractAssetInventory = state.Contract.ContractAssetInventory;
            //ContractStreamStates contract = new ContractStreamStates(ContractId, ContractCashVolume, ContractCashInventory, ContractAssetVolume, ContractAssetInventory);
            //line += $"\t{ContractId}\t{ContractCashVolume}\t{ContractCashInventory}\t{ContractAssetVolume}\t{ContractAssetInventory}";

            ////ContractTransfers
            //int TransferId = state.Transfer.TransferId;
            //decimal TransferCashVolume = state.Transfer.TransferCashVolume;
            //decimal TransferCashInventory = state.Transfer.TransferCashInventory;
            //decimal TransferAssetVolume = state.Transfer.TransferAssetVolume;
            //decimal TransferAssetInventory = state.Transfer.TransferAssetInventory;
            //LiquidityTransferStates transfer = new LiquidityTransferStates(TransferId, TransferCashVolume, TransferCashInventory, TransferAssetVolume, TransferAssetInventory);
            //line += $"\t{TransferId}\t{TransferCashVolume}\t{TransferCashInventory}\t{TransferAssetVolume}\t{TransferAssetInventory}";

            ////TransferProofs
            //int ProofId = state.Proof.ProofId;
            //decimal ProofDifficulty = state.Proof.ProofDifficulty;
            //decimal ProofStake = state.Proof.ProofStake;
            //decimal ProofWork = state.Proof.ProofWork;
            //int ProofCandidateProofId = state.Proof.ProofSuperProofId;
            //int ProofRelayProofId = state.Proof.ProofRelayProofId;
            //ConsensusStates proof = new ConsensusStates(ProofId, ProofDifficulty, ProofStake, ProofWork, ProofCandidateProofId, ProofRelayProofId);
            //line += $"\t{ProofId}\t{ProofDifficulty}\t{ProofStake}\t{ProofWork}\t{ProofCandidateProofId}\t{ProofRelayProofId}";

            //return line;

        //    return null;
        //}

        void CommitIntention(string intention)
        {

        }

        AddressStates Convert(string intention)
        {
            //return UserInput.Convert(intention);
            return null;
        }
        #endregion
    }
}
