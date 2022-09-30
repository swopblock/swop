using Swopblock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTest
{
    public class TestSwopblockModule
    {
        [Fact] 
        void TestStepState()
        {
            var Swopblock = new SwopblockModule(null, null);

            var input = SimulationStates.FromTest();

            var output = SimulationStates.FromRandom();

            Swopblock.StepState(input, out output);

            Assert.Equal(output, input);
        }
    }
}
