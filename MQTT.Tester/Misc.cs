using System;

namespace CoAP.Tester
{
    delegate void Action();
    delegate void Action<TArg1, TArg2>(TArg1 arg1, TArg2 arg2);

    static class Tuple
    {
        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new Tuple<T1, T2>(item1, item2);
        }
    }

    sealed class Tuple<T1, T2>
    {
        public T1 Item1 { get; private set; }
        public T2 Item2 { get; private set; }
        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }

    class Utils
    {
        public static Byte[] ToBytes(String hex)
        {
            try
            {
                if (hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    hex = hex.Remove(0, 2);
                if (hex.Length % 2 != 0)
                    hex = "0" + hex;
                //int byteLen = (hex.Length / 2) + (hex.Length % 2 == 0 ? 0 : 1);
                Byte[] tmp = new Byte[hex.Length / 2];
                //Byte[] tmp = new Byte[byteLen];
                for (Int32 i = 0, j = 0; i < hex.Length; i += 2)
                {
                    Int16 high = Int16.Parse(hex[i].ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
                    Int16 low = Int16.Parse(hex[i + 1].ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
                    tmp[j++] = Convert.ToByte(high * 16 + low);
                }

                return tmp;
            }
            catch { return null; }
        }

        public static String ToHexString(Byte[] data)
        {
            return ToHexString(data, 0, data.Length);
        }

        public static String ToHexString(Byte[] data, Int32 offset, Int32 length)
        {
            if (data == null)
                return String.Empty;
            else
                return "0x" + BitConverter.ToString(data, offset, length).Replace("-", "");
        }
    }
}
