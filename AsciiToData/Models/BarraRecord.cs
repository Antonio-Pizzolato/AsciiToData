using BinaryConverter.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Models
{
    public class BarraRecord : IBinaryWritable
    {
        public string? cod_prof { get; set; }
        public string? col { get; set; }
        public short? nbarra_ass { get; set; }
        public float? lung_standard { get; set; }
        public string? rif1 { get; set; }
        public string? rif2 { get; set; }
        public short? slats_ass { get; set; }
        public float? spess {  get; set; }


        public void WriteBinary(BinaryWriter writer)
        {
            // Supponiamo lunghezza fissa 8 per CodProf e 8 per Col
            WriteFixedString(writer, cod_prof, 8);
            WriteFixedString(writer, col, 8);
            writer.Write(nbarra_ass.HasValue ? nbarra_ass.Value : (short)0);
            writer.Write(lung_standard.HasValue ? lung_standard.Value : (float)0f);
            WriteFixedString(writer, rif1, 20);
            WriteFixedString(writer, rif2 , 20);
            writer.Write(slats_ass.HasValue ? slats_ass.Value : (short)0);
            writer.Write(spess.HasValue ? spess.Value : (float)0f);
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

        // Metodo statico per la deserializzazione
        public static BarraRecord ReadBinary(BinaryReader reader)
        {
            var record = new BarraRecord
            {
                cod_prof = ParserUtils.ReadFixedString(reader, 8),
                col = ParserUtils.ReadFixedString(reader, 8),
                nbarra_ass = reader.ReadInt16(),
                lung_standard = reader.ReadSingle(),
                rif1 = ParserUtils.ReadFixedString(reader, 20),
                rif2 = ParserUtils.ReadFixedString(reader, 20),
                slats_ass = reader.ReadInt16(),
                spess = reader.ReadSingle(),
            };
            return record;
        }
    }
}
