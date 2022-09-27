using Swopblock;

namespace SimulationUnitTesting
{
    public class UnitTestSimulationModule
    {
        [Fact]
        public void TestSimulationModule()
        {
            var simulationModule = new SimulationModule(new string[] { "simulation", "one"}, new string[] { "consensus", "two"}, new string[] { "execution", "three"});

            Assert.NotNull(simulationModule);

            Assert.Equal("simulation", SimulationModule.simulationArgs[0]);
            Assert.Equal("one", SimulationModule.simulationArgs[1]);

            Assert.Equal("consensus", SimulationModule.consensusArgs[0]);
            Assert.Equal("two", SimulationModule.consensusArgs[1]);

            Assert.Equal("execution", SimulationModule.executionArgs[0]);
            Assert.Equal("three", SimulationModule.executionArgs[1]);
        }

        [Fact]
        public void TestSimulationArgs()
        {
            var sim = new SimulationModule(new string[] { }, new string[] { }, new string[] { });

            Assert.NotNull(sim);
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
            var client = new SwopblockModule(null, null);

            client.PokeInEntryInput(SimulationStates.Empty);

            Assert.NotNull(client.PeekAtEntryOutput());
        }

        [Fact]
        public void TestSwopblockConcensus()
        {
            var client = new SwopblockModule(null, null);

            client.PokeInConsensusInput(SimulationStates.Empty);

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
            var client = new SwopblockModule(null, null);

            client.PokeInExecutionInput(SimulationStates.Empty);

            Assert.NotNull(client.PeekAtExecutionOuput());
        }

        [Fact]
        public void TestSwopblockConcensus()
        {
            var client = new SwopblockModule(null, null);

            client.PokeInExitInput(SimulationStates.Empty);

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

        [Fact]
        public void TestSimulationParsing()
        {
            var testA = SimulationStates.FromTest();

            var line = testA.ParseToTabbedLine();

            var testB = SimulationStates.ParseFromTabbedLine(line);

            Assert.True(testA.IsEqual(testB));
        }


    }
}