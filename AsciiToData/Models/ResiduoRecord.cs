using BinaryConverter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Models
{
    //public class ResiduoRecord : IBinaryWritable
    //{
    //    public short? n_barra {  get; set; }

    //    public float? l_ext {  get; set; }
    //    public float? l_int { get; set; }
    //    public float? ang_sx { get; set; }
    //    public float? ang_dx { get; set; }

    //    public string? rif { get; set; }

    //    public void WriteBinary(BinaryWriter writer)
    //    {
    //        writer.Write(n_barra.HasValue ? n_barra.Value : (short)0);

    //        writer.Write(l_ext.HasValue ? l_ext.Value : (float)0f);
    //        writer.Write(l_int.HasValue ? l_int.Value : (float)0f);
    //        writer.Write(ang_sx.HasValue ? ang_sx.Value : (float)0f);
    //        writer.Write(ang_dx.HasValue ? ang_dx.Value : (float)0f);

    //        WriteFixedString(writer, rif, 7+1);

    //    }

    //    private void WriteFixedString(BinaryWriter writer, string? value, int length)
    //    {
    //        // Se la stringa è null, considerala come string.Empty
    //        string fixedValue = (value ?? string.Empty);
    //        // Se la stringa è più lunga del limite, troncarla; altrimenti, pad con spazi
    //        fixedValue = fixedValue.Length > length
    //            ? fixedValue.Substring(0, length)
    //            : fixedValue.PadRight(length, '\0');
    //        byte[] bytes = System.Text.Encoding.Unicode.GetBytes(fixedValue);
    //        writer.Write(bytes);
    //    }

    //    public static ResiduoRecord ReadBinary(BinaryReader reader)
    //    {
    //        var record = new ResiduoRecord
    //        {
    //            n_barra = reader.ReadInt16(),

    //            l_ext = reader.ReadSingle(),
    //            l_int = reader.ReadSingle(),
    //            ang_sx = reader.ReadSingle(),
    //            ang_dx = reader.ReadSingle(),

    //            rif = ParserUtils.ReadFixedString(reader, 7+1)
    //        };
    //        return record;
    //    }
    //}

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct DATI_PEZZO_RESTANTE
    {
        public short n_barra;  // Chiave di corrispondenza
        public short dummy2;

        public float l_ext;
        public float l_int;
        public float angt_sx;
       // public float angp_sx;
        public float angt_dx;
        //public float angp_dx;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] dummy; // 8 byte di padding

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7 + 1)]
        public string rif;  // Riferimento del cliente per etichettare il riutilizzabile


        public static void WriteDatiResiduoRecord(BinaryWriter writer, DATI_PEZZO_RESTANTE record)
        {
            writer.Write(record.n_barra);
            writer.Write(record.dummy2);

            writer.Write(record.l_ext);
            writer.Write(record.l_int);
            writer.Write(record.angt_sx);
            //writer.Write(record.angp_sx);
            writer.Write(record.angt_dx);
            //writer.Write(record.angp_dx);

            writer.Write(record.dummy);

            WriteFixedString(writer, record.rif, 7+1);
        }

        static void WriteFixedString(BinaryWriter writer, string s, int fixedLength)
        {
            if (s == null)
                s = string.Empty;
            if (s.Length > fixedLength)
                s = s.Substring(0, fixedLength);
            for (int i = 0; i < fixedLength; i++)
            {
                char c = (i < s.Length) ? s[i] : '\0';
                writer.Write(c);  // Questo scrive 2 byte per char (UTF-16 LE)
            }


        }
    }
}
