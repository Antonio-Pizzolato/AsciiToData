using BinaryConverter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Models
{
    //public class LavoroRecord : IBinaryWritable
    //{
    //    public string? num_lav { get; set; }
    //    public string? stato_lav { get; set; }

    //    public void WriteBinary(System.IO.BinaryWriter writer)
    //    {
    //        WriteFixedString(writer, num_lav, 8+1);
    //        WriteFixedString(writer, stato_lav, 8+1);
    //    }

    //    private void WriteFixedString(System.IO.BinaryWriter writer, string? value, int length)
    //    {
    //        // Se la stringa è null, considerala come string.Empty
    //        string? fixedValue = (value ?? string.Empty);
    //        // Se la stringa è più lunga del limite, troncarla; altrimenti, pad con spazi
    //        fixedValue = fixedValue.Length > length
    //            ? fixedValue.Substring(0, length)
    //            : fixedValue.PadRight(length, '\0');
    //        byte[] bytes = System.Text.Encoding.Unicode.GetBytes(fixedValue);
    //        writer.Write(bytes);
    //        Console.WriteLine($"{num_lav},{stato_lav}");
    //    }

    //    // Metodo statico per la deserializzazione
    //    public static LavoroRecord ReadBinary(BinaryReader reader)
    //    {
    //        var record = new LavoroRecord
    //        {
    //            num_lav = ParserUtils.ReadFixedString(reader, 8+1),
    //            stato_lav = ParserUtils.ReadFixedString(reader, 8+1)
    //        };
    //        return record;
    //    }
    //}



    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DATI_LAVORO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8 + 1)]
        public string num_lav;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8 + 1)]
        public string stato_lav;


        public static void WriteDatiLavoroRecord(BinaryWriter writer, DATI_LAVORO record)
        {
            WriteFixedString(writer, record.num_lav, 8+1);
            WriteFixedString(writer, record.stato_lav, 8+1);
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
