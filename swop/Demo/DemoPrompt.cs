using Swopblock.Intentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swopblock.Demo
{
    public class DemoPrompt
    {
        IntentionTree Tree = new IntentionTree();

        SimulationStates simulationStates = SimulationStates.Empty;
        public void Run()
        {
            Console.WriteLine();
            Console.Write("Intent: ");

            string intention = Console.ReadLine();

            if (intention.ToLower() != "exit")
            {
                if (Tree.Validate(intention))
                {
                    SimulationStates nState = SimulationStates.ParseFromIntention(intention);

                    simulationStates.Add(nState);

                    Console.WriteLine(simulationStates.ParseToTabbedLine());
                }
                else
                {
                    Console.WriteLine("Invalid Input");                    
                }

                Run();
            }
        }
    }
}
