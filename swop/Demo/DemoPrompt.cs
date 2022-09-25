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
        IntentionTree Tree = DemoWeb.GetTree();

        SwopblockClient client = new SwopblockClient();
        public void Run()
        {
            LiquidityStreams state;

            Console.WriteLine("Commands:");
            Console.WriteLine("Run - Runs a simulation.");
            Console.WriteLine("Trade - Simulates a specific trade.");
            Console.WriteLine("Enter a command:");

            string command = Console.ReadLine();

            if (false) //(client.CaptureIntention(command))
            {
                Console.WriteLine("Congratz! Your request has been accepted by the Swopblock Network!");
                    
                //state = client.ConsensusProcessor.GetNetworkState();

                //Console.WriteLine("Cash Volume: " + state.Contract.ContractCashVolume);
                //Console.WriteLine("Asset Volume: " + state.Contract.ContractAssetVolume);
            }           
        }
    }
}
