using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swopblock.Stack.NetworkLayer
{
    internal class Settings
    {
        public static string SuperBlockFolder { get; set; }
        public static int SuperBlockCount { get; set; }
        public static List<string> NetworkPeers { get; set; }
    }
}
