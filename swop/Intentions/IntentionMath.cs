using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swopblock.Intentions.Utilities;

namespace Swopblock.Intentions
{
    public class IntentionMath
    {
        public static string ConvertToSiUnit(decimal dec)
        {
            string value = "";

            for(int i = 0; i < SiPrefix.PrefixList.Count; i++)
            {
                if (SiPrefix.PrefixList[i].Multiplier <= dec)
                {
                    decimal dm = dec / SiPrefix.PrefixList[i].Multiplier;

                    value = dm + SiPrefix.PrefixList[i].Prefix;

                    break;
                }
            }

            return value;
        }

        public static decimal ConvertFromSiUnit(string SiUnit)
        {
            decimal value = 0;

            for (int c = 0; c < SiUnit.Length; c++)
            {
                string pr = SiUnit.Substring(c);

                if (!Utility.HasNumbers(pr))
                {
                    for (int i = 0; i < SiPrefix.PrefixList.Count; i++)
                    {
                        if (SiPrefix.PrefixList[i].Prefix == pr)
                        {
                            decimal prValue = SiPrefix.PrefixList[i].Multiplier;

                            decimal strValue = Convert.ToDecimal(SiUnit.Substring(0, c));

                            value = strValue * prValue;

                            return value;
                        }
                    }
                }
            }

            return value;
        }

        public class SiPrefix
        {
            public string Prefix { get; set; }
            public decimal Multiplier { get; set; }

            public static List<SiPrefix> PrefixList = new List<SiPrefix>
            {
                // positive exponent
                new SiPrefix
                {
                    Prefix = "Y",
                    Multiplier = 1000000000000000000000000m
                },
                new SiPrefix
                {
                    Prefix = "Z",
                    Multiplier = 1000000000000000000000m
                },
                new SiPrefix
                {
                    Prefix = "E",
                    Multiplier = 1000000000000000000m
                },
                new SiPrefix
                {
                    Prefix = "P",
                    Multiplier = 1000000000000000m
                },
                new SiPrefix
                {
                    Prefix = "T",
                    Multiplier = 1000000000000m
                },   
                new SiPrefix
                {
                    Prefix = "G",
                    Multiplier = 1000000000m
                },   
                new SiPrefix
                {
                    Prefix = "M",
                    Multiplier = 1000000m
                },   
                new SiPrefix
                {
                    Prefix = "k",
                    Multiplier = 1000m
                },   
                new SiPrefix
                {
                    Prefix = "h",
                    Multiplier = 100m
                },   
                new SiPrefix
                {
                    Prefix = "da",
                    Multiplier = 10m
                },  
                /// negative exponent
                new SiPrefix
                {
                    Prefix = "d",
                    Multiplier = 0.1m
                },   
                new SiPrefix
                {
                    Prefix = "c",
                    Multiplier = 0.01m
                },   
                new SiPrefix
                {
                    Prefix = "m",
                    Multiplier = 0.001m
                },
                new SiPrefix
                {
                    Prefix = "u",
                    Multiplier = 0.000001m
                },
                new SiPrefix
                {
                    Prefix = "n",
                    Multiplier = 0.000000001m
                },
                new SiPrefix
                {
                    Prefix = "p",
                    Multiplier = 0.000000000001m
                },
                new SiPrefix
                {
                    Prefix = "f",
                    Multiplier = 0.000000000000001m
                },
                new SiPrefix
                {
                    Prefix = "a",
                    Multiplier = 0.000000000000000001m
                },
                new SiPrefix
                {
                    Prefix = "z",
                    Multiplier = 0.000000000000000000001m
                },
                new SiPrefix
                {
                    Prefix = "y",
                    Multiplier = 0.000000000000000000000001m
                }
            };
        }
    }
}
