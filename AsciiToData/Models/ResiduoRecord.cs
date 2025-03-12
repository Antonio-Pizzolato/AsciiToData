using BinaryConverter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Models
{
    public class ResiduoRecord : IBinaryWritable
    {
        public short? n_barra {  get; set; }

        public float? l_ext {  get; set; }
        public float? l_int { get; set; }
        public float? ang_sx { get; set; }
        public float? ang_dx { get; set; }
        
        public string? rif { get; set; }

        public void WriteBinary(BinaryWriter writer)
        {
            writer.Write(n_barra.HasValue ? n_barra.Value : (short)0);

            writer.Write(l_ext.HasValue ? l_ext.Value : (float)0f);
            writer.Write(l_int.HasValue ? l_int.Value : (float)0f);
            writer.Write(ang_sx.HasValue ? ang_sx.Value : (float)0f);
            writer.Write(ang_dx.HasValue ? ang_dx.Value : (float)0f);

            WriteFixedString(writer, rif, 7);

        }

        private void WriteFixedString(BinaryWriter writer, string? value, int length)
        {
            // Se la stringa è null, considerala come string.Empty
            string fixedValue = (value ?? string.Empty);
            // Se la stringa è più lunga del limite, troncarla; altrimenti, pad con spazi
            fixedValue = fixedValue.Length > length
                ? fixedValue.Substring(0, length)
                : fixedValue.PadRight(length, ' ');
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(fixedValue);
            writer.Write(bytes);
        }

        public static ResiduoRecord ReadBinary(BinaryReader reader)
        {
            var record = new ResiduoRecord
            {
                n_barra = reader.ReadInt16(),

                l_ext = reader.ReadSingle(),
                l_int = reader.ReadSingle(),
                ang_sx = reader.ReadSingle(),
                ang_dx = reader.ReadSingle(),

                rif = ParserUtils.ReadFixedString(reader, 7)
            };
            return record;
        }
     }
}
