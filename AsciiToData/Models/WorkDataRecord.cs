using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiToData.Models
{
    public class WorkDataRecord : CMP265Record
    {
        public string? Code { get; set; }
        public string? Status { get; set; }
    }
}
