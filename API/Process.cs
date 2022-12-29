// Copywrite (c) 2022 Swopblock LLC   (see https://github.com/swopblock)
// Created November 29, 2022 3:51 PM ET by Jeff Hilde, jeff@swopblock.org

using Swopblock.API.Custody;
using Swopblock.API.Data;
using Swopblock.API.Process;
using System.Diagnostics;

namespace Swopblock.API.Process
{
     public class AutoFeedbackLayer
    {
        public virtual Message Call(Message message)
        {
            return message;
        }

        public virtual Message Return(Message message)
        {
            return message;
        }
    }

    public class AutoApplicationLayer : AutoFeedbackLayer
    {
    }

    public class AutoIncentiveLayer : AutoApplicationLayer
    {
    }

    public class AutoConsensusLayer : AutoIncentiveLayer
    {
        public void Bid(Bidding message) { }

        public void Ask(Asking message) { }
    }

    public class AutoNetworkLayer : AutoConsensusLayer
    {
        static UInt16 n = 0;

        public static AutoNetworkLayer[] AutoNeighborhood = new AutoNetworkLayer[n];

        public AutoNetworkLayer()
        {
            AutoNeighborhood[n++] = this;
        }
    }

    public class InterNetworkLayer : AutoNetworkLayer
    {
        public static InterNetworkLayer[] Nodes; 
    }

    public class UserNetworkLayer : InterNetworkLayer
    {
        static UInt16 n = 0;

        public static UserNetworkLayer[] UserNeighborhood = new UserNetworkLayer[n];

        public UserNetworkLayer()
        {
            UserNeighborhood[n++] = this;
        }

        public override Message Call(Message message)
        {
            foreach (var neighborhood in UserNeighborhood)
            {
                Call(message);
            }

            return base.Call(message);
        }

        public override Message Return(Message message)
        {
            return base.Return(message);
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
