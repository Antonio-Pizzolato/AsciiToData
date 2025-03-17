using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Utils
{
    public static class ParserUtils
    {
        public static string RemoveTrailingCommas(string input)
        {
            //while (input.EndsWith(","))
            //{
            //    input = input.TrimEnd(',');
            //}
            return input;
        }

        // Metodi helper per la sicurezza
        public static string SafeGet(string[] fields, int index)
        {
            return fields.Length > index ? fields[index] : string.Empty;
        }

        public static short SafeParseShort(string[] fields, int index, short defaultValue = 0)
        {
            string? rawValue = SafeGet(fields, index).Trim();
            return short.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out short result)
                ? result
                : defaultValue;
        }

        public static float SafeParseFloat(string[] fields, int index, float defaultValue = 0.0f)
        {
            string? rawValue = SafeGet(fields, index).Trim();
            return float.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out float result)
                ? result
                : defaultValue;

        }

        public static ulong SafeParseULong(string[] fields, int index, ulong defaultValue = 0)
        {
            string? rawValue = SafeGet(fields, index).Trim();
            return ulong.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out ulong result) 
                ? result 
                : defaultValue;
        }

        // Metodo per leggere una stringa fissa dal BinaryReader
        //public static string ReadFixedString(BinaryReader reader, int length)
        //{
        //    byte[] bytes = reader.ReadBytes(length);
        //    return Encoding.ASCII.GetString(bytes).Trim();
        //}
    }
}
