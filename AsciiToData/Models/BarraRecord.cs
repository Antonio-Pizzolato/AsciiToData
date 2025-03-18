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
    /// <summary>
    /// Rappresenta un record di "Barra" (tipo B).
    /// La struct è configurata per il marshalling in formato sequenziale (Pack = 1) 
    /// e per la codifica Unicode (UTF-16 LE) per le stringhe a lunghezza fissa.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct DATI_BARRA
    {
        /// <summary>
        /// Numero della barra.
        /// </summary>
        public short num_barra;

        /// <summary>
        /// Codice profilo (17 caratteri fissi).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string cod_prof;

        /// <summary>
        /// Spessore.
        /// </summary>
        public float spess;

        /// <summary>
        /// Colore (9 caratteri fissi).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string col;

        /// <summary>
        /// Descrizione (21 caratteri fissi).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string descr;

        /// <summary>
        /// Campo di padding o dati extra.
        /// </summary>
        public short dummy2;
        public short dummy3;
        /// <summary>
        /// Lunghezza standard.
        /// </summary>
        public float lung_standard;
        /// <summary>
        /// Tipo di barra.
        /// </summary>
        public short tipo;
        /// <summary>
        /// Numero di barre da visualizzare assieme.
        /// </summary>
        public short vis_nb_ass;
        /// <summary>
        /// Numero di barre assieme.
        /// </summary>
        public short nbarra_ass;
        /// <summary>
        /// Flag per tagliata.
        /// </summary>
        public short tagliata;

        /// <summary>
        /// Riferimento (8 caratteri fissi).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string rif;

        /// <summary>
        /// Array di 32 byte (padding o dati extra).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] dummy;



        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string dp;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string pr;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string next_barra;




        /// <summary>
        /// Metodo statico per scrivere un record DATI_BARRA in un BinaryWriter.
        /// I campi vengono scritti in sequenza, assicurando che ogni campo stringa venga "padded"
        /// al numero fisso di caratteri richiesto.
        /// </summary>
        /// <param name="writer">Il BinaryWriter su cui scrivere.</param>
        /// <param name="record">Il record DATI_BARRA da scrivere.</param>
        public static void WriteDatiBarreRecord(BinaryWriter writer, DATI_BARRA record)
        {
            // Campo short: num_barra
            writer.Write(record.num_barra);

            // Campo stringa: cod_prof (lunghezza fissa, 17 caratteri)
            WriteFixedString(writer, record.cod_prof, 17);

            // Campo float: spess
            writer.Write(record.spess);

            // Campo stringa: col (lunghezza fissa, 9 caratteri)
            WriteFixedString(writer, record.col, 9);

            // Campo stringa: descr (lunghezza fissa, 21 caratteri)
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

            // Campo stringa: rif (lunghezza fissa, 8 caratteri)
            WriteFixedString(writer, record.rif, 8);

            // Campo array di byte: dummy (32 byte)
            writer.Write(record.dummy);

            // Campi stringa per i puntatori (dp, pr, next_barra) – lunghezza fissa 4 caratteri
            WriteFixedString(writer, record.dp, 2);
            WriteFixedString(writer, record.pr, 2);
            WriteFixedString(writer, record.next_barra, 2);
        }

        /// <summary>
        /// Metodo helper che scrive una stringa in un BinaryWriter come campo a lunghezza fissa in UTF-16 LE.
        /// Se la stringa è più corta, vengono scritti caratteri null ('\0') per completare la lunghezza.
        /// Se la stringa è troppo lunga, viene troncata.
        /// Ogni carattere viene scritto come 2 byte.
        /// </summary>
        /// <param name="writer">Il BinaryWriter su cui scrivere la stringa.</param>
        /// <param name="s">La stringa da scrivere.</param>
        /// <param name="fixedLength">Il numero totale di caratteri da scrivere (incluso il padding).</param>
        static void WriteFixedString(BinaryWriter writer, string s, int fixedLength)
        {
            // Se la stringa è null, la sostituisce con string.Empty
            if (s == null)
                s = string.Empty;

            // Se la stringa supera la lunghezza fissa, la tronca
            if (s.Length > fixedLength)
                s = s.Substring(0, fixedLength);

            // Itera per il numero fisso di caratteri
            for (int i = 0; i < fixedLength; i++)
            {
                // Se la stringa contiene un carattere in questa posizione, lo scrive; altrimenti scrive '\0'
                char c = (i < s.Length) ? s[i] : '\0';

                // Scrive il carattere in UTF-16 LE (2 byte per char)
                writer.Write(c);
            }


        }
    }

}
