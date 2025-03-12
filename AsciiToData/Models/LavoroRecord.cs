using BinaryConverter.Utils;
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
            WriteFixedString(writer, stato_lav, 8);
        }

        private void WriteFixedString(System.IO.BinaryWriter writer, string? value, int length)
        {
            // Se la stringa è null, considerala come string.Empty
            string fixedValue = (value ?? string.Empty);
            // Se la stringa è più lunga del limite, troncarla; altrimenti, pad con spazi
            fixedValue = fixedValue.Length > length
                ? fixedValue.Substring(0, length)
                : fixedValue.PadRight(length, ' ');
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(fixedValue);
            writer.Write(bytes);
            Console.WriteLine($"{num_lav},{stato_lav}");
        }

        // Metodo statico per la deserializzazione
        public static LavoroRecord ReadBinary(BinaryReader reader)
        {
            var record = new LavoroRecord
            {
                num_lav = ParserUtils.ReadFixedString(reader, 8),
                stato_lav = ParserUtils.ReadFixedString(reader, 8)
            };
            return record;
        }
    }
}
