using BinaryConverter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Models
{
    //public class PezzoRecord : IBinaryWritable
    //{
    //    public float? l_ext { get; set; }
    //    public float? l_int { get; set; }
    //    public float? angt_sx { get; set; }
    //    public float? angp_sx { get; set; }
    //    public float? angt_dx { get; set; }
    //    public float? angp_dx { get; set; }

    //    public short? num_pezzo { get; set; }
    //    public short? n_carrello { get; set; }
    //    public short? n_slot { get; set; }

    //    public long? n_unico_pezzo { get; set; }

    //    public string? id { get; set; }
    //    public string? tag_speciale { get; set; }
    //    public string? ordine { get; set; }
    //    public string? cliente { get; set; }

    //    public string? pian_trav1 { get; set; }
    //    public string? pian_trav2 { get; set; }
    //    public string? pian_trav3 { get; set; }

    //    public string? rinf { get; set; }
    //    public string? fissaggio { get; set; }
    //    public string? cod_tip { get; set; }

    //    public string? f_acqua1 { get; set; }
    //    public string? f_acqua2 { get; set; }
    //    public string? f_acqua3 { get; set; }

    //    public string? note { get; set; }

    //    public float? q1 { get; set; }
    //    public float? q2 { get; set; }
    //    public float? q3 { get; set; }
    //    public float? q4 { get; set; }

    //    public string? info1 { get; set; }
    //    public string? info2 { get; set; }
    //    public string? info3 { get; set; }
    //    public string? info4 { get; set; }
    //    public string? info5 { get; set; }


    //    public void WriteBinary (BinaryWriter writer)
    //    {
    //        writer.Write(l_ext.HasValue ? l_ext.Value : (float)0f);
    //        writer.Write(l_int.HasValue ? l_int.Value : (float)0f);
    //        writer.Write(angt_sx.HasValue ? angt_sx.Value : (float)0f);
    //        writer.Write(angp_sx.HasValue ? angp_sx.Value : (float)0f);
    //        writer.Write(angt_dx.HasValue ?  angt_dx.Value : (float)0f);
    //        writer.Write(angp_dx.HasValue ? angp_dx.Value : (float)0f);

    //        writer.Write(num_pezzo.HasValue ? num_pezzo.Value : (short)0);
    //        writer.Write(n_carrello.HasValue ? n_carrello.Value : (short)0);
    //        writer.Write(n_slot.HasValue ? n_slot.Value : (short)0);

    //        writer.Write(n_unico_pezzo.HasValue ? n_unico_pezzo.Value : (long)0);

    //        WriteFixedString(writer, id, 40+1);
    //        WriteFixedString(writer, tag_speciale, 4+1);
    //        WriteFixedString(writer, ordine, 7+1);
    //        WriteFixedString(writer, cliente, 30+1);

    //        WriteFixedString(writer, pian_trav1, 6+1);
    //        WriteFixedString(writer, pian_trav2, 6+1);
    //        WriteFixedString(writer, pian_trav3, 6+1);

    //        WriteFixedString(writer, rinf, 5+1);
    //        WriteFixedString(writer, fissaggio, 25+1);
    //        WriteFixedString(writer, cod_tip, 15+1);

    //        WriteFixedString(writer, f_acqua1, 4+1);
    //        WriteFixedString(writer, f_acqua2, 4+1);
    //        WriteFixedString(writer, f_acqua3, 4+1);

    //        WriteFixedString(writer, note, 30+1);

    //        writer.Write(q1.HasValue ? q1.Value : (float)0f);
    //        writer.Write(q2.HasValue ? q2.Value : (float)0f);
    //        writer.Write(q3.HasValue ? q3.Value : (float)0f);
    //        writer.Write(q4.HasValue ? q4.Value : (float)0f);

    //        WriteFixedString(writer, info1, 60+1);
    //        WriteFixedString(writer, info2, 60+1);
    //        WriteFixedString(writer, info3, 60+1);
    //        WriteFixedString(writer, info4, 60+1);
    //        WriteFixedString(writer, info5, 60+1);
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

    //    // Metodo statico per la deserializzazione
    //    public static PezzoRecord ReadBinary(BinaryReader reader)
    //    {
    //        var record = new PezzoRecord
    //        {
    //            l_ext = reader.ReadSingle(),
    //            l_int = reader.ReadSingle(),
    //            angt_sx = reader.ReadSingle(),
    //            angp_sx = reader.ReadSingle(),
    //            angt_dx = reader.ReadSingle(),
    //            angp_dx = reader.ReadSingle(),

    //            num_pezzo = reader.ReadInt16(),
    //            n_carrello = reader.ReadInt16(),
    //            n_slot = reader.ReadInt16(),

    //            n_unico_pezzo = reader.ReadInt64(),

    //            id = ParserUtils.ReadFixedString(reader, 40+1),
    //            tag_speciale = ParserUtils.ReadFixedString(reader,4+1),
    //            ordine = ParserUtils.ReadFixedString(reader, 7+1),
    //            cliente = ParserUtils.ReadFixedString(reader, 30+1),

    //            pian_trav1 = ParserUtils.ReadFixedString(reader, 6+1),
    //            pian_trav2 = ParserUtils.ReadFixedString(reader, 6+1),
    //            pian_trav3 = ParserUtils.ReadFixedString(reader, 6+1),

    //            rinf = ParserUtils.ReadFixedString(reader, 5+1),
    //            fissaggio = ParserUtils.ReadFixedString(reader, 15+1),
    //            cod_tip = ParserUtils.ReadFixedString(reader, 25+1),

    //            f_acqua1 = ParserUtils.ReadFixedString(reader, 4+1),
    //            f_acqua2 = ParserUtils.ReadFixedString(reader, 4+1),
    //            f_acqua3 = ParserUtils.ReadFixedString(reader, 4+1),

    //            note = ParserUtils.ReadFixedString(reader, 30+1),

    //            q1 = reader.ReadSingle(),
    //            q2 = reader.ReadSingle(),
    //            q3 = reader.ReadSingle(),
    //            q4 = reader.ReadSingle(),

    //            info1 = ParserUtils.ReadFixedString(reader, 60+1),
    //            info2 = ParserUtils.ReadFixedString(reader, 60+1),
    //            info3 = ParserUtils.ReadFixedString(reader, 60+1),
    //            info4 = ParserUtils.ReadFixedString(reader, 60+1),
    //            info5 = ParserUtils.ReadFixedString(reader, 60+1),
    //        };
    //        return record;
    //    }
    //}




    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DATI_PEZZO
    {
        public short num_pezzo;
        public short n_barra; // Chiave di corrispondenza

        public float angt_sx;
        //public float angp_sx;
        public float angt_dx;
        //public float angp_dx;

        public float l_ext;
        public float l_int;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40 + 1)]
        public string id;

        public short n_carrello;
        public short n_slot;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4 + 1)]
        public string tag_speciale;

        public uint n_unico_pezzo;

        public short n;
        public short n_lav;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7 + 1)]
        public string ordine;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30 + 1)]
        public string cliente;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6 + 1)]
        public string pian_trav1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6 + 1)]
        public string pian_trav2;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6 + 1)]
        public string pian_trav3;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5 + 1)]
        public string rinf;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25 + 1)]
        public string fissaggio;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15 + 1)]
        public string cod_tip;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4 + 1)]
        public string f_acqua1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4 + 1)]
        public string f_acqua2;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4 + 1)]
        public string f_acqua3;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30 + 1)]
        public string note;

        public short taglia_doppia;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] q;

        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60 + 1)]
        //public string info1;

        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60 + 1)]
        //public string info2;

        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60 + 1)]
        //public string info3;

        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60 + 1)]
        //public string info4;

        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60 + 1)]
        //public string info5;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] dummy; // Array di 20 byte di padding

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] pointers; // Array di 4 byte per puntatori




        public static void WriteDatiPezziRecord(BinaryWriter writer, DATI_PEZZO record)
        {

            writer.Write(record.num_pezzo);
            writer.Write(record.n_barra);

            writer.Write(record.angt_sx);
            //writer.Write(record.angp_sx);
            writer.Write(record.angt_dx);
            //writer.Write(record.angp_dx);

            writer.Write(record.l_ext);
            writer.Write(record.l_int);

            WriteFixedString(writer, record.id, 40 + 1);//52 (2=1) //54 io

            writer.Write(record.n_carrello);
            writer.Write(record.n_slot);

            WriteFixedString(writer, record.tag_speciale, 4 + 1);

            writer.Write(record.n_unico_pezzo);

            writer.Write(record.n);
            writer.Write(record.n_lav);

            WriteFixedString(writer, record.ordine, 7 + 1);
            WriteFixedString(writer, record.cliente, 30 + 1);
            WriteFixedString(writer, record.pian_trav1, 6 + 1);
            WriteFixedString(writer, record.pian_trav2, 6 + 1);
            WriteFixedString(writer, record.pian_trav3, 6 + 1);
            WriteFixedString(writer, record.rinf, 5 + 1);
            WriteFixedString(writer, record.fissaggio, 25 + 1);
            WriteFixedString(writer, record.cod_tip, 15 + 1);
            WriteFixedString(writer, record.f_acqua1, 4 + 1);
            WriteFixedString(writer, record.f_acqua2, 4 + 1);
            WriteFixedString(writer, record.f_acqua3, 4 + 1);
            WriteFixedString(writer, record.note, 30 + 1);

            writer.Write(record.taglia_doppia);
            writer.Write(record.taglia_doppia);

            foreach (float val in record.q)
                writer.Write(val);

            //WriteFixedString(writer, record.info1, 60 + 1);
            //WriteFixedString(writer, record.info2, 60 + 1);
            //WriteFixedString(writer, record.info3, 60 + 1);
            //WriteFixedString(writer, record.info4, 60 + 1);
            //WriteFixedString(writer, record.info5, 60 + 1);

            writer.Write(record.dummy);
            writer.Write(record.pointers);
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




            //public static void WriteFilePez(List<DATI_PEZZO> listaPezzi, string outputPath)
            //{
            //        using (FileStream fs = new FileStream(outputPath, FileMode.Create))
            //        using (BinaryWriter writer = new BinaryWriter(fs, Encoding.Unicode))
            //        {
            //            //writer.Write((ushort)0xFEFF);
            //            foreach (var pez in listaPezzi)
            //            {
            //                byte[] recordBytes = BinaryFileWriter.StructureToByteArray(pez);
            //                writer.Write(recordBytes);
            //            }
            //        }
            //}


        }

    }

}