using BinaryConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Services
{
    public class ParsedRecords
    {
        public List<DATI_LAVORO> Lavori = new List<DATI_LAVORO>();
        public List<DATI_BARRA> Barre = new List<DATI_BARRA>();
        public List<DATI_PEZZO> Pezzi = new List<DATI_PEZZO>();
        public List<DATI_PEZZO_RESTANTE> Residui = new List<DATI_PEZZO_RESTANTE>();
    }
}
