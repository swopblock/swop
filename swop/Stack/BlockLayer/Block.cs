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

        public Block() { }
        public Block(BlockchainTag tag)
        {
            Tag = tag;
        }

        public virtual byte[] Serialize()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes((int)Tag));

            bytes.AddRange(BitConverter.GetBytes(transactions.Count));

            foreach (Transaction tx in transactions)
            {
                byte[] txBt = tx.Serialize();

                bytes.AddRange(BitConverter.GetBytes(txBt.Length));

                bytes.AddRange(txBt);
            }

            return bytes.ToArray();
        }
        public virtual Block Deserialize(byte[] data)
        {
            int minLen = 4;
            int index = 0;

            transactions.Clear();

            if (data != null)
            {
                if (data.Length >= minLen)
                {
                    int tag = BitConverter.ToInt32(data, index += 4);
                    int num = BitConverter.ToInt32(data, index += 4);

                    Block blk = new Block((BlockchainTag)tag);

                    int inx = 0;

                    Transaction tx = null;

                    while (inx++ < num)
                    {
                        tx = new Transaction();

                        int len = BitConverter.ToInt32(data, index += 4);

                        byte[] dat = Utility.GetNextByteSet(data, index, len);

                        tx.Deserialize(dat);

                        blk.transactions.Add(tx);
                    }
                }
            }

            return null;
        }
    }
}
