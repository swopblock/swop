using swop.SwopCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTest
{
    public sealed class Simulation
    {
        public SimulationSystemNetworks[] networks;

        [Theory]
        [InlineData(2,3,4,5,6,7)]
        public Simulation BuildSimultion(int networkCount, int clientCount, int serverCount, int contractCount, int transferCount, int proofCount)
        {
            var simulation = new Simulation();

            networks = new SimulationSystemNetworks[networkCount];

            for (int i = 0; i < networkCount; i++)
            {
                networks[i] = new SimulationSystemNetworks();

                networks[i].BuildSimultion(clientCount, serverCount, contractCount, transferCount, proofCount);
            }

            Assert.Equal(networkCount, networks.Length);

            return simulation;
        }
    }

    public sealed class SimulationSystemNetworks
    {
        public SimulationSwopblockClients[] clients;

        [Theory]
        [InlineData(2, 3, 4, 5, 6)]
        public SimulationSystemNetworks BuildSimultion(int clientCount, int serverCount, int contractCount, int transferCount, int proofCount)
        {
            var network = new SimulationSystemNetworks();

            clients = new SimulationSwopblockClients[clientCount];

            for (int i = 0; i < clientCount; i++)
            {
                clients[i] = new SimulationSwopblockClients();

                clients[i].BuildSimultion(serverCount, contractCount, transferCount, proofCount);
            }

            Assert.Equal(clientCount, clients.Length);

            return network;
        }
    }

    public sealed class SimulationSwopblockClients
    {
        public SwopblockClient SwopblockClient;

        public SimulationAssetServers[] servers;

        [Theory]
        [InlineData(2, 3, 4, 5)]
        public SimulationSwopblockClients BuildSimultion(int serverCount, int contractCount, int transferCount, int proofCount)
        {
            var client = new SimulationSwopblockClients();

            SwopblockClient = new SwopblockClient();

            servers = new SimulationAssetServers[serverCount];

            for (int i = 0; i < serverCount; i++)
            {
                servers[i] = new SimulationAssetServers();

                servers[i].BuildSimultion(contractCount, transferCount, proofCount);
            }

            Assert.Equal(serverCount, servers.Length);

            Assert.NotNull(SwopblockClient);

            return client;
        }
    }

    public sealed class SimulationAssetServers
    {
        public SimulationClientContracts[] contracts;

        [Theory]
        [InlineData(2, 3, 4)]
        public SimulationAssetServers BuildSimultion(int contractCount, int transferCount, int proofCount)
        {
            var server = new SimulationAssetServers();

            contracts = new SimulationClientContracts[contractCount];

            for (int i = 0; i < contractCount; i++)
            {
                contracts[i] = new SimulationClientContracts();

                contracts[i].BuildSimultion(transferCount, proofCount);
            }

            Assert.Equal(contractCount, contracts.Length);

            return server;
        }
    }

    public class SimulationClientContracts
    {
        public SimulationContractTransfers[] transfers;

        [Theory]
        [InlineData(2, 3)]
        public SimulationClientContracts BuildSimultion(int transferCount, int proofCount)
        {
            var contract = new SimulationClientContracts();

            transfers = new SimulationContractTransfers[transferCount];

            for (int i = 0; i < transferCount; i++)
            {
                transfers[i] = new SimulationContractTransfers();

                transfers[i].BuildSimultion(proofCount);
            }

            Assert.Equal(transferCount, transfers.Length);

            return contract;
        }
    }

    public class SimulationContractTransfers
    {
        public SimulationTransferProofs[] proofs;

        [Theory]
        [InlineData(2)]
        public SimulationContractTransfers BuildSimultion(int proofCount)
        {
            var transfer = new SimulationContractTransfers();

            proofs = new SimulationTransferProofs[proofCount];

            for (int i = 0; i < proofCount; i++)
            {
                proofs[i] = new SimulationTransferProofs();
            }

            Assert.Equal(proofCount, proofs.Length);

            return transfer;
        }
    }

    public class SimulationTransferProofs
    {
    }


}
