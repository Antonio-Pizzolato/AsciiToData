using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiToData.Models
{
    public abstract class CMP265Record
    {
        public required string RecordType { get; set; }
    }
}
