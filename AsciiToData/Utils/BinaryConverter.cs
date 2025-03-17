using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Utils
{
    public static class BinaryConverter
    {
        ///// <summary>
        ///// Converte un valore short in array di byte (formato binario)
        ///// </summary>
        ///// <param name="value">Valore short da convertire</param>
        ///// <returns>Array di byte corrispondente</returns>
        //public static byte[] ConvertShortToBytes(short value)
        //{
        //    // Metodo 1: Utilizzo di BitConverter
        //    return BitConverter.GetBytes(value);

        //    // Metodo 2: Manuale
        //    /*
        //    byte[] result = new byte[2]; // short = 2 byte
        //    result[0] = (byte)(value & 0xFF);         // byte meno significativo
        //    result[1] = (byte)((value >> 8) & 0xFF);  // byte più significativo
        //    return result;
        //    */
        //}

        ///// <summary>
        ///// Converte un valore float in array di byte (formato binario)
        ///// </summary>
        ///// <param name="value">Valore float da convertire</param>
        ///// <returns>Array di byte corrispondente</returns>
        //public static byte[] ConvertFloatToBytes(float value)
        //{
        //    return BitConverter.GetBytes(value);
        //}

        ///// <summary>
        ///// Converte un valore long in array di byte (formato binario)
        ///// </summary>
        ///// <param name="value">Valore long da convertire</param>
        ///// <returns>Array di byte corrispondente</returns>
        //public static byte[] ConvertLongToBytes(long value)
        //{
        //    return BitConverter.GetBytes(value);
        //}

        ///// <summary>
        ///// Converte un valore double in array di byte (formato binario)
        ///// </summary>
        ///// <param name="value">Valore double da convertire</param>
        ///// <returns>Array di byte corrispondente</returns>
        //public static byte[] ConvertDoubleToBytes(double value)
        //{
        //    return BitConverter.GetBytes(value);
        //}

        ///// <summary>
        ///// Converte una stringa in array di byte (formato binario)
        ///// </summary>
        ///// <param name="value">Stringa da convertire</param>
        ///// <param name="encoding">Encoding da utilizzare (default UTF-8)</param>
        ///// <returns>Array di byte corrispondente</returns>
        //public static byte[] ConvertStringToBytes(string value, Encoding encoding = null)
        //{
        //    if (encoding == null)
        //        encoding = Encoding.UTF8;

        //    return encoding.GetBytes(value);
        //}




        public static byte[] GetUtf16FixedString(string input, int maxChars)
        {
            // Convertiamo direttamente la stringa in byte UTF-16 LE
            byte[] stringBytes = Encoding.Unicode.GetBytes(input);

            // Numero massimo di byte (ogni carattere UTF-16 = 2 byte)
            int maxBytes = maxChars * 2;

            if (stringBytes.Length > maxBytes)
            {
                // Se la stringa è troppo lunga, la tronchiamo ai primi maxBytes byte
                Array.Resize(ref stringBytes, maxBytes);
            }
            else if (stringBytes.Length < maxBytes)
            {
                // Se è più corta, aggiungiamo \0\0 fino a riempire il campo
                Array.Resize(ref stringBytes, maxBytes);
            }

            return stringBytes;
        }

    }
}
