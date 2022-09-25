using Swopblock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    public class TestSimulation : Simulation
    {
        int ContractBatchCount = 10;

        LiquidityStreamStates Entry, Consensus, Execution, Exit;

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
            foreach (var network in networks)
            {
                foreach (var client in network.clients)
                {
                    foreach (var servers in client.servers)
                    {
                        for (int i = 0; i < ContractBatchCount; i++)
                        {
                            client.Exit.PeekAtExitOutput();
                        }
                    }
                }
            }

            base.PokeInEntryInput(Entry);
            return base.PeekAtExitOutput();
        }

        public override void PokeInConsensusInput(LiquidityStreamStates ConsensusEntry)
        {
            base.PokeInConsensusInput(ConsensusEntry);
        }

        public override void PokeInEntryInput(LiquidityStreamStates Entry)
        {
            foreach (var network in networks)
            {
                foreach (var client in network.clients)
                {
                    foreach (var server in client.servers)
                    {
                        foreach (var contract in server.contracts)
                        {
                            for (int i = 0; i < ContractBatchCount; i++)
                            {
                                //
                            }
                        }
                    }
                }
            }

            base.PokeInEntryInput(Entry);
        }

        public override void PokeInExecutionInput(LiquidityStreamStates ExectionEntry)
        {
            base.PokeInExecutionInput(ExectionEntry);
        }

        public override void PokeInExitInput(LiquidityStreamStates ExitEntry)
        {
            base.PokeInExitInput(ExitEntry);
        }

        public SwopblockModule PublicSwopblockClient;

        //public SimulationSystemNetworks[] networks;

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
        public TestSimulationSwopblockClients()
        {
            Entry = new SwopblockModule();

            Consensus = new SwopblockModule();

            Execution = new SwopblockModule();

            Exit = new SwopblockModule();
        }

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
