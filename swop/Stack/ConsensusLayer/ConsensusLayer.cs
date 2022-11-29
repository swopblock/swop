// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

namespace Swopblock.Stack.ConsensusLayer
{
    public class ConsensusModule
    {
        public ConsensusModule(params string[] args)
        {

        }

        public SimulationStates Run(SimulationStates state)
        {
            return state;
        }

        public int StepState(SimulationStates input, out SimulationStates output)
        {
            output = input;

            return 0;
        }
    }
}
