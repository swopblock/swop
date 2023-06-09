﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Swopblock.API.Application;
using Swopblock.API.Liquidity;
using Swopblock.API.Network;
using Swopblock.API.Swopping;

namespace SimulationTesting.TestAPI
{
    public class TestAPP : IApp
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
                Accounts = null;

                Assert.Null(testAcounts);

                return null;
            }

            set
            {
                testAcounts = value;
            }
        }

        public IReport SubmitIntention(IOrder Order)
        {
            throw new NotImplementedException();
        }
    }

    public class TestCORE : ICore
    {
        public IMain Main { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IReport SubmitOrder(IOrder Order)
        {
            throw new NotImplementedException();
        }
    }

}
