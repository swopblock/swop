using Swopblock.API.Application;
using Swopblock.API.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationTesting.TestAPI
{
    internal class TestNetworkAPI
    {
    }

    public class TestINode : INode
    {
        public IApp APP
        {
            [Fact]
            get => throw new NotImplementedException();

            [Fact]
            set => throw new NotImplementedException();
        }

        public ICore CORE
        {
            [Fact]
            get => throw new NotImplementedException();

            [Fact]
            set => throw new NotImplementedException();
        }

        public ICarriers[] CARRIERS
        {
            [Fact]
            get => throw new NotImplementedException();

            [Fact]
            set => throw new NotImplementedException();
        }

        public INetworking Network
        {
            [Fact]
            get => throw new NotImplementedException();

            [Fact]
            set => throw new NotImplementedException();
        }

        [Fact]
        public void TestTestINode()
        {

            Assert.NotNull(this);
        }

        [Fact]
        void TestNew()
        {
            var sim = new TestINode();

            Assert.NotNull(sim);
        }

        [Fact]
        void TestNext()
        {
            var sim = new TestINode();

            sim.APP = APP;
        }




    }

}
