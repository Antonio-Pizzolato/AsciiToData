using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Services
{
    public class BinaryFileReader
    {
        /// Legge un file binario e restituisce una lista di oggetti usando la funzione di lettura passata.
        public List<T> ReadBinaryFile<T>(string filePath, Func<BinaryReader, T> readFunc)
        {
            var records = new List<T>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    T item = readFunc(br);
                    records.Add(item);
                }
            }

            return records;
        }
    }
}
