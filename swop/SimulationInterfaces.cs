// Copywrite (c) 2022 Swopblock LLC
// See https://github.com/swopblock

using Swopblock.Swopping;
using Swopblock.Application;

namespace Swopblock.Simulation
{
    public interface INode : ISimulation
    {
        public APP APP { get; set; }

        public CORE CORE { get; set; }

        public CARRIERS[] CARRIERS { get; set; }

        public INetworking Network { get; set; }
    }

    public interface INetworking : ISimulation
    {
        public IConsenting Consensus { get; set; }

        public IExecuting Execution { get; set; }

        public ISettling Settlement { get; set; }

        public INode[] Nodes { get; set; }
    }

    public interface ISimulation { }
}
