using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swopblock.Intentions
{
    // some datatypes are implemented some are not yet 
    public enum DataType { Name, Tag, Ignore, Root, Trunk, StringValue };
    public class IntentionBranch
    {
        public string Name { get; set; }
        public DataType dType { get; set; }
        public byte byteTag = 0;

        private bool scanned = false;

        public List<IntentionBranch> SubBranches { get; set; }
        public IntentionBranch()
        {
            SubBranches = new List<IntentionBranch>();
        }

        public void Reset()
        {
            if (scanned == true)
            {
                scanned = false;

                if (SubBranches != null)
                {
                    if (SubBranches.Count > 0)
                    {
                        SubBranches.ForEach(x => x.Reset());
                    }
                }
            }
        }

        public IntentionBranch GetBranchTrunk()
        {
            if (!scanned)
            {
                if (dType == DataType.Trunk)
                {
                    return this;
                }

                scanned = true;

                if (SubBranches != null)
                {
                    if (SubBranches.Count > 0)
                    {
                        foreach (IntentionBranch br in SubBranches)
                        {
                            IntentionBranch brch = br.GetBranchTrunk();

                            if (brch != null)
                            {
                                return brch;
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// This automatically sets the byte encoding for serialization. You may need to manually do this if your sub nodes exceed 256 sub nodes for a single IntentionWeb
        /// </summary>
        /// <param name="start">What binary value to start the auto byte encoding at</param>
        public void SetByteEncoding(byte start = 65)
        {
            if(byteTag == 0)
            {
                // you need to do this so the current IntentBranch is assigned the first byteTag
                byteTag = start;
            }

            // do it incrementally for the SubBranches
            if (SubBranches != null)
            {
                if (SubBranches.Count > 0)
                {
                    for(int i = 0; i < SubBranches.Count; i++)
                    {
                        if (SubBranches[i].byteTag == 0)
                        { 
                            // iterate forward
                            SubBranches[i].byteTag = start++;
                        }
                    }
                    for (int i = 0; i < SubBranches.Count; i++)
                    {
                        // check if it has been previously set
                        if (!scanned)
                        {
                            scanned = true;
                            SubBranches[i].SetByteEncoding();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Finds the number of characters that matches the pattern. Used to compare post serialized intention segment with pre serialized ones.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public int MatchesPattern(string code, int startIndex = 0)
        {
            if (code.Length > startIndex)
            {
                string startat = code.Substring(startIndex);

                if (dType != DataType.Tag && dType != DataType.Root && dType != DataType.Trunk)
                {
                    return 0;
                }
                else
                {
                    string parse = startat.ToLower();
                    string nameprs = Name.ToLower();

                    if (parse.Length >= nameprs.Length)
                    {
                        if (parse.Substring(0, nameprs.Length) == nameprs)
                        {
                            return nameprs.Length;
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Checks if an intention segment matches a pattern segment.
        /// </summary>
        /// <param name="original">The intention segment</param>
        /// <param name="match">The pattern segment</param>
        /// <returns></returns>
        public static MatchResult MatchesPattern(string original, string match)
        {
            List<string> emb = new List<string>();

            char trigger = (char)0; // used to tell if a value is a parameter

            // ii is the index in the pattern segment
            // we need this as a separate index since parameters are variable in size
            int ii = 0;

            // loop through the original text
            for (int i = 0; i < original.Length; i++)
            {
                if (ii < match.Length)
                {
                    char c = original[i]; // current placement in original
                    char m = match[ii];   // current placement in pattern

                    // check if the chars at position i and position ii match
                    if (m != c)
                    {
                        if (m == '*') //checks if we are at the begining of a parameter
                        {
                            if (match.Length > ii + 2)
                            {
                                //checks for end of a parameter
                                if (match[ii + 2] == '*')
                                {
                                    trigger = match[ii + 1];

                                    #region Check For Fail

                                    if (trigger == c)
                                    {

                                    }
                                    else if(i > 0)
                                    {
                                        if(trigger != original[i-1])
                                        {
                                            return new MatchResult
                                            {
                                                Matches = false
                                            };
                                        }
                                    }
                                    else
                                    {
                                        return new MatchResult
                                        {
                                            Matches = false
                                        };
                                    }

                                    #endregion

                                    #region Parameter Assessment 
                                    int inx = original.IndexOf(trigger, i + 1);

                                    int inp = original.IndexOf('.', i);

                                    if (trigger == ' ')
                                    {
                                        if (inx == -1 && inp != -1)
                                        {
                                            inx = inp;
                                        }

                                        if (inx != -1 && inp != -1)
                                        {
                                            if (inp < inx)
                                            {
                                                inx = inp;
                                            }
                                        }
                                    }

                                    if(inx == -1)
                                    {
                                        int inxTr = original.IndexOf(trigger, i + 1);

                                        if (original[original.Length -1] != trigger && inxTr == -1)
                                        {
                                            inx = original.Length;
                                        }
                                    }

                                    if (inx != -1)
                                    {
                                        string val = original.Substring(i, inx - i);

                                        val = val.Replace(trigger.ToString(), "");

                                        emb.Add(val);

                                        i += val.Length - 1;

                                        if (trigger != ' ') i += 2;

                                        ii += 3;
                                    }
                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            return new MatchResult
                            {
                                Matches = false
                            };
                        }
                    }
                    else
                    {
                        ii++;
                    }
                }

                else
                {
                    return new MatchResult
                    {
                        Matches = true,
                        Length = i,
                        EmbeddedValues = emb,
                        Value = match
                    };
                }
            }

            if (original != null)
            {
                if (original.Length > 0)
                {
                    return new MatchResult
                    {
                        Matches = true,
                        Length = original.Length,
                        EmbeddedValues = emb,
                        Value = match
                    };
                }
            }

            return new MatchResult
            {
                Matches = false
            };
        }
    }
}
