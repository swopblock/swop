using Swopblock.Intentions.Utilities;
using Swopblock.Stack.NetworkLayer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Swopblock.Stack.BlockLayer
{
    public class DataManager
    {
        public ConcurrentQueue<SuperBlock> SuperBlocks { get; set; }

        public NetworkClient client = new NetworkClient();

        public SuperBlock CurrentBlock = new SuperBlock();

        private List<SuperBlock> unsavedBlocks { get; set; }

        private List<string> logs { get; set; }

        private bool running = true;

        public DataManager()
        {
            SuperBlocks = new ConcurrentQueue<SuperBlock>();
            unsavedBlocks = new List<SuperBlock>();
            logs = new List<string>();

            client.ToggleLogging();
        }

        public void StartNetwork()
        {
            new Thread(() =>
            {
                //Thread.CurrentThread.IsBackground = true;

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

        public void AddPeer(IPAddress address)
        {
            if (Settings.NetworkPeers == null) Settings.NetworkPeers = new List<string>();
            
            Settings.NetworkPeers.Add(address.ToString());
        }

        public void AddBlock(Block blk)
        {
            CurrentBlock.AddBlock(blk);
        }

        public void AddTx(Transaction tx, Block.BlockchainTag chain)
        {
            CurrentBlock.AddTx(tx, chain);
        }

        public byte[] Serialize()
        {
            return CurrentBlock.Serialize();
        }

        public void SendPacket(byte[] data, Packet.PacketType pkType)
        {
            client.SendData(data, pkType);
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

        public List<SuperBlock> ProcessData(List<Packet> messages)
        {
            List<SuperBlock> sblocks = new List<SuperBlock>();

            foreach (Packet ms in messages)
            {
                SuperBlock bk = new SuperBlock();
                sblocks.Add(bk.Deserialize(ms.GetMessageData()));
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
                if (blk != null)
                {
                    byte[] bt = blk.Serialize();

                    foreach (Block block in blk.Blocks)
                    {
                        foreach (Transaction tx in block.transactions)
                        {
                            foreach (DataTag dat in tx.ValueData)
                            {
                                Console.WriteLine(dat.Name + " : " + Utility.ConvertToReadable(dat.Data));
                            }
                        }
                    }
                }

                //File.WriteAllBytes(Settings.SuperBlockFolder 
                //    + "\\SuperBlock:" 
                //    + Settings.SuperBlockCount++ 
                //    + ".blk", bt);
            }

            unsavedBlocks.Clear();
        }
    }
}
