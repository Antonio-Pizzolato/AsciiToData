using BinaryConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Services
{
    public interface IBinaryFileWriter
    {
        void WriteBinaryFile<T>(string fileName, List<T> records) where T : IBinaryWritable;
    }
}
