using Swopblock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTest
{
    public sealed class Simulation
    {
        public SwopblockClientMove PublicSwopblockClient;

        public SimulationSystemNetworks[] networks;

        [Theory]
        [InlineData(2, 3, 4, 5, 6, 7)]
        [InlineData(7, 6, 5, 4, 3, 2)]
        public void BuildSimultion(int networkCount, int clientCount, int serverCount, int contractCount, int transferCount, int proofCount)
        {
            networks = new SimulationSystemNetworks[networkCount];

            for (int i = 0; i < networkCount; i++)
            {
                networks[i] = new SimulationSystemNetworks();

                networks[i].BuildSimultion(clientCount, serverCount, contractCount, transferCount, proofCount);
            }

            Assert.Equal(networkCount, networks.Length);
        }
    }

    public sealed class SimulationSystemNetworks
    {
        public SimulationSwopblockClients[] clients;

        [Theory]
        [InlineData(2, 3, 4, 5, 6)]
        public void BuildSimultion(int clientCount, int serverCount, int contractCount, int transferCount, int proofCount)
        {
            clients = new SimulationSwopblockClients[clientCount];

            for (int i = 0; i < clientCount; i++)
            {
                clients[i] = new SimulationSwopblockClients();

                clients[i].BuildSimultion(serverCount, contractCount, transferCount, proofCount);
            }

            Assert.Equal(clientCount, clients.Length);
        }
    }

    public sealed class SimulationSwopblockClients
    {
        public SwopblockClientMove SwopblockClient;

        public SimulationAssetServers[] servers;

        [Theory]
        [InlineData(2, 3, 4, 5)]
        public void BuildSimultion(int serverCount, int contractCount, int transferCount, int proofCount)
        {
            SwopblockClient = new SwopblockClientMove();

            servers = new SimulationAssetServers[serverCount];

            for (int i = 0; i < serverCount; i++)
            {
                servers[i] = new SimulationAssetServers();

                servers[i].BuildSimultion(contractCount, transferCount, proofCount);
            }

            Assert.Equal(serverCount, servers.Length);

            Assert.NotNull(SwopblockClient);
        }
    }

    public sealed class SimulationAssetServers
    {
        public SimulationClientContracts[] contracts;

        [Theory]
        [InlineData(2, 3, 4)]
        public void BuildSimultion(int contractCount, int transferCount, int proofCount)
        {
            contracts = new SimulationClientContracts[contractCount];

            for (int i = 0; i < contractCount; i++)
            {
                contracts[i] = new SimulationClientContracts();

                contracts[i].BuildSimultion(transferCount, proofCount);
            }

            Assert.Equal(contractCount, contracts.Length);
        }
    }

    public class SimulationClientContracts
    {
        public SimulationContractTransfers[] transfers;

        [Theory]
        [InlineData(2, 3)]
        public void BuildSimultion(int transferCount, int proofCount)
        {
            transfers = new SimulationContractTransfers[transferCount];

            for (int i = 0; i < transferCount; i++)
            {
                transfers[i] = new SimulationContractTransfers();

                transfers[i].BuildSimultion(proofCount);
            }

            Assert.Equal(transferCount, transfers.Length);
        }
    }

    public class SimulationContractTransfers
    {
        public SimulationTransferProofs[] proofs;

        [Theory]
        [InlineData(2)]
        public void BuildSimultion(int proofCount)
        {
            proofs = new SimulationTransferProofs[proofCount];

            for (int i = 0; i < proofCount; i++)
            {
                proofs[i] = new SimulationTransferProofs();
            }

            Assert.Equal(proofCount, proofs.Length);
        }
    }

    public class SimulationTransferProofs
    {
    }


}
