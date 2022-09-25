using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swop.Demo
{
    public static class Extentions
    {
        public static ContractStreamStates Add(this ContractStreamStates obj, ContractStreamStates two)
        {
            ContractStreamStates cState = new ContractStreamStates(
                0,
                obj.ContractCashVolume + two.ContractCashVolume,
                0,
                obj.ContractAssetVolume + two.ContractAssetVolume,
                0);


            return cState;
        }
    }
}
