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
    /// Rappresenta un record residuo (tipo R) per la conversione in file binari.
    /// La struct è definita in modo sequenziale (Pack = 1) e utilizza il CharSet Unicode per le stringhe.
    /// Le stringhe a lunghezza fissa vengono serializzate in UTF-16 LE (ogni char occupa 2 byte).
    /// </summary>
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



        /// <summary>
        /// Scrive un record DATI_PEZZO_RESTANTE in un BinaryWriter.
        /// I campi vengono scritti in sequenza nel formato binario specificato.
        /// Le stringhe a lunghezza fissa vengono gestite con padding (con caratteri null) se necessario.
        /// </summary>
        /// <param name="writer">Il BinaryWriter su cui scrivere il record.</param>
        /// <param name="record">Il record DATI_PEZZO_RESTANTE da scrivere.</param>
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

        /// <summary>
        /// Metodo helper che scrive una stringa in un BinaryWriter come campo a lunghezza fissa in UTF-16 LE.
        /// Se la stringa è più corta, viene completata con caratteri null ('\0'); se è troppo lunga, viene troncata.
        /// Ogni carattere viene scritto come 2 byte.
        /// </summary>
        /// <param name="writer">Il BinaryWriter su cui scrivere la stringa.</param>
        /// <param name="s">La stringa da scrivere.</param>
        /// <param name="fixedLength">Il numero totale di caratteri da scrivere (incluso il padding).</param>
        static void WriteFixedString(BinaryWriter writer, string s, int fixedLength)
        {
            // Se la stringa è null, assegna string.Empty per evitare eccezioni
            if (s == null)
                s = string.Empty;

            // Se la stringa supera la lunghezza fissa, la tronca
            if (s.Length > fixedLength)
                s = s.Substring(0, fixedLength);

            // Scrive ogni carattere; se non ci sono abbastanza caratteri, scrive '\0' per completare la lunghezza
            for (int i = 0; i < fixedLength; i++)
            {
                // Se esiste un carattere in questa posizione, lo scrive; altrimenti, scrive il carattere null
                char c = (i < s.Length) ? s[i] : '\0';

                // BinaryWriter.Write(char) scrive il carattere in UTF-16 LE (2 byte per char)
                writer.Write(c);
            }


        }
    }
}
