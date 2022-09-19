using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swopblock.Intentions
{
    /// <summary>
    /// Object for storing pattern matching information
    /// </summary>
    public class MatchResult
    {
        public string Value { get; set; }
        public bool Matches { get; set; }
        public int Length { get; set; }
        public List<string> EmbeddedValues { get; set; }
    }
}
