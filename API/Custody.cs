// Copywrite (c) 2022 Swopblock LLC   (see https://github.com/swopblock)
// Created December 29, 2022 4:53 PM ET by Jeff Hilde, jeff@swopblock.org

using Swopblock.API.Data;
using Swopblock.API.Process;
using Swopblock.API.State;

namespace Swopblock.API.Custody
{
    public abstract class APP : UserControlLayer
    {
        public User Pending, Confirming;

        public CORE CORE { get; init; }
    }

    public abstract class CORE : UserIncentiveLayer
    {
        public User Pending, Confirming;

        public APP APP { get; init; }

        public CARRIER[] CARRIER { get; init; }

        public override Message Call(Message message)
        {
            CARRIER[0].Call(message);

            return base.Call(message);
        }
    }

    public abstract class CARRIER : UserConsensusLayer
    {
        public User Pending, Confirming;

        public CORE CORE { get; init; }
    }
}
