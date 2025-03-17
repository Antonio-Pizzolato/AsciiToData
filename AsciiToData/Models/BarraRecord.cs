using BinaryConverter.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Models
{
    //public class BarraRecord : IBinaryWritable
    //{
    //    public string? cod_prof { get; set; }
    //    public string? col { get; set; }
    //    public short? nbarra_ass { get; set; }
    //    public float? lung_standard { get; set; }
    //    public string? rif1 { get; set; }
    //    public string? rif2 { get; set; }
    //    public short? slats_ass { get; set; }
    //    public float? spess {  get; set; }


    //    public void WriteBinary(BinaryWriter writer)
    //    {
    //        // Supponiamo lunghezza fissa 8 per CodProf e 8 per Col
    //        WriteFixedString(writer, cod_prof, 8 + 1);
    //        WriteFixedString(writer, col, 8 + 1);
    //        if (nbarra_ass.Value is >= 0 and <= 2)
    //            writer.Write(nbarra_ass.HasValue ? nbarra_ass.Value : (short)0);
    //        else Console.WriteLine("Numero barre assieme troppo alto");
    //        writer.Write(lung_standard.HasValue ? lung_standard.Value : (float)0f);
    //        WriteFixedString(writer, rif1, 20 + 1);
    //        WriteFixedString(writer, rif2, 20 + 1);
    //        writer.Write(slats_ass.HasValue ? slats_ass.Value : (short)0);
    //        writer.Write(spess.HasValue ? spess.Value : (float)0f);
    //    }

    //    //public void WriteBinary(BinaryWriter writer)
    //    //{
    //    //    writer.Write(BinaryConverter.Utils.BinaryConverter.ConvertStringToBytes(cod_prof));
    //    //    writer.Write(BinaryConverter.Utils.BinaryConverter.ConvertStringToBytes(col));
    //    //    writer.Write(BinaryConverter.Utils.BinaryConverter.ConvertShortToBytes(nbarra_ass));
    //    //    writer.Write(BinaryConverter.Utils.BinaryConverter.ConvertFloatToBytes(lung_standard));
    //    //    writer.Write(BinaryConverter.Utils.BinaryConverter.ConvertStringToBytes(rif1));
    //    //    writer.Write(BinaryConverter.Utils.BinaryConverter.ConvertStringToBytes(rif2));
    //    //    writer.Write(BinaryConverter.Utils.BinaryConverter.ConvertShortToBytes(slats_ass));
    //    //    writer.Write(BinaryConverter.Utils.BinaryConverter.ConvertFloatToBytes(spess));
    //    //}

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
    //    public static BarraRecord ReadBinary(BinaryReader reader)
    //    {
    //        var record = new BarraRecord
    //        {
    //            cod_prof = ParserUtils.ReadFixedString(reader, 8+1),
    //            col = ParserUtils.ReadFixedString(reader, 8+1),
    //            nbarra_ass = reader.ReadInt16(),
    //            lung_standard = reader.ReadSingle(),
    //            rif1 = ParserUtils.ReadFixedString(reader, 20+1),
    //            rif2 = ParserUtils.ReadFixedString(reader, 20+1),
    //            slats_ass = reader.ReadInt16(),
    //            spess = reader.ReadSingle(),
    //        };
    //        return record;
    //    }
    //}



    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DATI_BARRA
    {
        public short num_barra;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string cod_prof;

        public float spess;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string col;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string descr;

        public short dummy2;
        public short dummy3;
        public float lung_standard;
        public short tipo;
        public short vis_nb_ass;
        public short nbarra_ass;
        public short tagliata;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string rif;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] dummy;



        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string dp;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string pr;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string next_barra;





        public static void WriteDatiBarreRecord(BinaryWriter writer, DATI_BARRA record)
        {
            // Campo short: num_barra
            writer.Write(record.num_barra);

            // Campo stringa: cod_prof (lunghezza fissa, es. 17 caratteri)
            WriteFixedString(writer, record.cod_prof, 17);

            // Campo float: spess
            writer.Write(record.spess);

            // Campo stringa: col (lunghezza fissa, es. 9 caratteri)
            WriteFixedString(writer, record.col, 9);

            // Campo stringa: descr (lunghezza fissa, es. 21 caratteri)
            WriteFixedString(writer, record.descr, 21);

            // Campo short: dummy2
            writer.Write(record.dummy2);

            // Campo short: dummy3
            writer.Write(record.dummy3);

            // Campo float: lung_standard
            writer.Write(record.lung_standard);

            // Campo short: tipo
            writer.Write(record.tipo);

            // Campo short: vis_nb_ass
            writer.Write(record.vis_nb_ass);

            // Campo short: nbarra_ass
            writer.Write(record.nbarra_ass);

            // Campo short: tagliata
            writer.Write(record.tagliata);

            // Campo stringa: rif (lunghezza fissa, es. 8 caratteri)
            WriteFixedString(writer, record.rif, 8);

            // Campo array di byte: dummy (32 byte)
            writer.Write(record.dummy);

            // Campi stringa per i puntatori (dp, pr, next_barra) – ad es. lunghezza fissa 4 caratteri
            WriteFixedString(writer, record.dp, 4);
            WriteFixedString(writer, record.pr, 4);
            WriteFixedString(writer, record.next_barra, 4);
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





            //public static void WriteFileBar(List<DATI_BARRA> listaBarre, string outputPath)
            //{
            //    using (FileStream fs = new FileStream(outputPath, FileMode.Create))
            //    using (BinaryWriter writer = new BinaryWriter(fs, Encoding.Unicode))
            //    {
            //        // Se vuoi aggiungere un BOM all'inizio:
            //        //writer.Write((ushort)0xFEFF);

            //        foreach (var barra in listaBarre)
            //        {
            //            byte[] recordBytes = BinaryFileWriter.StructureToByteArray(barra);
            //            writer.Write(recordBytes);
            //        }
            //    }

            //}



        }
    }

}
