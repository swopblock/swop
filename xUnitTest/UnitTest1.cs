using Swopblock;

namespace SimulationUnitTesting
{
    public class UnitTestSimulationModule
    {
        [Fact]
        public void TestSimulationModule()
        {
            var simulationModule = new SimulationModule();

            Assert.NotNull(simulationModule);
        }

        public void TestSwopblockEntrySimulation()
        {
            var clientSim = new SimulationSwopblockClients();

            Assert.NotNull(clientSim.Entry);
        }

        [Fact]
        public void TestSwopblockConsensusSimulation()
        {
            var clientSim = new SimulationSwopblockClients();

            Assert.NotNull(clientSim.Consensus);
        }

        [Fact]
        public void TestSwopblockExecutionSimulation()
        {
            var clientSim = new SimulationSwopblockClients();

            Assert.NotNull(clientSim.Execution);
        }

        [Fact]
        public void TestSwopblockExitSimulation()
        {
            var clientSim = new SimulationSwopblockClients();

            Assert.NotNull(clientSim.Exit);
        }

        [Fact]
        public void TestSimulationArgs()
        {
            var simulationArgs = new string[] { };

            var sim = new SimulationUnitTesting.SimulationModule(simulationArgs);

            Assert.Empty(simulationArgs);
        }
    }
}

namespace ConsensusUnitTesting
{
    public class UnitTestConcensusModule
    {
        [Fact]
        public void TestSwopblockEntry()
        {
            var client = new SwopblockModule();

            client.PokeInEntryInput(LiquidityStreamStates.Empty);

            Assert.NotNull(client.PeekAtEntryOutput());
        }

        [Fact]
        public void TestSwopblockConcensus()
        {
            var client = new SwopblockModule();

            client.PokeInConsensusInput(LiquidityStreamStates.Empty);

            Assert.NotNull(client.PeekAtConsensusOutput());
        }

        [Fact]
        public void TestConcensusArgs()
        {
            var consensusArgs = new string[] { };

            var sim = new ConsensusModule(consensusArgs);

            Assert.Empty(consensusArgs);
        }
    }
}

namespace ExecutionUnitTesting
{ 

    public class UnitTestExecutionModule
    {
        [Fact]
        public void TestSwopblockEntry()
        {
            var client = new SwopblockModule();

            client.PokeInExecutionInput(LiquidityStreamStates.Empty);

            Assert.NotNull(client.PeekAtExecutionOuput());
        }

        [Fact]
        public void TestSwopblockConcensus()
        {
            var client = new SwopblockModule();

            client.PokeInExitInput(LiquidityStreamStates.Empty);

            Assert.NotNull(client.PeekAtExitOutput());
        }

        [Fact]
        public void TestExecutionArgs()
        {
            var executionArgs = new string[] { };

            var sim = new ExecutionModule(executionArgs);

            Assert.Empty(executionArgs);
        }
    }
}

namespace ProgramUnitTesting
{
    public class UnitTestProgramModule
    {
        [Fact]
        public void TestGetModuleArgs()
        {
            var args = new string[] { "simulation", "1", "consensus", "1", "2", "execution", "1", "2", "3" };

            Program.GetModuleArgs(args);

            Assert.Equal(args[0], Program.simulationArgs[0]);

            Assert.Equal(args[1], Program.simulationArgs[1]);

            Assert.Equal(args[2], Program.consensusArgs[0]);

            Assert.Equal(args[3], Program.consensusArgs[1]);

            Assert.Equal(args[4], Program.consensusArgs[2]);

            Assert.Equal(args[5], Program.executionArgs[0]);

            Assert.Equal(args[6], Program.executionArgs[1]);

            Assert.Equal(args[7], Program.executionArgs[2]);
        }
    }
}