using Swopblock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//JJH

namespace SimulationUnitTesting
{
    public class SimulationModule
    {
        public static string[] simulationArgs; public static string[] consensusArgs; public static string[] executionArgs;

        public SimulationModule(string[] simulationArgs, string[] consensusArgs, string[] executionArgs)
        {
            SimulationModule.simulationArgs = simulationArgs;
            SimulationModule.consensusArgs = consensusArgs;
            SimulationModule.executionArgs = executionArgs;
        }

        public virtual SimulationStates PeekAtEntryOutput()
        {
            return null;
        }

        public virtual SimulationStates PeekAtConsensusOutput()
        {
            return null;
        }

        public virtual SimulationStates PeekAtExecutionOuput()
        {
            return null;
        }

        public virtual SimulationStates PeekAtExitOutput()
        {
            return null;
        }

        public virtual void PokeInEntryInput(SimulationStates State)
        {
            foreach(var network in networks)
            {
                foreach(var client in network.clients)
                {
                    foreach(var server in client.servers)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            client.SimulationSwopblockClient.PokeInEntryInput(State);
                            //Make and Poke new random contract state
                        }
                    }
                }
            }
        }

        public virtual void PokeInConsensusInput(SimulationStates State)
        {
        }

        public virtual void PokeInExecutionInput(SimulationStates State)
        {
        }

        public virtual void PokeInExitInput(SimulationStates State)
        {
        }

        public SimulationSystemNetworks[] networks;

        public virtual void BuildSimultion(int networkCount, int clientCount, int serverCount, int contractCount, int transferCount, int proofCount)
        {
            networks = new SimulationSystemNetworks[networkCount];

            for (int i = 0; i < networkCount; i++)
            {
                networks[i] = new SimulationSystemNetworks();

                networks[i].BuildSimultion(clientCount, serverCount, contractCount, transferCount, proofCount);
            }

        }
    }

    public class SimulationSystemNetworks
    {
        public SimulationSwopblockClients[] clients;

        public virtual void BuildSimultion(int clientCount, int serverCount, int contractCount, int transferCount, int proofCount)
        {
            clients = new SimulationSwopblockClients[clientCount];

            for (int i = 0; i < clientCount; i++)
            {
                clients[i] = new SimulationSwopblockClients();

                clients[i].BuildSimultion(serverCount, contractCount, transferCount, proofCount);

                //clients[i].SimulationSwopblockClient.PokeInEntryInput();
            }

        }
    }

    public class SimulationSwopblockClients
    {     
        public SwopblockModule SimulationSwopblockClient;

        public SimulationSwopblockClients()
        {
            SimulationSwopblockClient = new SwopblockModule(SimulationModule.consensusArgs, SimulationModule.executionArgs);
        }


        public SimulationAssetServers[] servers;

        public virtual void BuildSimultion(int serverCount, int contractCount, int transferCount, int proofCount)
        {
            servers = new SimulationAssetServers[serverCount];

            for (int i = 0; i < serverCount; i++)
            {
                servers[i] = new SimulationAssetServers();

                servers[i].BuildSimultion(contractCount, transferCount, proofCount);
            }
        }
    }

    public class SimulationAssetServers
    {
        public SimulationClientContracts[] contracts;

        public virtual void BuildSimultion(int contractCount, int transferCount, int proofCount)
        {
            contracts = new SimulationClientContracts[contractCount];

            for (int i = 0; i < contractCount; i++)
            {
                contracts[i] = new SimulationClientContracts();

                contracts[i].BuildSimultion(transferCount, proofCount);
            }

        }
    }

    public class SimulationClientContracts
    {
        public SimulationContractTransfers[] transfers;

        public virtual void BuildSimultion(int transferCount, int proofCount)
        {
            transfers = new SimulationContractTransfers[transferCount];

            for (int i = 0; i < transferCount; i++)
            {
                transfers[i] = new SimulationContractTransfers();

                transfers[i].BuildSimultion(proofCount);
            }

        }
    }

    public class SimulationContractTransfers
    {
        public SimulationTransferProofs[] proofs;

        public virtual void BuildSimultion(int proofCount)
        {
            proofs = new SimulationTransferProofs[proofCount];

            for (int i = 0; i < proofCount; i++)
            {
                proofs[i] = new SimulationTransferProofs();
            }

        }
    }

    public class SimulationTransferProofs
    {
    }


}
