// Copywrite (c) 2022 Swopblock LLC   (see https://github.com/swopblock)
// Created November 29, 2022 3:51 PM ET by Jeff Hilde, jeff@swopblock.org

using Swopblock.API.Application;
using Swopblock.API.Data;
using Swopblock.API.Process;
using Swopblock.API.State;
using System.Diagnostics;

namespace Swopblock.API.Process
{
    public abstract class AbstractLayer
    {
        public static string Text(Transaction message)
        {
            return message.Text();
        }

        public static Transaction Parse(string message)
        {
            return Transaction.Parse(message);
        }

        public static Sourcing Source(string message)
        {
            return Sourcing.
        }

        public abstract string Buy(Buying message);

        public abstract Buying Buy(string message);

        //Bid, Ask, 

    }

    public class AutoFeedbackLayer
    {
        public Exchange Signed, Broadcast, Confirmed;

        public void Buy();
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
        public void Confirm(Transaction message)
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
            base.Confirm(Transaction.Parse(message));
        }
    }

    public class UserNetworkLayer : InterNetworkLayer
    {
        public void Sign(Transaction message)
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
