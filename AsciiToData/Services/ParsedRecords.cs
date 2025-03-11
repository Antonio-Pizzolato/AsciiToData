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
        public List<LavoroRecord> Lavori { get; set; } = new List<LavoroRecord>();
        public List<BarraRecord> Barre { get; set; } = new List<BarraRecord>();
        public List<PezzoRecord> Pezzi { get; set; } = new List<PezzoRecord>();
        public List<ResiduoRecord> Residui { get; set; } = new List<ResiduoRecord>();
    }
}
