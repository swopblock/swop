using Swopblock.Intentions.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swopblock.Stack.BlockLayer
{
    public class Transaction
    {
        public List<DataTag> ValueData { get; set; }

        public virtual byte[] Serialize()
        {
            List<byte> bytes = new List<byte>();

            foreach(DataTag tag in ValueData)
            {
                bytes.AddRange(BitConverter.GetBytes(tag.Name.Length));
                bytes.AddRange(Utility.ConvertToBytes(tag.Name));
                bytes.AddRange(BitConverter.GetBytes(tag.Data.Length));
                bytes.AddRange(tag.Data);
            }

            return bytes.ToArray();
        }
    }
}
