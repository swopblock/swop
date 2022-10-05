using Swopblock.Intentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swopblock.Intentions;
using System.Net;
using System.Security.Cryptography;

namespace Swopblock
{
    internal class DemoWeb
    {
        public static List<string> Patterns = new List<string> 
        {
            "i am * * exactly * * * * of mine at address * * in order to * * at least * * * * of yours at address * * and my order is good until the market volume reaches * * swobl using my signature * *.",
            "i am * * exactly * * * * of mine at address * * in order to * * at least * * * * of yours from the market and my order is good until the market volume reaches * * swobl using my signature * *.",
            "i am * * at least * * * * of yours at address * * in order to * * exactly * * * * of mine at address * * and my order is good until the market volume reaches * * swobl using my signature * *.",
            "i am * * at least * * * * of yours from the market in order to * * exactly * * * * of mine at address * * and my order is good until the market volume reaches * * swobl using my signature * *."
        };


        public static DataBag DefaultParse(string intention)
        {
            /*
           * I am [bidding] exactly [100] [SWOBL] of mine at address [cid] 
           * in order to buy at least [1] [BTC] of yours at address [address]
           * and my order is good until the market volume reaches [expirationVolume] SWOBL 
           * using my signature [transferId].
           */

            /*
             * I am [asking] at least [7000] [BTC] of yours from the market in order to
             * sell exactly [30] [SWOBL] of mine from my address [370] 
             * and my order is good until the market volume reaches [79999] SWOBL 
             * using my signature [2]."
             */

            string assetTag = "";
            int assetID = 0;
            decimal cashSup = 0;
            decimal cashDem = 0;
            decimal assetSup = 0;
            decimal assetDem = 0;
            decimal cashLock = 0;

            MatchResult res = null;

            foreach (string pattern in DemoWeb.Patterns)
            {
                res = IntentionBranch.MatchesPattern(intention, pattern);

                if (res.Matches)
                {
                    break;
                }
            }

            if (res != null)
            {
                if (res.EmbeddedValues != null)
                {
                    if (res.EmbeddedValues.Count > 7)
                    {
                        if (res.EmbeddedValues[0].ToLower() == "bidding")
                        {
                            if (res.EmbeddedValues[2].ToLower() == "swobl")
                            {
                                cashSup = decimal.Parse(res.EmbeddedValues[1]);
                                cashDem = 0;
                                assetSup = 0;
                                assetDem = decimal.Parse(res.EmbeddedValues[5]);
                                cashLock = decimal.Parse(res.EmbeddedValues[8]);

                                assetTag = res.EmbeddedValues[6].ToLower();
                            }
                        }
                        else if (res.EmbeddedValues[0].ToLower() == "asking")
                        {
                            assetTag = res.EmbeddedValues[2].ToLower();

                            if (assetTag.ToLower() != "swobl")
                            {
                                cashDem = 0;
                                cashSup = decimal.Parse(res.EmbeddedValues[3]);
                                assetDem = decimal.Parse(res.EmbeddedValues[1]);
                                cashLock = decimal.Parse(res.EmbeddedValues[8]);
                                assetSup = 0;
                            }
                        }

                        for (int i = 0; i < ushort.MaxValue; i++)
                        {
                            Program.AssetTags tag = (Program.AssetTags)i;

                            if (tag.ToString().ToLower() == assetTag)
                            {
                                assetID = i;
                                break;
                            }

                            // break if tag no longer converts to text
                        }
                    }
                }
            }
            return new DataBag
            {
               assetID = assetID,
               cashDem = cashDem,
               cashSup = cashSup,
               assetDem = assetDem,
               assetSup = assetSup,
               cashLock = cashLock
            };
        }
        public class DataBag
        {
            public int assetID = 0;
            public decimal cashSup = 0;
            public decimal cashDem = 0;
            public decimal assetSup = 0;
            public decimal assetDem = 0;
            public decimal cashLock = 0;
        }

        public class StateBag
        {
            public SimulationStates nState { get; set; }
            public Report Report { get; set; }
        }

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
