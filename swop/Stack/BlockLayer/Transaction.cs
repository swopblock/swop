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

        public Transaction()
        {
            ValueData = new List<DataTag>();
        }

        public virtual byte[] Serialize()
        {
            List<byte> bytes = new List<byte>();

            if (ValueData != null)
            {
                foreach (DataTag tag in ValueData)
                {
                    //tag name length 4 bytes
                    bytes.AddRange(BitConverter.GetBytes(tag.Name.Length));
                    //tag name content
                    bytes.AddRange(Utility.ConvertToBytes(tag.Name));
                    //data length 4 bytes
                    bytes.AddRange(BitConverter.GetBytes(tag.Data.Length));
                    //data content
                    bytes.AddRange(tag.Data);
                }
            }

            return bytes.ToArray();
        }

        public virtual void Deserialize(byte[] data)
        {
            int minlen = 8;

            ValueData = new List<DataTag>();

            if (data != null)
            {
                if (data.Length >= minlen) // minlen = 8 because thats the minimum for an empty data tag
                {
                    int index = 0;

                    while (index < data.Length - minlen)
                    {
                        int nameLen = BitConverter.ToInt32(data, index += 4);
                        byte[] namebyt = Utility.GetNextByteSet(data, index, nameLen);
                        string name = Utility.ConvertToString(namebyt);

                        index += nameLen;

                        int rawLen = BitConverter.ToInt32(data, index += 4);
                        byte[] raw = Utility.GetNextByteSet(data, index, rawLen);

                        index += rawLen;

                        ValueData.Add(new DataTag
                        {
                            Data = raw,
                            Name = name
                        });
                    }
                }
            }
        }
    }
}
