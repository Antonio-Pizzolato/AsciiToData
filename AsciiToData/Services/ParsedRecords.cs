using BinaryConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Services
{

    /// <summary>
    /// Contenitore per i record parsati dal file di input.
    /// Questa classe raggruppa le liste di record per ciascun tipo:
    /// Lavori, Barre, Pezzi e Residui.
    /// </summary>
    public class ParsedRecords
    {
        /// <summary>
        /// Lista dei record di tipo "L" (Lavori).
        /// </summary>
        public List<DATI_LAVORO> Lavori = new List<DATI_LAVORO>();

        /// <summary>
        /// Lista dei record di tipo "B" (Barre).
        /// </summary>
        public List<DATI_BARRA> Barre = new List<DATI_BARRA>();

        /// <summary>
        /// Lista dei record di tipo "P" (Pezzi).
        /// </summary>
        public List<DATI_PEZZO> Pezzi = new List<DATI_PEZZO>();

        /// <summary>
        /// Lista dei record di tipo "R" (Residui).
        /// </summary>
        public List<DATI_PEZZO_RESTANTE> Residui = new List<DATI_PEZZO_RESTANTE>();
    }
}
