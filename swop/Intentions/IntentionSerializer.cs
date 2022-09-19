using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swopblock.Intentions.Utilities;

namespace Swopblock.Intentions
{
    public class IntentionSerializer
    {
        public IntentionBranch structuredTrunk { get; set; }

        public static readonly byte byteStart = 65;
        public static readonly byte reservedCap = 3;
        public static readonly byte dataNext = 0;
        public static readonly byte sentenceEnd = 1;
        public static readonly byte endChar = (byte)('.');

        public IntentionSerializer() 
        {
            structuredTrunk = new IntentionBranch();
        }
        public IntentionSerializer(IntentionWeb WebStructure)
        {
            BuildStructure(WebStructure, WebStructure.GetAllNames());

            if(structuredTrunk == null) structuredTrunk = new IntentionBranch();
        }

        public virtual byte[] Serialize(string intentions)
        {
            List<byte> data = new List<byte>();

            int start = 0;

            IntentionBranch branchRoot = structuredTrunk;

            while (start < intentions.Length)
            {
                int val = -1;

                if (intentions[start] == ' ')
                {
                    start++;
                }

                if(start >= intentions.Length)
                {
                    break;
                }

                if (intentions[start] == endChar)
                {
                    branchRoot = structuredTrunk;
                    data.Add(sentenceEnd);
                }
                else
                {
                    val = structuredTrunk.MatchesPattern(intentions, start);

                    IntentionBranch tmp = null;

                    if (val != -1)
                    {
                       tmp = branchRoot;
                    }

                    foreach (IntentionBranch branch in branchRoot.SubBranches)
                    { 
                        if (val == -1)
                        {
                            val = branch.MatchesPattern(intentions, start);

                            tmp = branch;
                        }
                    }

                    if (val != -1)
                    {
                        if (val > 0)
                        {
                            data.Add(tmp.byteTag);
                            start += val;
                        }
                        else
                        {
                            int inx = intentions.IndexOf(' ', start);
                            int qinx = intentions.IndexOf('"', start);

                            int inxEnd = intentions.IndexOf((char)endChar, start);

                            if(qinx != -1)
                            {
                                if(qinx < inx)
                                {
                                    int qq = intentions.IndexOf('"', qinx + 1);

                                    if(qq != -1)
                                    {
                                        inx = intentions.IndexOf(' ', qq);
                                    }
                                }
                            }

                            if (inxEnd != -1)
                            {
                                if (inx > inxEnd)
                                {
                                    inx = inxEnd;
                                }
                            }

                            string str = string.Empty;

                            if (inx != -1)
                            {
                                str = intentions.Substring(start, inx - start);
                            }
                            else
                            {
                                str = intentions.Substring(start);
                            }

                            if (str.Length < 256)
                            {
                                data.Add(dataNext);
                                data.Add((byte)str.Length);
                                byte[] dta = Utility.ConvertToBytes(str);
                                data.AddRange(dta);
                                start += str.Length;
                            }
                        }

                        branchRoot = tmp;
                    }
                }

                if (val == -1)
                {
                    start++;
                }

                if (branchRoot.SubBranches == null)
                {
                    branchRoot = structuredTrunk;
                }
                else
                {
                    if (branchRoot.SubBranches.Count == 0)
                    {
                        branchRoot = structuredTrunk;
                    }
                }
            }

            return data.ToArray();
        }

