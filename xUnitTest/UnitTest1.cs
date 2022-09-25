using Swopblock;

namespace xUnitTest
{
    public class UnitTestSimulationModule
    {
        [Fact]
        public void TestSimulationModule()
        {
            var simulationModule = new Simulation();

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
    }

    public class UnitTestConcensusModule
    {
        [Fact]
        public void TestSwopblockEntry()
        {
            var client = new SwopblockClient();

            client.PokeInEntryInput(LiquidityStreamStates.Empty);

            Assert.NotNull(client.PeekAtEntryOutput());
        }

        [Fact]
        public void TestSwopblockConcensus()
        {
            var client = new SwopblockClient();

            client.PokeInConsensusInput(LiquidityStreamStates.Empty);

            Assert.NotNull(client.PeekAtConsensusOutput());
        }
    }

    public class UnitTestExecutionModule
    {
        [Fact]
        public void TestSwopblockEntry()
        {
            var client = new SwopblockClient();

            client.PokeInExecutionInput(LiquidityStreamStates.Empty);

            Assert.NotNull(client.PeekAtExecutionOuput());
        }

        [Fact]
        public void TestSwopblockConcensus()
        {
            var client = new SwopblockClient();

            client.PokeInExitInput(LiquidityStreamStates.Empty);

            Assert.NotNull(client.PeekAtExitOutput());
        }
    }

}