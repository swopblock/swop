using Swopblock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    public class TestSimulation : SimulationModule
    {
        public override LiquidityStreamStates PeekAtConsensusOutput()
        {
            return base.PeekAtConsensusOutput();
        }

        public override LiquidityStreamStates PeekAtEntryOutput()
        {
            return base.PeekAtEntryOutput();
        }

        public override LiquidityStreamStates PeekAtExecutionOuput()
        {
            return base.PeekAtExecutionOuput();
        }

        public override LiquidityStreamStates PeekAtExitOutput()
        {
            return base.PeekAtExitOutput();
        }

        public override void PokeInConsensusInput(LiquidityStreamStates state)
        {
            base.PokeInConsensusInput(state);
        }

        public override void PokeInEntryInput(LiquidityStreamStates state)
        {
            base.PokeInEntryInput(state);
        }

        public override void PokeInExecutionInput(LiquidityStreamStates state)
        {
            base.PokeInExecutionInput(state);
        }

        public override void PokeInExitInput(LiquidityStreamStates state)
        {
            base.PokeInExitInput(state);
        }

        [Theory]
        [InlineData(2, 3, 4, 5, 6, 7)]
        [InlineData(7, 6, 5, 4, 3, 2)]
        public override void BuildSimultion(int networkCount, int clientCount, int serverCount, int contractCount, int transferCount, int proofCount)
        {
            base.BuildSimultion(networkCount, clientCount, serverCount, contractCount, transferCount, proofCount);

            Assert.Equal(networkCount, networks.Length);
        }
    }

    public class TestSimulationSystemNetworks : SimulationSystemNetworks
    {
        [Theory]
        [InlineData(2, 3, 4, 5, 6)]
        public override void BuildSimultion(int clientCount, int serverCount, int contractCount, int transferCount, int proofCount)
        {
            base.BuildSimultion(clientCount, serverCount, contractCount, transferCount, proofCount);

            Assert.Equal(clientCount, clients.Length);
        }
    }

    public class TestSimulationSwopblockClients : SimulationSwopblockClients
    {
        [Theory]
        [InlineData(2, 3, 4, 5)]
        public override void BuildSimultion(int serverCount, int contractCount, int transferCount, int proofCount)
        {
            base.BuildSimultion(serverCount, contractCount, transferCount, proofCount);

            Assert.Equal(serverCount, servers.Length);
        }
    }

    public class TestSimulationAssetServers : SimulationAssetServers
    {
        [Theory]
        [InlineData(2, 3, 4)]
        public override void BuildSimultion(int contractCount, int transferCount, int proofCount)
        {
            base.BuildSimultion(contractCount, transferCount, proofCount);

            Assert.Equal(contractCount, contracts.Length);
        }
    }

    public class TestSimulationClientContracts : SimulationClientContracts
    {
        [Theory]
        [InlineData(2, 3)]
        public override void BuildSimultion(int transferCount, int proofCount)
        {
            base.BuildSimultion(transferCount, proofCount);

            Assert.Equal(transferCount, transfers.Length);
        }
    }

    public class TestSimulationContractTransfers : SimulationContractTransfers
    {
        [Theory]
        [InlineData(2)]
        public override void BuildSimultion(int proofCount)
        {
            base.BuildSimultion(proofCount);

            Assert.Equal(proofCount, proofs.Length);
        }
    }

    public class SimulationTransferProofs
    {
    }


}
