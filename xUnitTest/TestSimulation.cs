﻿using Swopblock.Simulation;
using Swopblock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationUnitTesting
{
    public class TestSimulation : SimulationModule
    {
        public TestSimulation() : base(null, null, null) { }

        public TestSimulation(string[] sim, string[] con, string[] exe) : base(sim, con, exe)
        {

        }

        public override SimulationStates PeekAtConsensusOutput()
        {
            return base.PeekAtConsensusOutput();
        }

        public override SimulationStates PeekAtEntryOutput()
        {
            return base.PeekAtEntryOutput();
        }

        public override SimulationStates PeekAtExecutionOuput()
        {
            return base.PeekAtExecutionOuput();
        }

        public override SimulationStates PeekAtExitOutput()
        {
            return base.PeekAtExitOutput();
        }

        public override void PokeInConsensusInput(SimulationStates state)
        {
            base.PokeInConsensusInput(state);
        }

        public override void PokeInEntryInput(SimulationStates state)
        {
            base.PokeInEntryInput(state);
        }

        public override void PokeInExecutionInput(SimulationStates state)
        {
            base.PokeInExecutionInput(state);
        }

        public override void PokeInExitInput(SimulationStates state)
        {
            base.PokeInExitInput(state);
        }

        //[Theory]
        //[InlineData(2, 3, 4, 5, 6, 7)]
        //[InlineData(7, 6, 5, 4, 3, 2)]
        //public override void BuildSimultion(int networkCount, int clientCount, int serverCount, int contractCount, int transferCount, int proofCount)
        //{
        //    base.BuildSimultion(networkCount, clientCount, serverCount, contractCount, transferCount, proofCount);

        //    Assert.Equal(networkCount, networks.Length);
        //}
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
