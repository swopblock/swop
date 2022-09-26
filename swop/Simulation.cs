using Swopblock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//JJH

namespace SimulationUnitTesting
{
    public class SimulationModule : SwopblockModule
    {
        public SimulationModule(params string[] args)
        {

        }

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
                        //for (int i = 0; i < ContractBatchCount; i++)
                        //{
                        //    client.Exit.PeekAtExitOutput();
                        //}
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
                            //for (int i = 0; i < ContractBatchCount; i++)
                            //{
                            //    //
                            //}
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
            }

        }
    }

    public class SimulationSwopblockClients
    {
        public SwopblockModule Entry, Consensus, Execution, Exit;

        public SimulationAssetServers[] servers;

        public SimulationSwopblockClients()
        {
            Entry = new SwopblockModule();

            Consensus = new SwopblockModule();

            Execution = new SwopblockModule();

            Exit = new SwopblockModule();
        }

        public virtual void BuildSimultion(int serverCount, int contractCount, int transferCount, int proofCount)
        {
            var SwopblockClient = new SwopblockModule();

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
