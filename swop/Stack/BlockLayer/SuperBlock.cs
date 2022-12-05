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
        public virtual byte[] Serialize()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(Blocks.Count));

            foreach (Block blk in Blocks)
            {
                byte[] txBt = blk.Serialize();

                bytes.AddRange(BitConverter.GetBytes(txBt.Length));

                bytes.AddRange(txBt);
            }

            return bytes.ToArray();
        }
    }
}
