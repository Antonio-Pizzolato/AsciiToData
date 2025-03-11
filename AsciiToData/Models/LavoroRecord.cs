using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Models
{
    public class LavoroRecord : IBinaryWritable
    {
        public string? num_lav { get; set; }
        public string? stato_lav { get; set; }

        public void WriteBinary(System.IO.BinaryWriter writer)
        {
            WriteFixedString(writer, num_lav, 8);
            WriteFixedString(writer, num_lav, 8);
        }

        private void WriteFixedString(System.IO.BinaryWriter writer, string value, int length)
        {
            // Se il campo è più corto, lo padderemo con spazi; se più lungo, lo troncchiamo
            var fixedValue = value.Length > length ? value.Substring(0, length) : value.PadRight(length, ' ');
            var bytes = Encoding.ASCII.GetBytes(fixedValue);
            writer.Write(bytes);
        }
    }
}
