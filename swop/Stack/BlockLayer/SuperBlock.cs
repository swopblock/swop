using Swopblock.Intentions.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Swopblock.Stack.BlockLayer
{
    public class SuperBlock
    {
        public List<Block> Blocks = new List<Block>();

        public int Version = 0;

        public void AddBlock(Block blk)
        {
            Blocks.Add(blk);
        }

        public void AddTx(Transaction tx, Block.BlockchainTag chain)
        {
            Block selected = Blocks.Where(x => x.GetTag() == chain).FirstOrDefault();

            if (selected != null)
            {
                selected.AddTx(tx);
            }
            else
            {
                Blocks.Add(new Block(chain));

                AddTx(tx, chain);
            }
        }
        public virtual byte[] Serialize()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(Version));

            bytes.AddRange(BitConverter.GetBytes(Blocks.Count));

            foreach (Block blk in Blocks)
            {
                byte[] txBt = blk.Serialize();

                bytes.AddRange(BitConverter.GetBytes(txBt.Length));

                bytes.AddRange(txBt);
            }

            return bytes.ToArray();
        }

        public virtual SuperBlock Deserialize(byte[] data)
        {
            int minlen = 8;

            if (data != null)
            {
                if (data.Length > minlen)
                {
                    SuperBlock block = new SuperBlock();

                    int index = 0;

                    int version = BitConverter.ToInt32(data, index);

                    index += 4;

                    int count = BitConverter.ToInt32(data, index);

                    index += 4;

                    block.Version = version;

                    for (int x = 0; x < count; x++)
                    {
                        int len = BitConverter.ToInt32(data, index);

                        index += 4;

                        byte[] rawBytes = Utility.GetNextByteSet(data, index, len);

                        index += len;

                        Block blk = new Block();

                        blk.Deserialize(rawBytes);

                        block.Blocks.Add(blk);
                    }

                    return block;
                }
            }

            return null;
        }
    }
}
