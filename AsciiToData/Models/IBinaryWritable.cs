using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Models
{
    public interface IBinaryWritable
    {
        void WriteBinary(System.IO.BinaryWriter writer);
    }
}
