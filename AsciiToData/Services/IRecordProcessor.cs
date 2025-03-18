using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Services
{

    /// <summary>
    /// Interfaccia per il processore di record.
    /// Ogni classe che implementa questa interfaccia deve fornire la logica per elaborare
    /// un file di input e restituire un oggetto <see cref="ParsedRecords"/> contenente i record parsati.
    /// </summary>
    public interface IRecordProcessor
    {
        /// <summary>
        /// Processa il file specificato e restituisce i record parsati.
        /// </summary>
        /// <param name="filePath">Il percorso del file da elaborare.</param>
        /// <returns>Un oggetto <see cref="ParsedRecords"/> contenente le liste di record per ciascun tipo.</returns>
        ParsedRecords ProcessFile(string filePath);
    }
}
