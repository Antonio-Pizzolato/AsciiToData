using BinaryConverter.Models;
using BinaryConverter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Utils
{
    public class BinaryFileWriter : IBinaryFileWriter
    {
        public void WriteBinaryFile<T>(string fileName, List<T> records) where T : IBinaryWritable
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                foreach (var record in records)
                {
                    record.WriteBinary(bw);
                }
            }
        }
    }
}
