using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace Swopblock.Stack.NetworkLayer
{
    public class NetworkClient
    {
        public List<string> Peers { get; set; }

        public ConcurrentQueue<Packet> RecievedPackets { get; set; }

        private static readonly ushort port = 25233; //hotel room at money2020

        bool watching = true;

        bool logData = true;

        int resendAttempts = 10;

        UdpClient client = new UdpClient();

        public NetworkClient(List<string> peers)
        {
            Peers = peers;
            RecievedPackets = new ConcurrentQueue<Packet>();
        }

        public NetworkClient()
        {
            Peers = new List<string>();
            RecievedPackets = new ConcurrentQueue<Packet>();
        }

        public void Setup()
        {
            if (Peers != null)
            {
                if (Peers.Count > 0)
                {
                    foreach (string ip in Peers)
                    {
                        try
                        {
                            IPAddress address = IPAddress.Parse(ip);

                            client.Connect(new IPEndPoint(address, port));
                        }
                        catch (Exception ex)
                        {
                            if (logData) Debug.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        public void SendData(byte[] data, Packet.PacketType pkType)
        {
            int count = 0;

            int attempt = 0;

            while (count < data.Length)
            {
                byte[] rawPack = Packet.PackBytes(data, pkType);

                count = client.Send(rawPack, rawPack.Length);

                if (attempt++ > resendAttempts)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(333 * attempt);
                }
            }
        }

        public void StartRecieving()
        {
            new Thread(() =>
            {
                IPEndPoint point = new IPEndPoint(IPAddress.Any, port);

                UdpClient server = new UdpClient(point);

                Thread.CurrentThread.IsBackground = true;

                while (watching)
                {
                    byte[] data = server.Receive(ref point);

                    if (data != null)
                    {
                        if (data.Length > 0)
                        {
                            Packet pkt = Packet.GetPacket(data);

                            if (pkt != null)
                            {
                                RecievedPackets.Enqueue(pkt);

                                if (logData) Console.WriteLine(pkt.Readable);
                            }
                            else
                            {
                                if (logData) Console.WriteLine("[No Packet]");
                            }
                        }
                    }
                }
            }).Start();
        }

        public void ToggleLogging()
        {
            logData = !logData;
        }
    }
}
