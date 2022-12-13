using Swopblock.Stack.NetworkLayer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swopblock.Stack.BlockLayer
{
    public class DataManager
    {
        public ConcurrentQueue<SuperBlock> SuperBlocks { get; set; }

        public NetworkClient client = new NetworkClient();

        private List<SuperBlock> unsavedBlocks { get; set; }

        private bool running = true;

        public DataManager()
        {
            client.Peers = Settings.NetworkPeers;
            SuperBlocks = new ConcurrentQueue<SuperBlock>();
            unsavedBlocks = new List<SuperBlock>();
        }

        public void RunNetwork()
        {
            new Thread(() =>
            {
                client.Setup();
                client.StartRecieving();

                while (running)
                {
                    List<Packet> pks = RecieveData(client.RecievedPackets);

                    if (pks != null)
                    {
                        if (pks.Count > 0)
                        {
                            List<SuperBlock> blocks = ProcessData(pks);

                            if (blocks != null)
                            {
                                if (blocks.Count > 0)
                                {
                                    unsavedBlocks.AddRange(blocks);

                                    Save();
                                }
                            }
                        }
                    }

                    Thread.Sleep(10);
                }
            }).Start();
        }

        public List<Packet> RecieveData(ConcurrentQueue<Packet> packets)
        {
            List<Packet> pk = new List<Packet>();

            while (!packets.IsEmpty)
            {
                Packet qpk = null;

                if (packets.TryDequeue(out qpk))
                {
                    pk.Add(qpk);
                }
            }

            return pk;
        }

        public List<SuperBlock> ProcessData(List<Packet> packets)
        {
            List<SuperBlock> sblocks = new List<SuperBlock>();

            foreach (Packet packet in packets)
            {
                SuperBlock bk = new SuperBlock();
                bk.Deserialize(packet.GetMessageData());
                sblocks.Add(bk);
            }

            return sblocks;
        }

        public Transaction GetTxByAddress(string address)
        {
            return null;
        }

        public Transaction GetTxById(string id)
        {
            return null;
        }

        public void Save()
        {
            foreach (SuperBlock blk in unsavedBlocks)
            {
                byte[] bt = blk.Serialize();

                File.WriteAllBytes(Settings.SuperBlockFolder
                    + "\\SuperBlock:"
                    + Settings.SuperBlockCount++
                    + ".blk", bt);
            }

            unsavedBlocks.Clear();
        }
    }
}
