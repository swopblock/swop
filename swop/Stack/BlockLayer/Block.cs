using Swopblock.Intentions.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swopblock.Stack.BlockLayer
{
    public class Block
    {
        public enum BlockchainTag { None, BTC, ETH }

        BlockchainTag Tag = BlockchainTag.None;

        List<Transaction> transactions = new List<Transaction>();   

        public Block(BlockchainTag tag)
        {
            Tag = tag;
        }

        public virtual byte[] Serialize()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes((int)Tag));

            bytes.AddRange(BitConverter.GetBytes(transactions.Count));

            foreach(Transaction tx in transactions)
            {
                byte[] txBt = tx.Serialize();

                bytes.AddRange(BitConverter.GetBytes(txBt.Length));

                bytes.AddRange(txBt);
            }

            return bytes.ToArray();
        }
    }
}
