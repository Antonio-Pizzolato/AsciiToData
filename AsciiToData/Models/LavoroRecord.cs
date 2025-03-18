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
    /// Rappresenta un record di lavoro (LAVORO) con due campi a lunghezza fissa.
    /// La struct è configurata per il marshalling sequenziale con Pack = 1.
    /// I campi stringa sono serializzati in UTF-16 LE, dove ogni char occupa 2 byte.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DATI_LAVORO
    {
        /// <summary>
        /// Numero del lavoro. Campo a lunghezza fissa (8+1 caratteri).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8 + 1)]
        public string num_lav;

        // <summary>
        /// Stato del lavoro. Campo a lunghezza fissa (8+1 caratteri).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8 + 1)]
        public string stato_lav;


        /// <summary>
        /// Scrive un record DATI_LAVORO in un BinaryWriter, campo per campo.
        /// Utilizza WriteFixedString per assicurare il padding corretto dei campi stringa.
        /// </summary>
        /// <param name="writer">Il BinaryWriter su cui scrivere il record.</param>
        /// <param name="record">Il record DATI_LAVORO da scrivere.</param>
        public static void WriteDatiLavoroRecord(BinaryWriter writer, DATI_LAVORO record)
        {
            // Scrive il campo num_lav con un padding fino a 9 caratteri (8+1)
            WriteFixedString(writer, record.num_lav, 8+1);
            // Scrive il campo stato_lav con un padding fino a 9 caratteri (8+1)
            WriteFixedString(writer, record.stato_lav, 8+1);
        }


        /// <summary>
        /// Scrive una stringa in formato UTF-16 LE come campo a lunghezza fissa.
        /// Se la stringa è più corta del numero di caratteri specificato, viene riempita con caratteri null ('\0').
        /// Se è più lunga, viene troncata.
        /// </summary>
        /// <param name="writer">Il BinaryWriter su cui scrivere.</param>
        /// <param name="s">La stringa da scrivere.</param>
        /// <param name="fixedLength">Il numero totale di caratteri da scrivere (compresi i caratteri di padding).</param>
        static void WriteFixedString(BinaryWriter writer, string s, int fixedLength)
        {
            // Se la stringa è null, utilizza string.Empty per evitare errori
            if (s == null)
                s = string.Empty;

            // Se la stringa supera la lunghezza fissa, viene troncata
            if (s.Length > fixedLength)
                s = s.Substring(0, fixedLength);

            // Scrive ogni carattere: se la stringa è più corta, scrive '\0' per le posizioni mancanti.
            for (int i = 0; i < fixedLength; i++)
            {
                char c = (i < s.Length) ? s[i] : '\0';
                // BinaryWriter.Write(char) scrive il carattere in formato UTF-16 LE (2 byte)
                writer.Write(c);
            }


        }

    }
}
