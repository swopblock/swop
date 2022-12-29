using Swopblock.Intentions;
using Swopblock.Stack.BlockLayer;
using Swopblock.Stack.NetworkLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Swopblock.Demo
{
    public class DemoPrompt
    {
        IntentionTree Tree = new IntentionTree();
        DataManager manager = new DataManager();

        SimulationStates simulationStates = SimulationStates.Empty;

        public void Start()
        {
            Console.WriteLine("Hello, Swopblock!");
            Console.WriteLine();

            Console.Write("Connect To: ");
            string value = Console.ReadLine();

            try
            {
                manager.AddPeer(IPAddress.Parse(value));
                manager.StartNetwork();
            }
            catch
            {
                Console.WriteLine("Incorrect Format");
            }

            Run();
        }
        public void Run()
        {
            Console.Write("Intent: ");

            string intention = Console.ReadLine();

            intention = intention.ToLower();

            if (intention != "exit")
            {
                if (Tree.Validate(intention))
                {
                    SimulationStates nState = SimulationStates.ParseFromIntention(intention);

                    manager.AddTx(new Transaction(intention), Block.BlockchainTag.BTC);

                    manager.SendPacket(manager.Serialize(), Packet.PacketType.SendBestBlock);

                    //if (simulationStates.ConsensusState.MarketCashVolume < nState.AddressState.CashLock)
                    //{
                    //    simulationStates = simulationStates.Add(nState);

                    //    Console.WriteLine(simulationStates.ParseToTabbedLine());
                    //}
                    //else
                    //{
                    //    Console.WriteLine("\nTx Expired After Volume: " + nState.AddressState.CashLock);
                    //}
                }
                else
                {
                    Console.WriteLine("Intentions are invalid!");
                    Console.WriteLine();
                }

                Run();
            }
        }
    }
}
