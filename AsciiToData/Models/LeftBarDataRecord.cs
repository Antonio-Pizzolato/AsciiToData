using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiToData.Models
{
    public class LeftBarDataRecord : CMP265Record
    {
        public double? ExLength { get; set; }
        public double? InLength { get; set; }
        public double? LAngle { get; set; }
        public double? RAngle { get; set; }
        public string? Reference { get; set; }
    }
}
