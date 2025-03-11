using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Utils
{
    public static class ParserUtils
    {
        public static string RemoveTrailingCommas(string input)
        {
            while (input.EndsWith(","))
            {
                input = input.TrimEnd(',');
            }
            return input;
        }

        public static short ParseShort(string input)
        {
            short.TryParse(input, out short value);
            return value;
        }

        public static float ParseFloat(string input)
        {
            float.TryParse(input, out float value);
            return value;
        }
    }
}