        public virtual string Deserialize(byte[] data)
        {
            string code = "";

            int start = 0;

            IntentionBranch branchRoot = structuredTrunk;

            bool hit = false;

            while (start < data.Length)
            {
                if (data[start] == sentenceEnd)
                {
                    if(branchRoot.Name != structuredTrunk.Name)
                    {
                        code += (char)endChar;
                        hit = false;
                    }

                    if (hit)
                    {
                        code += " ";
                    }

                    structuredTrunk.Reset();
                    IntentionBranch tr = structuredTrunk.GetBranchTrunk();

                    if (tr != null)
                    {
                        branchRoot = tr;
                    }
                    else
                    {
                        branchRoot = structuredTrunk;
                    }

                    start++;
                }
                else
                {
                    if(hit)code += " ";
                }

                hit = false;                             

                if (branchRoot != null)
                {
                    if (branchRoot.dType == DataType.Ignore)
                    {
                        if (branchRoot.SubBranches != null)
                        {
                            if (branchRoot.SubBranches.Count > 0)
                            {
                                branchRoot = branchRoot.SubBranches.First();
                            }
                        }
                    }

                    if (start < data.Length)
                    {
                        if (branchRoot.byteTag == data[start])
                        {
                            if (branchRoot.dType != DataType.StringValue)
                            {
                                code += branchRoot.Name;
                            }

                            start++;

                            hit = true;

                            if (branchRoot.SubBranches != null)
                            {
                                if (branchRoot.SubBranches.Count > 0)
                                {
                                    if (data.Length > start + 1)
                                    {
                                        if (data[start] == dataNext)
                                        {
                                            start++;

                                            byte len = data[start++];

                                            if (data.Length >= start + len)
                                            {

                                                byte[] dt = Utility.GetNextByteSet(data, start, len);

                                                string str = Utility.ConvertToString(dt);

                                                code += " " + str;

                                                start += (str.Length);

                                                branchRoot = branchRoot.SubBranches.Where(x => x.dType == DataType.StringValue).FirstOrDefault();

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (branchRoot != null)
                    {
                        if (branchRoot.SubBranches != null)
                        {
                            if (branchRoot.SubBranches.Count > 0)
                            {
                                foreach (IntentionBranch br in branchRoot.SubBranches)
                                {
                                    if (data.Length > start)
                                    {
                                        if (data[start] != dataNext)
                                        {
                                            if (br.byteTag == data[start])
                                            {
                                                hit = true;
                                                branchRoot = br;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!hit)
                    {
                        start++;
                    }
                }
                else
                {
                    start++;
                }
            }

            return code.Trim();
        }

        public virtual void BuildStructure(IntentionWeb WebStructure, string[] Terms)
        {//Enum.GetNames<NamedTerms>().Length
            List<IntentionBranch> branches = new List<IntentionBranch>();

            for (int term = 0; term < Terms.Length; term++)
            {
                WebStructure.Reset();

                string name = Terms[term];

                IntentionWeb sub = WebStructure.GetByName(name);

                if (sub != null)
                {
                    // Debug.WriteLine(name);

                    IntentionBranch branch = branches.Where(x => sub.TagEquals(x.Name)).FirstOrDefault();

                    if (branch == null)
                    {
                        branch = new IntentionBranch
                        {
                            Name = sub.NameTag,
                            dType = sub.dType,
                            SubBranches = new List<IntentionBranch>()
                        };
                    }

                    if (branch.SubBranches == null)
                    {
                        branch.SubBranches = new List<IntentionBranch>();
                    }
                    branches.Add(branch);
                }
            }

            for (int term = 0; term < Terms.Length; term++)
            {
                WebStructure.Reset();

                string name = Terms[term];

                IntentionWeb sub = WebStructure.GetByName(name);

                if (sub != null)
                {
                    IntentionBranch branch = branches.Where(x => sub.TagEquals(x.Name)).FirstOrDefault();

                    if (branch.SubBranches == null)
                    {
                        branch.SubBranches = new List<IntentionBranch>();
                    }

                    branches.Add(branch);

                    foreach (IntentionWeb wb in sub.SubBranchTags)
                    {
                        IntentionBranch subBr = branches.Where(x => wb.TagEquals(x.Name)).FirstOrDefault();

                        if (subBr != null)
                        {
                            branch.SubBranches.Add(subBr);
                        }
                        else
                        {
                            branch.SubBranches.Add(
                                new IntentionBranch
                                {
                                    Name = wb.NameTag,
                                    dType = wb.dType
                                });
                        }
                    }

                    branch.SubBranches = branch.SubBranches.DistinctBy(x => x.Name).OrderBy(x => (int)x.dType).ToList();
                }
            }

            AssignEncodingBytes(branches);

            structuredTrunk = branches.Where(x => x.dType == DataType.Root).FirstOrDefault();
        }

        public virtual List<IntentionBranch> AssignEncodingBytes(List<IntentionBranch> webs)
        {
            List<IntentionBranch> setBinaryTerm = webs.OrderBy(x => x.SubBranches.Count).ToList();

            int bt = byteStart;

            foreach (IntentionBranch branch in setBinaryTerm)
            {
                foreach (IntentionBranch br in branch.SubBranches)
                {
                    if (br.byteTag == 0)
                    {
                        bt %= 256;

                        if (bt < reservedCap) bt = reservedCap;

                        br.byteTag = (byte)(bt++);
                    }
                }
            }

            List<IntentionBranch> subBranches = setBinaryTerm.Where(x => x.dType == DataType.Root).ToList();

            subBranches = subBranches.DistinctBy(x => x.Name).ToList();

            bt = 65;

            foreach (IntentionBranch br in subBranches)
            {
                if (br.byteTag == 0)
                {
                    bt %= 256;

                    if (bt < reservedCap) bt = reservedCap;

                    br.byteTag = (byte)(bt++);
                }
            }

            return subBranches.ToList();
        }
        public int MatchesPattern(byte[] data, string match)
        {

            return -1;
        }
    }
}
