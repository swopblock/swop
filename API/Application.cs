// Copywrite (c) 2022 Swopblock LLC   (see https://github.com/swopblock)
// Created December 29, 2022 4:53 PM ET by Jeff Hilde, jeff@swopblock.org

using Swopblock.API.Data;
using Swopblock.API.Process;
using Swopblock.API.State;

namespace Swopblock.API.Application
{
    public class APP : UserApplicationLayer
    {
        public CORE CORE;
    }

    public class CORE : UserIncentiveLayer
    {
        public APP APP;

        public CARRIER[] CARRIER;

    }

    public class CARRIER : UserConsensusLayer
    {
        public CORE CORE;
    }
}
