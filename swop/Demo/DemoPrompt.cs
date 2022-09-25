using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swopblock.Demo
{
    public class DemoPrompt
    {
        public static ContractStream Run()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("Run - Runs a simulation.");
            Console.WriteLine("Trade - Simulates a specific trade.");
            Console.WriteLine("Enter a command:");

            string line = Console.ReadLine();

            return new ContractStream(0, 0, 0, 0, 0);
        }

        public static ContractStream AddStates(ContractStream network, ContractStream contract)
        {
            return new ContractStream(
                0, /// new id
                network.ContractCashVolume + contract.ContractCashVolume,
                network.ContractCashInventory + contract.ContractCashInventory,
                network.ContractAssetVolume + contract.ContractAssetVolume,
                network.ContractAssetInventory + contract.ContractAssetInventory);
        }
    }
}
