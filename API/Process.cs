// Copywrite (c) 2022 Swopblock LLC   (see https://github.com/swopblock)
// Created November 29, 2022 3:51 PM ET by Jeff Hilde, jeff@swopblock.org

using Swopblock.API.Application;
using Swopblock.API.Data;
using Swopblock.API.Process;
using Swopblock.API.State;
using System.Diagnostics;

namespace Swopblock.API.Process
{
     public class AutoFeedbackLayer
     {
        public Main Signed, Broadcast, Confirmed;
     }

    public class AutoApplicationLayer : AutoFeedbackLayer
    {
    }

    public class AutoIncentiveLayer : AutoApplicationLayer
    {
    }

    public class AutoConsensusLayer : AutoIncentiveLayer
    {
    }

    public class AutoNetworkLayer : AutoConsensusLayer
    {
        public void Confirm(Message message)
        {

        }
    }

    public class InterNetworkLayer : AutoNetworkLayer
    {
        public static InterNetworkLayer[] Nodes;

        public static Random rand;

        static InterNetworkLayer()
        {
            rand = new Random();

            Nodes = new InterNetworkLayer[rand.Next(1, 1000)];
        }

        public void Broadcast(string message)
        {
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[rand.Next(Nodes.Length)].Receive(message);
            }
        }


        public void Receive(string message)
        {
            base.Confirm(Message.Parse(message));
        }
    }

    public class UserNetworkLayer : InterNetworkLayer
    {
        public void Sign(Message message)
        {
            base.Broadcast(message.Text());

            base.Confirm(message);
        }
    }

    public class UserConsensusLayer : UserNetworkLayer
    {
    }

    public class UserIncentiveLayer : UserConsensusLayer
    {
    }

    public class UserApplicationLayer : UserIncentiveLayer
    {
    }

    public class UserControlLayer : UserApplicationLayer
    {
    }
 }
