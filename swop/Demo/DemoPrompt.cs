using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swop.Demo
{
    public class DemoPrompt
    {
        public static ContractState Run()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("Run - Runs a simulation.");
            Console.WriteLine("Trade - Simulates a specific trade.");
            Console.WriteLine("Enter a command:");

            string line = Console.ReadLine();

            return new ContractState(0, 0, 0, 0, 0);
        }

        public static ContractState AddStates(ContractState network, ContractState contract)
        {
            return new ContractState(
                0, /// new id
                network.ContractCashVolume + contract.ContractCashVolume,
                network.ContractCashInventory + contract.ContractCashInventory,
                network.ContractAssetVolume + contract.ContractAssetVolume,
                network.ContractAssetInventory + contract.ContractAssetInventory);
        }
    }
}
