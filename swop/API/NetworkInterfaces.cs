// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using Swopblock.API.Swopping;
using Swopblock.API.Application;

namespace Swopblock.API.Network
{
    public interface INode : INetwork
    {
        public IApp APP { get; set; }

        public ICore CORE { get; set; }

        public ICarriers[] CARRIERS { get; set; }

        public INetworking Network { get; set; }
    }

    public interface INetworking : INetwork
    {
        public IConsenting Consensus { get; set; }

        public IExecuting Execution { get; set; }

        public ISettling Settlement { get; set; }

        public INode[] Nodes { get; set; }
    }

    public interface INetwork { }
}
