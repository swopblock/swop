using Swopblock.Intentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swopblock.Intentions;

namespace Swopblock
{
    internal class DemoWeb
    {
        public static string pattern = "i want to * * * * * * for * *";

        private static IntentionWeb demo = new IntentionWeb("i want to", new List<IntentionWeb>
        {
        new IntentionWeb("bid", new List<IntentionWeb>
        {
            new IntentionWeb("number", new List<IntentionWeb>
            {
                new IntentionWeb("SWOBL", new List<IntentionWeb>
                {
                    new IntentionWeb("for", new List<IntentionWeb>
                    {
                        new IntentionWeb("BTC", new List<IntentionWeb>
                        {

                        }),
                        new IntentionWeb("ETH", new List<IntentionWeb>
                        {

                        })
                    })
                })
            }, DataType.StringValue)
        }),
        new IntentionWeb("ask", new List<IntentionWeb>
        {
            new IntentionWeb("number", null)
        }),
        new IntentionWeb("buy", new List<IntentionWeb>
        {
            new IntentionWeb("BTC", new List<IntentionWeb>
            {
                new IntentionWeb("with", new List<IntentionWeb>
                {
                    new IntentionWeb("number", null)
                })
            }),
            new IntentionWeb("ETH", new List<IntentionWeb>
            {

            })
        }),
        new IntentionWeb("sell", new List<IntentionWeb>
        {
            new IntentionWeb("BTC", new List<IntentionWeb>
            {
                new IntentionWeb("for", new List<IntentionWeb>
                {
                    new IntentionWeb("number", null)
                })
            }),
            new IntentionWeb("ETH", new List<IntentionWeb>
            {
                 new IntentionWeb("for", null)
            })
        }),
        }, DataType.Root);

        public static IntentionTree GetTree()
        {
            return new IntentionTree(demo);
        }
    }
}
