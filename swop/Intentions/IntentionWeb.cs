using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swopblock.Intentions
{ 
    public class IntentionWeb
    { 
        public string NameTag { get; set; }
        public DataType dType = DataType.Tag;
        public List<IntentionWeb> SubBranchTags = new List<IntentionWeb>();

        private bool scanned = false;

        public IntentionWeb(string nameTag, List<IntentionWeb> subBranchTags, DataType dataType = DataType.Tag)
        {
            NameTag = nameTag;
            SubBranchTags = subBranchTags;
            this.dType = dataType;

        }

        public IntentionWeb() { }

        public void Reset()
        {
            scanned = false;

            if (SubBranchTags != null)
            {
                if (SubBranchTags.Count > 0)
                {
                    SubBranchTags.ForEach(x => x.Reset());
                }
            }
        }            

        public IntentionWeb Copy()
        {
            List<IntentionWeb> subs = new List<IntentionWeb>();

            if (SubBranchTags != null)
            {
                foreach (IntentionWeb sub in SubBranchTags)
                {
                    subs.Add(sub.Copy());
                }
            }

            return new IntentionWeb
            {
                NameTag = this.NameTag,
                scanned = this.scanned,
                SubBranchTags = subs,
                dType = this.dType
            };
        }

        public bool TagEquals(string name)
        {
            string nt = this.NameTag.Replace('_', ' ');
            string nm = name.Replace('_', ' ');

            return nt.ToLower() == nm.ToLower();
        }

        public string[] GetAllNames()
        {
            List<string> names = new List<string>();

            if (!scanned)
            {
                names.Add(NameTag);

                if (SubBranchTags != null)
                {
                    if (SubBranchTags.Count > 0)
                    {
                        foreach(IntentionWeb sub in SubBranchTags)
                        {
                            names.AddRange(sub.GetAllNames());
                        }
                    }
                }
            }

            return names.ToArray();
        }
        public IntentionWeb GetByName(string name)
        {
            List<IntentionWeb> wb = new List<IntentionWeb>();

            if (this.TagEquals(name)) wb.Add(this.Copy());

            if (!scanned)
            {
                scanned = true;

                if (SubBranchTags != null)
                {
                    foreach (IntentionWeb web in SubBranchTags)
                    {
                        if (web.TagEquals(name))
                        {
                            wb.Add(web);
                        }
                        else
                        {
                            if (!web.scanned)
                            {
                                IntentionWeb iw = web.GetByName(name);

                                if (iw != null) 
                                { 
                                    wb.Add(iw); 
                                }
                            }
                        }
                       
                    }
                }

                if (wb != null)
                {
                    if (wb.Count > 0)
                    {
                        return CombineWeb(wb);
                    }

                }
            }

            return null;
           
        }

        private static DataType getDataType(List<IntentionWeb> webs)
        {
            foreach(IntentionWeb w in webs)
            {
                if(w.dType != DataType.Tag)
                {
                    return w.dType;
                }
            }

            return DataType.Tag;
        }

        public static IntentionWeb CombineWeb(List<IntentionWeb> web)
        {
            IntentionWeb comb = null;

            if (web != null)
            {
                comb = web.FirstOrDefault();

                if (comb != null)
                {
                    comb = comb.Copy();

                    if (web.Count > 1)
                    {
                        List<IntentionWeb> nWeb = web.Where(x => x.NameTag == comb.NameTag).ToList();

                        foreach (IntentionWeb w in nWeb)
                        {
                            if (w.SubBranchTags != null)
                                comb.SubBranchTags.AddRange(w.SubBranchTags);
                        }

                        comb.SubBranchTags = comb.SubBranchTags.DistinctBy(x => x.NameTag).ToList();

                        comb.dType = getDataType(web);
                    }
                }
            }

            return comb;
        }

        public static IntentionWeb DefaultStructure()
        {
            IntentionWeb web = new IntentionWeb("i am", new List<IntentionWeb>
            {
                new IntentionWeb("paying", new List<IntentionWeb>
                {
                    new IntentionWeb("at least", new List<IntentionWeb>
                    {
                        new IntentionWeb("value", new List<IntentionWeb>
                        {
                            new IntentionWeb("and at most", new List<IntentionWeb>
                            {
                                new IntentionWeb("value", null)
                            }),
                            new IntentionWeb("USD", new List<IntentionWeb>
                            {
                                new IntentionWeb("of yours", new List<IntentionWeb>
                                {
                                    new IntentionWeb("at address", new List<IntentionWeb>
                                    {
                                        new IntentionWeb("reference", new List<IntentionWeb>
                                        {
                                            new IntentionWeb("in order to", new List<IntentionWeb>
                                            {
                                                new IntentionWeb("buy", new List<IntentionWeb>
                                                {
                                                     new IntentionWeb("at least", null)
                                                }),
                                                new IntentionWeb("send", new List<IntentionWeb>
                                                {
                                                     new IntentionWeb("at least", null)
                                                }),
                                            }),
                                            new IntentionWeb("and the payment is pending until when upon acceptance you withdraw", new List<IntentionWeb>
                                            {
                                                new IntentionWeb("of mine",null),
                                                new IntentionWeb("of yours",null),
                                                new IntentionWeb("value", null)
                                            }),
                                            new IntentionWeb("and my order is good until the market volume reaches", new List<IntentionWeb>
                                            {
                                                new IntentionWeb("value", null),
                                            }),
                                        }, DataType.StringValue),
                                    }),
                                    new IntentionWeb("from the market", new List<IntentionWeb>
                                    {
                                        new IntentionWeb("and my order is good until the market volume reaches", null)
                                })
                                }),
                                new IntentionWeb("of mine", new List<IntentionWeb>
                                {
                                    new IntentionWeb("at address", new List<IntentionWeb>
                                    {

                                    }),
                                    new IntentionWeb("from the market", new List<IntentionWeb>
                                    { 

                                    })
                                }),
                            }),

                            new IntentionWeb("SWOBL", new List<IntentionWeb>
                            {
                                new IntentionWeb("of yours", new List<IntentionWeb>
                                {
                                    
                                }),
                                new IntentionWeb("of mine", new List<IntentionWeb>
                                {

                                }),
                                new IntentionWeb("using", null)
                            }),
                            new IntentionWeb("ETH", new List<IntentionWeb>
                            {
                                 new IntentionWeb("of yours", new List<IntentionWeb>
                                {

                                }),
                                new IntentionWeb("of mine", new List<IntentionWeb>
                                {

                                }),
                            }),
                            new IntentionWeb("BTC", new List<IntentionWeb>
                            {
                                 new IntentionWeb("of yours", new List<IntentionWeb>
                                {

                                }),
                                new IntentionWeb("of mine", new List<IntentionWeb>
                                {

                                }),
                            }),
                        }, DataType.StringValue)
                    }),
                    new IntentionWeb("exactly", new List<IntentionWeb>
                    {
                         new IntentionWeb("value", null)
                    }),
                }),
                new IntentionWeb("ordering", new List<IntentionWeb>
                {
                    new IntentionWeb("exactly", null),
                    new IntentionWeb("at least", new List<IntentionWeb>
                    {
                        new IntentionWeb("value", new List<IntentionWeb>
                        {
                            new IntentionWeb("USD", new List<IntentionWeb>
                            {
                                new IntentionWeb("of yours", new List<IntentionWeb>
                                {
                                    new IntentionWeb("at address", new List<IntentionWeb>
                                    {
                                        new IntentionWeb("reference", new List<IntentionWeb>
                                        {
                                            new IntentionWeb("in order to", new List<IntentionWeb>
                                            {
                                                new IntentionWeb("buy", new List<IntentionWeb>
                                                {

                                                }),
                                                new IntentionWeb("send", new List<IntentionWeb>
                                                {

                                                }),
                                            }),
                                            new IntentionWeb("and the payment is pending until when upon acceptance you withdraw", new List<IntentionWeb>
                                            {

                                            }),
                                        }),
                                    }),
                                }),
                                new IntentionWeb("of mine", new List<IntentionWeb>
                                {
                                    new IntentionWeb("at address", new List<IntentionWeb>
                                    {
                                        new IntentionWeb("reference", new List<IntentionWeb>
                                        {
                                            new IntentionWeb("in order to", new List<IntentionWeb>
                                            {
                                                new IntentionWeb("buy", new List<IntentionWeb>
                                                {

                                                }),
                                                new IntentionWeb("send", new List<IntentionWeb>
                                                {

                                                }),
                                            }),
                                            new IntentionWeb("and the payment is pending until when upon acceptance you withdraw", new List<IntentionWeb>
                                            {

                                            }),
                                        }),
                                    }),
                                }),
                            }),

                            new IntentionWeb("SWOBL", new List<IntentionWeb>
                            {
                                 new IntentionWeb("of yours", new List<IntentionWeb>
                                {
                                    new IntentionWeb("at address", new List<IntentionWeb>
                                    {
                                        new IntentionWeb("reference", new List<IntentionWeb>
                                        {
                                            new IntentionWeb("in order to", new List<IntentionWeb>
                                            {
                                                new IntentionWeb("buy", new List<IntentionWeb>
                                                {

                                                }),
                                                new IntentionWeb("send", new List<IntentionWeb>
                                                {

                                                }),
                                            }),
                                            new IntentionWeb("and the payment is pending until when upon acceptance you withdraw", new List<IntentionWeb>
                                            {

                                            }),
                                        }),
                                    }),
                                }),
                                new IntentionWeb("of mine", new List<IntentionWeb>
                                {
                                    new IntentionWeb("at address", new List<IntentionWeb>
                                    {
                                        new IntentionWeb("reference", new List<IntentionWeb>
                                        {
                                            new IntentionWeb("in order to", new List<IntentionWeb>
                                            {
                                                new IntentionWeb("buy", new List<IntentionWeb>
                                                {

                                                }),
                                                new IntentionWeb("send", new List<IntentionWeb>
                                                {

                                                }),
                                            }),
                                            new IntentionWeb("and the payment is pending until when upon acceptance you withdraw", new List<IntentionWeb>
                                            {

                                            }),
                                        }),
                                    }),
                                }),
                            }),
                            new IntentionWeb("ETH", new List<IntentionWeb>
                            {
                                 new IntentionWeb("of yours", new List<IntentionWeb>
                                {
                                    new IntentionWeb("at address", new List<IntentionWeb>
                                    {
                                        new IntentionWeb("reference", new List<IntentionWeb>
                                        {
                                            new IntentionWeb("in order to", new List<IntentionWeb>
                                            {
                                                new IntentionWeb("buy", new List<IntentionWeb>
                                                {

                                                }),
                                                new IntentionWeb("send", new List<IntentionWeb>
                                                {

                                                }),
                                            }),
                                            new IntentionWeb("and the payment is pending until when upon acceptance you withdraw", new List<IntentionWeb>
                                            {

                                            }),
                                        }),
                                    }),
                                }),
                                new IntentionWeb("of mine", new List<IntentionWeb>
                                {
                                    new IntentionWeb("at address", new List<IntentionWeb>
                                    {
                                        new IntentionWeb("reference", new List<IntentionWeb>
                                        {
                                            new IntentionWeb("in order to", new List<IntentionWeb>
                                            {
                                                new IntentionWeb("buy", new List<IntentionWeb>
                                                {

                                                }),
                                                new IntentionWeb("send", new List<IntentionWeb>
                                                {

                                                }),
                                            }),
                                            new IntentionWeb("and the payment is pending until when upon acceptance you withdraw", new List<IntentionWeb>
                                            {

                                            }),
                                        }),
                                    }),
                                }),
                            }),
                            new IntentionWeb("BTC", new List<IntentionWeb>
                            {
                                 new IntentionWeb("of yours", new List<IntentionWeb>
                                {
                                    new IntentionWeb("at address", new List<IntentionWeb>
                                    {
                                        new IntentionWeb("reference", new List<IntentionWeb>
                                        {
                                            new IntentionWeb("in order to", new List<IntentionWeb>
                                            {
                                                new IntentionWeb("buy", new List<IntentionWeb>
                                                {

                                                }),
                                                new IntentionWeb("send", new List<IntentionWeb>
                                                {

                                                }),
                                            }),
                                            new IntentionWeb("and the payment is pending until when upon acceptance you withdraw", new List<IntentionWeb>
                                            {

                                            }),
                                        }),
                                    }),
                                }),
                                new IntentionWeb("of mine", new List<IntentionWeb>
                                {
                                    new IntentionWeb("at address", new List<IntentionWeb>
                                    {
                                        new IntentionWeb("reference", new List<IntentionWeb>
                                        {
                                            new IntentionWeb("in order to", new List<IntentionWeb>
                                            {
                                                new IntentionWeb("buy", new List<IntentionWeb>
                                                {

                                                }),
                                                new IntentionWeb("send", new List<IntentionWeb>
                                                {

                                                }),
                                            }),
                                            new IntentionWeb("and the payment is pending until when upon acceptance you withdraw", new List<IntentionWeb>
                                            {

                                            }),
                                        }),
                                    }),
                                }),
                            }),
                        })
                    }),
                }),
                new IntentionWeb("bidding", new List<IntentionWeb>
                {
                    new IntentionWeb("exactly", null),
                    new IntentionWeb("at least", null)
                }),
                new IntentionWeb("selling", new List<IntentionWeb>
                {
                    new IntentionWeb("exactly", null),
                    new IntentionWeb("at least", null)
                }),
                new IntentionWeb("using", new List<IntentionWeb>
                {
                    new IntentionWeb("my signature", new List<IntentionWeb>
                    {
                        new IntentionWeb("reference", null)
                    }),
                    new IntentionWeb("my policy", new List<IntentionWeb>
                    {
                        new IntentionWeb("reference", new List<IntentionWeb>
                        {
                            new IntentionWeb("and", new List<IntentionWeb>
                            {
                                new IntentionWeb("the swopblock market protocol", new List<IntentionWeb>
                                {
                                    new IntentionWeb("reference", null)
                                })
                            })
                        })
                    })
                }),
                new IntentionWeb("clearing", new List<IntentionWeb>
                {
                    new IntentionWeb("at least", null)
                })
            }, DataType.Root);

            return web;
        }
    }
}
