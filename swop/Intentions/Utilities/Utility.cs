using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace Swopblock.Intentions.Utilities
{
    public class Utility
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static readonly char[] hxChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };     
        
        private static string alphaNumeric = "0123456789qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";

        private static string Numeric = "0123456789.";
        
        /// <summary>
        /// Returns random readable text. Not a secure random.
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string RandomText(int len)
        {
            string txt = "";

            int rand = 0;

            Random rnum = new Random();

            for(int i = 0; i < len; i++)
            {
                rand = rnum.Next(0, alphaNumeric.Length);

                txt += alphaNumeric[rand];
            }

            return txt;
        }

        /// <summary>
        /// Gets completion text for intentions
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<string> ActiveBranch(IntentionBranch branch, string text)
        {
            string m = text.Trim();

            List<string> result = new List<string>();

            if (m.Length >= branch.Name.Length)
            {
                if (m.Substring(0, branch.Name.Length) == branch.Name || branch.dType == DataType.StringValue)
                {
                    if (branch.dType == DataType.StringValue)
                    {
                        m = m.TrimStart();
                        int i = m.IndexOf(' ');

                        if (i > 0)
                        {
                            m = m.Substring(i);
                        }
                    }
                    else
                    {
                        m = m.Substring(branch.Name.Length);
                    }

                    foreach (IntentionBranch sub in branch.SubBranches)
                    {
                        List<string> ac = ActiveBranch(sub, m);

                        for (int i = 0; i < ac.Count; i++)
                        {
                            if (m == string.Empty)
                            {
                                ac[i] = " " + ac[i];
                            }

                            result.Add(ac[i]);
                        }
                    }
                }
            }
            else
            {
                if (branch.Name.Substring(0, m.Length) == m)
                {
                    if (m.Length > 0)
                    {
                        result.Add(branch.Name.Substring(m.Length));
                    }
                }

                if (branch.dType == DataType.StringValue)
                {
                    if (result.Count == 0)
                        result.Add("");

                    result.Add(branch.Name + ": [Enter String Value]");
                }
            }


            return result;
        }

        //public static int GetVarIntByteLength(long value)
        //{
        //    if (value <= 0xFC) return 1;
        //    else if (value < 0xFE) return 3;
        //    else if (value < 0xFF) return 5;
        //    else return 9;
        //}


        /// <summary>
        /// Determins what type of number to convert a set of bytes to
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long GetIntegerValue(byte[] data)
        {
            if(data != null)
            {
                if(data.Length > 0)
                {
                    if(data.Length == 1) return data[0];
                    if (data.Length == 2) return BitConverter.ToInt16(data, 0);
                    if (data.Length == 4) return BitConverter.ToInt32(data, 0);
                    if (data.Length == 8) return BitConverter.ToInt64(data, 0);
                }
            }

            return 0;
        }

        /// <summary>
        /// Clips a set of bytes of a certain length from another byte array
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetNextByteSet(byte[] data, int startIndex, int length)
        {
            if (data != null)
            {
                if (data.Length >= startIndex + length && length > 0)
                {
                    byte[] value = new byte[length];

                    Array.Copy(data, startIndex, value, 0, length);

                    return value;
                }
            }

            return null;
        }

        /// <summary>
        /// Convert long unix time to a DateTime object
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(long unixTime)
        {
            return epoch.AddSeconds(unixTime);
        }

        /// <summary>
        /// Converts a string to byte array using char to byte direct conversion.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ConvertToBytes(string data)
        {
            List<byte> bytes = new List<byte>();

            if (data != null)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    bytes.Add((byte)data[i]);
                }
            }

            return bytes.ToArray();
        }

        /// <summary>
        /// Converts a byte array to string by way of a direct byte to char conversion
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ConvertToString(byte[] data)
        {
            string value = "";

            if (data != null)
            {

                for (int i = 0; i < data.Length; i++)
                {
                    value += (char)data[i];
                }
            }
            return value;
        }

        /// <summary>
        /// Checks if a hexadecimal string matches a byte array
        /// </summary>
        /// <param name="HexDecimalString"></param>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static bool ByteSegmentMatches(string HexDecimalString, byte[] data, int startIndex)
        {
            byte[] bts = HexDecimalToByteArray(HexDecimalString);

            if (bts != null)
            {

                if (bts.Length + startIndex > data.Length) return false;

                for (int i = startIndex; i < startIndex + bts.Length; i++)
                {
                    if (bts[i - startIndex] != data[i]) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if two byte arrays match
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static bool ByteArrayMatches(byte[] one, byte[] two)
        {
            if (one == null && two == null) return true;

            if (one != null && two != null)
            {
                if (one.Length != two.Length) return false;

                for (int i = 0; i < one.Length; i++)
                {
                    if (one[i] != two[i]) return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Swaps the byte order of a byte array
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] SwapEndian(byte[] data, int startIndex = 0, int length = -1)
        {
            if (data != null)
            {
                if (length < 0) length = data.Length;

                if (data.Length >= startIndex + length)
                {
                    byte[] bts = new byte[length];

                    int start = startIndex + bts.Length - 1;

                    for (int i = start; i >= startIndex; i--)
                    {
                        bts[start - i] = data[i];
                    }

                    return bts;
                }
            }

            return null;
        }

        private const string b58Digits = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        /// <summary>
        /// Converts a byte array to base58
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToBase58(byte[] data)
        {
            string result = string.Empty;  

            BigInteger intData = 0;
            for (int i = 0; i < data.Length; i++)
            {
                intData = (intData * 256) + data[i];
            }


            while (intData > 0)
            {
                int remainder = (int)(intData % 58);

                result = b58Digits[remainder] + result;

                intData /= 58;
            }

            for (int i = 0; i < data.Length && data[i] == 0; i++)
            {
                result = '1' + result;
            }

            return result;
        }

        /// <summary>
        /// Converts a byte array to hexadecimal
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToHexDecimal(byte[] data)
        {
            string hex = "";
            if (data != null)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    hex += data[i].ToString("x2");
                }
            }

            return hex;
        }
        #region Bits and Bytes and Other Annoying Things
        public static byte[] FromBits(string bitString)
        {
            List<byte> bytes = new List<byte>();

            for(int i = 0; i < bitString.Length; i+= 8)
            {
                int bt = 0;

                for(int b = i; b < i + 8; b++)
                {
                    if (b < bitString.Length)
                    {
                        if (bitString[b] == '1')
                        {
                            bt |= 1 << (8 - b);
                        }
                    }
                }

                bytes.Add((byte)bt);
            }

            return bytes.ToArray();
        }

        public static int FromHexCharacter(char c)
        {
            c = c.ToString().ToLower().First();

            for(int i = 0; i < hxChars.Length; i++)
            {
                if(c == hxChars[i])
                {
                    return i;
                }
            }

            return -1;
        }

        public static BigInteger HexToBigInteger(string hexDecimal)
        {
            BigInteger bg = 0;

            for (int i = 0; i < hexDecimal.Length; i++)
            {
                int inv = (hexDecimal.Length - 1) - i;
                bg += FromHexCharacter(hexDecimal[i]) * BigInteger.Pow(16, inv);
            }

            return bg;
        }

        public static string BigIntegerToHex(BigInteger bigValue)
        {
            byte[] bytes = bigValue.ToByteArray();

            BigInteger value = new BigInteger(bytes);

            string hex = "";

            while(value > 0)
            {
                BigInteger bg = value / 16;
                BigInteger rm = value % 16;

                hex += hxChars[(int)rm];

                value = bg;                
            }

            char[] vls = hex.Reverse().ToArray();

            hex = new string(vls);

            if(hex.Length % 2 != 0)
            {
                hex = "0" + hex;
            }

            return hex;
        }

        public static string ToBits(BigInteger bigValue)
        {
            byte[] bytes = bigValue.ToByteArray();

            BigInteger value = new BigInteger(bytes);

            string bits = "";

            while (value > 0)
            {
                BigInteger bg = value / 2;
                BigInteger rm = value % 2;

                bits += hxChars[(int)rm];

                value = bg;
            }

            char[] vls = bits.Reverse().ToArray();

            bits = new string(vls);

            return bits;
        }

        public static string ToBinaryBits(BigInteger bigValue)
        {
            byte[] bytes = bigValue.ToByteArray();
            var idx = bytes.Length - 1;

            string bits = "";

            var binary = Convert.ToString(bytes[idx], 2).PadLeft(8, '0');

            if (binary[0] != '0' && bigValue.Sign == 1)
            {
                bits += '0';
            }

            bits += binary;

            for (idx--; idx >= 0; idx--)
            {
                string pad = Convert.ToString(bytes[idx], 2).PadLeft(8, '0');
                bits += pad;
            }
            
            return bits;
        }

        public static byte[] bytesFromBits(string bitString)
        {
            List<byte> byt = new List<byte>();

            int bt = 0;

            for(int i = 0; i < bitString.Length; i+= 8)
            {
                bt = 0;

                for (int x = i + 7; x >= i; x--)
                {
                    if (bitString[x] == '1')
                    {
                        bt += (int)Math.Pow(2, (i + 7) - x);
                    }
                }

                byt.Add((byte)bt);
            }
            
            byt.Reverse();

            return byt.ToArray();
        }
        public static BigInteger NumberFromBits(string bitString)
        {
            BigInteger big = 0;

            char f = bitString.FirstOrDefault();

            if(f == '1')
            {
                big = 1;
            }

            for(int i = 0; i < bitString.Length; i++)
            {
                if(bitString[i] == '1')
                {
                    big++;
                }

                if(i != bitString.Length - 1) big *= 2;
            }

            return big;
        }
        #endregion
        /// <summary>
        /// Checks if an int is even
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEven(int value)
        {
            // i know i know it can be shortened

            if (value % 2 != 0) return false;

            return true;
        }

        public static byte[] HexDecimalToByteArray(string hex)
        {
            if (hex == null) return null;

            if (!IsEven(hex.Length)) return null;    

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static object CopyObject(object obj)
        {
            if (obj != null)
            {
                Type vType = obj.GetType();

                if (vType.IsArray)
                {
                    if (typeof(byte[]).IsAssignableFrom(vType.GetElementType()))
                    {
                        byte[] arr = (byte[])obj;
                        return GetNextByteSet(arr, 0, arr.Length);
                    }
                }
            }

            return obj;
        }


        /// <summary>
        /// Checks if an object can be converted to a particular type
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ConvertTo"></param>
        /// <returns></returns>
        public static bool ObjectCanConvert(object obj, Type ConvertTo)
        {
            if(obj == null) return false;   

            Type vType = obj.GetType();

            bool can = vType.IsAssignableFrom(ConvertTo);

            return can;
        }

        //doesnt work 
        //public static bool IsByteArray(object obj)
        //{
        //    if (obj != null)
        //    {
        //        Type vType = obj.GetType();

        //        if (vType.IsArray)
        //        {
        //            if (typeof(byte[]).IsAssignableFrom(vType.GetElementType()))
        //            {
        //                return true;
        //            }
        //            if (typeof(Byte[]).IsAssignableFrom(vType.GetElementType()))
        //            {
        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //}

        /// <summary>
        /// Checks if a string has numbers in it
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasNumbers(string value)
        {
            string hx = value.ToLower();

            for (int i = 0; i < hx.Length; i++)
            {
                if (Numeric.Contains(hx[i]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// tries to convert a string to an int
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int TryConvert(string val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {

            }

            return -1; //change to class package instead
        }
        /// <summary>
        /// Checks if a char is on the keyboard (c >= 32 && c <= 126)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsOnKeyboard(char c)
        {
            return (c >= 32 && c <= 126);
        }

        /// <summary>
        /// Checks if a string is alpha numeric characters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(string value) 
        {
            string hx = value.ToLower();

            for (int i = 0; i < hx.Length; i++)
            {
                if (!alphaNumeric.Contains(hx[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if a string contains only numbers
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool isNumeric(string value)
        {
            string hx = value.ToLower();

            for (int i = 0; i < hx.Length; i++)
            {
                if (!Numeric.Contains(hx[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if a string is convertable to hexadecimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsHexDecimal(string value)
        {
            string vals = new string(hxChars);

            string hx = value.ToLower();

            if(value.Length % 2 != 0)
            {
                return false;
            }

            for (int i = 0; i < hx.Length; i++)
            {
                if (!vals.Contains(hx[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Converts to a combination of hexadecimal and keyboard characters 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ConvertToReadable(byte[] data)
        {
            string val = "";

            for(int i = 0; i < data.Length;i++)
            {
                if (IsOnKeyboard((char)data[i]))
                    val += (char)data[i];
                else
                {
                    val += data[i].ToString("X2");
                }
            }

            return val;
        }
    }
}
