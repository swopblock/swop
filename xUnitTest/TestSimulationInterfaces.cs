using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Swopblock.Application;
using Swopblock.Liquidity;
using Swopblock.Simulation;
using Swopblock.Swopping;

namespace xUnitTest.TestSimulation
{
    public class TestAPP : APP
    {
        decimal test;

        public decimal CashBalance 
        {
            [Fact]
            get
            {
                test = 0;

                CashBalance = 123456789M;

                Assert.Equal(123456789M, test);

                return test;
            }

            set
            {
                test = value;
            }
        }

        public decimal CashVolume
        {
            [Fact]
            get
            {
                test = 0;

                CashBalance = 123456789M;

                Assert.Equal(123456789M, test);

                return test;
            }

            set
            {
                test = value;
            }
        }

        IAccount[] testAcounts;

        public IAccount[] Accounts
        {
            [Fact]
            get
            {
                Accounts = (IAccount[])null;

                Assert.Null(testAcounts);

                return (IAccount[])null;
            }

            set
            {
                testAcounts = (IAccount[])value;
            }
        }

        public IReport SubmitIntention(IOrder Order)
        {
            throw new NotImplementedException();
        }
    }

    public class TestINode : INode
    {
        public APP APP 
        { 
            [Fact] 
            get => throw new NotImplementedException();

            [Fact]
            set => throw new NotImplementedException(); 
        }

        public CORE CORE 
        {
            [Fact]
            get => throw new NotImplementedException();

            [Fact]
            set => throw new NotImplementedException(); 
        }

        public CARRIERS[] CARRIERS 
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
