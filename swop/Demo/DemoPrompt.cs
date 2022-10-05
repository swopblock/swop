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

            intention = intention.ToLower();

            if (intention != "exit")
            {
                if (Tree.Validate(intention))
                {
                    SimulationStates nState = SimulationStates.ParseFromIntention(intention);

                    if (simulationStates.ConsensusState.MarketCashVolume < nState.ContractState.CashLock)
                    {
                        simulationStates = simulationStates.Add(nState);

                        Console.WriteLine(simulationStates.ParseToTabbedLine());
                    }
                    else
                    {
                        Console.WriteLine("\nTx Expired After Volume: " + nState.ContractState.CashLock);
                    }
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
