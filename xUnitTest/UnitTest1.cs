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

        //[Fact]
        //public void TestStepState()
        //{
        //    var consensus = new ConsensusModule(null);

        //    var input = SimulationStates.FromTest();


        //    var expected = SimulationStates.FromRandom();

        //    SimulationStates actual;

        //    var stepState = consensus.StepState(input, out actual);


        //    Assert.Equal(expected, actual);
        //}

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

        [Fact]
        public void TestStepState()
        {
            var execution = new ExecutionModule(null);

            var input = SimulationStates.FromTest();


            var expected = SimulationStates.FromRandom();

            SimulationStates actual;

            var stepState = execution.StepState(input, out actual);


            Assert.Equal(expected, actual);
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

        [Fact]
        public void TestSimulationStateTabbedFormat()
        {
            string tabbedLine = "0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\t0\n";

            Assert.True(SimulationStates.CheckTabbedLineFormat(tabbedLine));
        }

        string[] asset = new[] { "SWOBL", "BTC", "ETH" };

        string[] transfer = new[] { "buy", "sell" };

        string[] contracting = new[] { "bidding", "asking" };

        [Theory]
        [InlineData(1, 150, 1, 10, 5000, 59999)]
        [InlineData(2, 260, 1, 20, 6000, 69999)]
        [InlineData(1, 370, 2, 30, 7000, 79999)]
        [InlineData(2, 480, 2, 40, 8000, 89999)]
        //I am [bidding] exactly [100] [SWOBL] of mine from my address [cid] in order to buy at least [1] [BTC] of yours from the market and my order is good until the market volume reaches [expirationVolume] SWOBL using my signature [transferId].
        public void TestSimulationStateFromIntention(int assetId, int contractId, int transferId, decimal valueOfMine, decimal valueOfYours, decimal expirationVolume)
        {
            var ofMine = $"exactly {valueOfMine} SWOBL of mine from my address {contractId}";

            var ofYours = $"at least {valueOfYours} {asset[assetId]} of yours from the market";

            var expiration = $"and my order is good until the market volume reaches {expirationVolume} SWOBL";

            var signature = $"using my signature {transferId}.";

            string contracting;

            string intention = null;

            decimal cashSupply = 0, cashDemand = 0, cashLock = expirationVolume, assetSupply = 0, assetDemand = 0;

            if (transferId == 1) //buy
            {
                contracting = $"bidding {ofMine} in order to buy {ofYours}";

                intention = $"I am {contracting} {expiration} {signature}";

                cashSupply = valueOfMine;

                cashDemand = 0;

                assetSupply = 0;
                // asset demand wasnt set so i fixed it
                assetDemand = valueOfYours;
            }
            else if (transferId == 2) //sell
            {
                contracting = $"asking {ofYours} in order to sell {ofMine}";

                intention = $"I am {contracting} {expiration} {signature}";

                cashSupply = valueOfMine;
                // cash demand wasnt set so i fixed it
                cashDemand = 0;

                assetSupply = 0;

                assetDemand = valueOfYours;
            }
            else
            {
                Assert.True(false);
            }

            var stateA = SimulationStates.ParseFromIntention(intention);

            var stateB = new SimulationStates();

            stateB.MainStreamState = MainStates.Empty;

            stateB.BranchStreamState = new BranchStates(assetId, 0, 0, 0, 0, 0);

            stateB.ContractStreamState = new ContractStates(contractId, cashSupply, cashDemand, cashLock, assetSupply, assetDemand);

            stateB.SignatureStreamTransfer = new TransferStates(transferId, cashSupply, cashDemand, cashLock, assetSupply, assetDemand);

            stateB.ConsensusState = ConsensusStates.Empty;

            Assert.True(stateA.MainStreamState.IsEqual(stateB.MainStreamState));
            Assert.True(stateA.BranchStreamState.IsEqual(stateB.BranchStreamState));
            Assert.True(stateA.ContractStreamState.IsEqual(stateB.ContractStreamState));
            Assert.True(stateA.SignatureStreamTransfer.IsEqual(stateB.SignatureStreamTransfer));

            Assert.True(stateA.IsEqual(stateB));

        }
    }
}