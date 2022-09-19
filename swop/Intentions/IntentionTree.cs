using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Swopblock.Intentions
{
    public class IntentionTree
    { 
        public IntentionSerializer Serializer { get; set; }
       
        //i am paying at most 200 and at least 100 swobl of mine at address SomeAddress in order to send at most 25 and at least 20 usd of mine at address SomeAddress and the payment is pending until when upon acceptance you withdraw 1 btc of yours at address OtherAddress
        
        public IntentionTree()
        {
            
        }

        public IntentionTree(IntentionSerializer CustomSerializer)
        {
            Serializer = CustomSerializer;
        }


        public IntentionTree(IntentionWeb WebStructure)
        {
            Serializer = new IntentionSerializer(WebStructure);
        }

        public bool Validate(string intention)
        {
            byte[] serial = Serializer.Serialize(intention);

            string result = Serializer.Deserialize(serial);

            return intention.ToLower().Trim() == result.ToLower().Trim();
        }

        public static IntentionTree DefaultTree()
        {
            return new IntentionTree(IntentionWeb.DefaultStructure());
        }
    }
}
