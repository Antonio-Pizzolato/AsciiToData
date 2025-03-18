using BinaryConverter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Models
{
    /// <summary>
    /// Rappresenta un record di tipo "Pezzo" (P) per la conversione in file binari.
    /// La struct è disposta in memoria in modo sequenziale (Pack = 1) e utilizza campi stringa a lunghezza fissa.
    /// I campi stringa sono serializzati in UTF-16 LE, dove ogni char occupa 2 byte.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DATI_PEZZO
    {
        public short num_pezzo;
        public short n_barra;

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



        /// <summary>
        /// Scrive il record DATI_PEZZO in un BinaryWriter, campo per campo, nel formato binario specificato.
        /// I campi stringa vengono scritti come stringhe a lunghezza fissa in UTF-16 LE, utilizzando il padding con '\0' se necessario.
        /// </summary>
        /// <param name="writer">Il BinaryWriter su cui scrivere il record.</param>
        /// <param name="record">Il record DATI_PEZZO da scrivere.</param>
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

        /// <summary>
        /// Metodo helper che scrive una stringa in un BinaryWriter come campo a lunghezza fissa in UTF-16 LE.
        /// Se la stringa è più corta, viene riempita con caratteri null ('\0'); se è troppo lunga, viene troncata.
        /// Ogni carattere viene scritto come 2 byte.
        /// </summary>
        /// <param name="writer">Il BinaryWriter su cui scrivere la stringa.</param>
        /// <param name="s">La stringa da scrivere.</param>
        /// <param name="fixedLength">Il numero totale di caratteri da scrivere (incluso il padding).</param>
        static void WriteFixedString(BinaryWriter writer, string s, int fixedLength)
        {
            if (s == null)
                s = string.Empty;
            if (s.Length > fixedLength)
                s = s.Substring(0, fixedLength);

            // Itera per il numero fisso di caratteri, scrivendo ogni carattere o un carattere null se la stringa è più corta
            for (int i = 0; i < fixedLength; i++)
            {
                char c = (i < s.Length) ? s[i] : '\0';
                writer.Write(c);  // Scrive il carattere in formato UTF-16 LE (2 byte per char)
            }


        }

    }

}