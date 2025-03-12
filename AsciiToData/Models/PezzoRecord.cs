using BinaryConverter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Models
{
    public class PezzoRecord : IBinaryWritable
    {
        public float? l_ext { get; set; }
        public float? l_int { get; set; }
        public float? angt_sx { get; set; }
        public float? angp_sx { get; set; }
        public float? angt_dx { get; set; }
        public float? angp_dx { get; set; }

        public short? num_pezzo { get; set; }
        public short? n_carrello { get; set; }
        public short? n_slot { get; set; }

        public long? n_unico_pezzo { get; set; }

        public string? id { get; set; }
        public string? tag_speciale { get; set; }
        public string? ordine { get; set; }
        public string? cliente { get; set; }

        public string? pian_trav1 { get; set; }
        public string? pian_trav2 { get; set; }
        public string? pian_trav3 { get; set; }

        public string? rinf { get; set; }
        public string? fissaggio { get; set; }
        public string? cod_tip { get; set; }

        public string? f_acqua1 { get; set; }
        public string? f_acqua2 { get; set; }
        public string? f_acqua3 { get; set; }

        public string? note { get; set; }

        public float? q1 { get; set; }
        public float? q2 { get; set; }
        public float? q3 { get; set; }
        public float? q4 { get; set; }

        public string? info1 { get; set; }
        public string? info2 { get; set; }
        public string? info3 { get; set; }
        public string? info4 { get; set; }
        public string? info5 { get; set; }


        public void WriteBinary (BinaryWriter writer)
        {
            writer.Write(l_ext.HasValue ? l_ext.Value : (float)0f);
            writer.Write(l_int.HasValue ? l_int.Value : (float)0f);
            writer.Write(angt_sx.HasValue ? angt_sx.Value : (float)0f);
            writer.Write(angp_sx.HasValue ? angp_sx.Value : (float)0f);
            writer.Write(angt_dx.HasValue ?  angt_dx.Value : (float)0f);
            writer.Write(angp_dx.HasValue ? angp_dx.Value : (float)0f);

            writer.Write(num_pezzo.HasValue ? num_pezzo.Value : (short)0);
            writer.Write(n_carrello.HasValue ? n_carrello.Value : (short)0);
            writer.Write(n_slot.HasValue ? n_slot.Value : (short)0);

            writer.Write(n_unico_pezzo.HasValue ? n_unico_pezzo.Value : (long)0);

            WriteFixedString(writer, id, 40);
            WriteFixedString(writer, tag_speciale, 4);
            WriteFixedString(writer, ordine, 7);
            WriteFixedString(writer, cliente, 30);

            WriteFixedString(writer, pian_trav1, 6);
            WriteFixedString(writer, pian_trav2, 6);
            WriteFixedString(writer, pian_trav3, 6);

            WriteFixedString(writer, rinf, 5);
            WriteFixedString(writer, fissaggio, 25);
            WriteFixedString(writer, cod_tip, 15);

            WriteFixedString(writer, f_acqua1, 4);
            WriteFixedString(writer, f_acqua2, 4);
            WriteFixedString(writer, f_acqua3, 4);

            WriteFixedString(writer, note, 30);

            writer.Write(q1.HasValue ? q1.Value : (float)0f);
            writer.Write(q2.HasValue ? q2.Value : (float)0f);
            writer.Write(q3.HasValue ? q3.Value : (float)0f);
            writer.Write(q4.HasValue ? q4.Value : (float)0f);

            WriteFixedString(writer, info1, 60);
            WriteFixedString(writer, info2, 60);
            WriteFixedString(writer, info3, 60);
            WriteFixedString(writer, info4, 60);
            WriteFixedString(writer, info5, 60);
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
        public static PezzoRecord ReadBinary(BinaryReader reader)
        {
            var record = new PezzoRecord
            {
                l_ext = reader.ReadSingle(),
                l_int = reader.ReadSingle(),
                angt_sx = reader.ReadSingle(),
                angp_sx = reader.ReadSingle(),
                angt_dx = reader.ReadSingle(),
                angp_dx = reader.ReadSingle(),

                num_pezzo = reader.ReadInt16(),
                n_carrello = reader.ReadInt16(),
                n_slot = reader.ReadInt16(),

                n_unico_pezzo = reader.ReadInt64(),

                id = ParserUtils.ReadFixedString(reader, 40),
                tag_speciale = ParserUtils.ReadFixedString(reader,4),
                ordine = ParserUtils.ReadFixedString(reader, 7),
                cliente = ParserUtils.ReadFixedString(reader, 30),

                pian_trav1 = ParserUtils.ReadFixedString(reader, 6),
                pian_trav2 = ParserUtils.ReadFixedString(reader, 6),
                pian_trav3 = ParserUtils.ReadFixedString(reader, 6),

                rinf = ParserUtils.ReadFixedString(reader, 5),
                fissaggio = ParserUtils.ReadFixedString(reader, 15),
                cod_tip = ParserUtils.ReadFixedString(reader, 25),

                f_acqua1 = ParserUtils.ReadFixedString(reader, 4),
                f_acqua2 = ParserUtils.ReadFixedString(reader, 4),
                f_acqua3 = ParserUtils.ReadFixedString(reader, 4),

                note = ParserUtils.ReadFixedString(reader, 30),

                q1 = reader.ReadSingle(),
                q2 = reader.ReadSingle(),
                q3 = reader.ReadSingle(),
                q4 = reader.ReadSingle(),

                info1 = ParserUtils.ReadFixedString(reader, 60),
                info2 = ParserUtils.ReadFixedString(reader, 60),
                info3 = ParserUtils.ReadFixedString(reader, 60),
                info4 = ParserUtils.ReadFixedString(reader, 60),
                info5 = ParserUtils.ReadFixedString(reader, 60),
            };
            return record;
        }
    }
}
