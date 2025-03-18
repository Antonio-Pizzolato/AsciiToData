using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Utils
{

    /// <summary>
    /// Fornisce metodi helper per l'estrazione sicura dei valori da un array di stringhe e il loro parsing nei rispettivi tipi.
    /// Questi metodi sono utili per gestire input da file di testo e garantire che, in caso di errori, venga restituito un valore di default.
    /// </summary>
    public static class ParserUtils
    {


        /// <summary>
        /// Recupera in modo sicuro il valore in maiuscolo dall'array di stringhe all'indice specificato.
        /// Se l'indice non esiste, restituisce una stringa vuota.
        /// </summary>
        /// <param name="fields">Array di stringhe da cui estrarre il valore.</param>
        /// <param name="index">Indice dell'array da leggere.</param>
        /// <returns>Il valore in maiuscolo se presente, altrimenti string.Empty.</returns>
        public static string SafeGet(string[] fields, int index)
        {
            return fields.Length > index ? fields[index].ToUpper() : string.Empty;
        }



        /// <summary>
        /// Effettua il parsing sicuro di un valore short da un array di stringhe.
        /// Se il parsing fallisce, restituisce il valore di default specificato (0 se non diversamente indicato).
        /// </summary>
        /// <param name="fields">Array di stringhe contenente il valore da convertire.</param>
        /// <param name="index">Indice dell'array in cui si trova il valore.</param>
        /// <param name="defaultValue">Valore di default da restituire in caso di fallimento del parsing.</param>
        /// <returns>Il valore short parsato oppure il defaultValue.</returns>
        public static short SafeParseShort(string[] fields, int index, short defaultValue = 0)
        {
            // Ottiene il valore grezzo, lo trimma e lo converte in maiuscolo
            string? rawValue = SafeGet(fields, index).Trim();

            // Prova ad effettuare il parsing in short usando le impostazioni culturali invariant
            return short.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out short result)
                ? result
                : defaultValue;
        }


        /// <summary>
        /// Effettua il parsing sicuro di un valore float da un array di stringhe.
        /// Se il parsing fallisce, restituisce il valore di default specificato (0.0f se non diversamente indicato).
        /// </summary>
        /// <param name="fields">Array di stringhe contenente il valore da convertire.</param>
        /// <param name="index">Indice dell'array in cui si trova il valore.</param>
        /// <param name="defaultValue">Valore di default da restituire in caso di fallimento del parsing.</param>
        /// <returns>Il valore float parsato oppure il defaultValue.</returns>
        public static float SafeParseFloat(string[] fields, int index, float defaultValue = 0.0f)
        {
            string? rawValue = SafeGet(fields, index).Trim();
            return float.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out float result)
                ? result
                : defaultValue;

        }


        /// <summary>
        /// Effettua il parsing sicuro di un valore unsigned int (uint) da un array di stringhe.
        /// Se il parsing fallisce, restituisce il valore di default specificato (0 se non diversamente indicato).
        /// </summary>
        /// <param name="fields">Array di stringhe contenente il valore da convertire.</param>
        /// <param name="index">Indice dell'array in cui si trova il valore.</param>
        /// <param name="defaultValue">Valore di default da restituire in caso di fallimento del parsing.</param>
        /// <returns>Il valore uint parsato oppure il defaultValue.</returns>
        public static uint SafeParseUInt(string[] fields, int index, uint defaultValue = 0)
        {
            string? rawValue = SafeGet(fields, index).Trim();
            return uint.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out uint result) 
                ? result 
                : defaultValue;
        }



        /// <summary>
        /// Effettua il parsing sicuro di un valore unsigned long (ulong) da un array di stringhe.
        /// Se il parsing fallisce, restituisce il valore di default specificato (0 se non diversamente indicato).
        /// </summary>
        /// <param name="fields">Array di stringhe contenente il valore da convertire.</param>
        /// <param name="index">Indice dell'array in cui si trova il valore.</param>
        /// <param name="defaultValue">Valore di default da restituire in caso di fallimento del parsing.</param>
        /// <returns>Il valore ulong parsato oppure il defaultValue.</returns>
        public static ulong SafeParseULong(string[] fields, int index, ulong defaultValue = 0)
        {
            string? rawValue = SafeGet(fields, index).Trim();
            return ulong.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out ulong result) 
                ? result 
                : defaultValue;
        }
        

    }
}
