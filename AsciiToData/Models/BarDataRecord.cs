using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiToData.Models
{
    public class BarDataRecord : CMP265Record
    {
        public string? Code { get; set; }
        public string? Colour { get; set; }
        public double? BarsNr { get; set; }
        public double? BarLength { get; set; }
        public string? Ref1 { get; set; }
        public string? Ref2 { get; set; }
        public double? SlatsTogether { get; set; }
        public float? Thickness { get; set; }
    }
}
