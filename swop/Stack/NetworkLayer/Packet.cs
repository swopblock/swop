using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swopblock.Intentions.Utilities;

namespace Swopblock.Stack.NetworkLayer
{
    public class Packet
    {
        public enum PacketType
        {
            None,
            GetPeerList,
            GetBestBlock,
            GetMerkleTree,
            SendPeerList,
            SendBestBlock,
            SendMerkleTree
        }

        public PacketType packetType = PacketType.None;

        private static string head = "[SwopBlock]";

        private string Heading { get; set; }
        private byte[] rawData { get; set; }
        private byte[] data { get; set; }

        private byte[] renderedData { get; set; }

        public string Readable
        {
            get
            {
                return Heading
                    + " "
                    + Length
                    + " "
                    + Utility.ConvertToReadable(data)
                    + " "
                    + Checksum;
            }
            private set { }
        }

        public int Length = 0;
        public int Checksum = 0;

        public Packet(byte[] data, PacketType pkType = PacketType.None)
        {
            renderedData = PackBytes(data, pkType);
        }

        public Packet()
        {

        }

        public static byte[] PackBytes(byte[] data, PacketType pkType)
        {
            List<byte> bytes = new List<byte>();

            for (int i = 0; i < head.Length; i++)
            {
                bytes.Add((byte)head[i]);
            }

            bytes.AddRange(BitConverter.GetBytes((int)pkType));

            bytes.AddRange(BitConverter.GetBytes(data.Length));
            bytes.AddRange(data);
            bytes.AddRange(BitConverter.GetBytes(checksum(data)));

            return bytes.ToArray();
        }

        public static Packet GetPacket(byte[] data)
        {
            Packet packet = null;

            if (data.Length > 8)
            {
                string header = Utility.ConvertToString(Utility.GetNextByteSet(data, 0, head.Length));

                if (header == head)
                {
                    int pty = BitConverter.ToInt32(data, head.Length);

                    int len = BitConverter.ToInt32(data, head.Length + 4);
                    byte[] bytes = Utility.GetNextByteSet(data, 8 + head.Length, len);
                    int check = BitConverter.ToInt32(data, len + 8 + head.Length);

                    if (check == checksum(bytes))
                    {
                        packet = new Packet
                        {
                            Heading = header,
                            packetType = (PacketType)pty,
                            Length = len,
                            rawData = data,
                            Checksum = check,
                            data = bytes
                        };
                    }
                }
            }

            return packet;
        }

        public byte[] GetDataForSending()
        {
            if (renderedData != null)
            {
                if (renderedData.Length > 0)
                {
                    return renderedData;
                }
            }

            return null;
        }

        public byte[] GetMessageData()
        {
            return data;
        }

        private static int checksum(byte[] data)
        {
            double value = 0;

            for (int i = 0; i < data.Length; i++)
            {
                value += data[i];
                value *= 53.97;
                value %= int.MaxValue;
            }

            return (int)value;
        }
    }
}
