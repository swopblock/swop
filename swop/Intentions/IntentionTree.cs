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

        /// <summary>
        /// Checks if the submited intention is a valid format
        /// </summary>
        /// <param name="intention"></param>
        /// <returns></returns>
        public bool Validate(string intention)
        {
            if(intention != null)
            {
                if(intention != string.Empty)
                {
                    byte[] serial = Serializer.Serialize(intention);

                    string result = Serializer.Deserialize(serial);

                    return intention.ToLower().Trim() == result.ToLower().Trim();
                }
            }

            return false;
        }

        public static IntentionTree DefaultTree()
        {
            return new IntentionTree(IntentionWeb.DefaultStructure());
        }
    }
}
