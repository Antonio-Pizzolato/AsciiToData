using BinaryConverter.Models;
using BinaryConverter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Utils
{
    public class BinaryFileWriter
    {
        //public void WriteBinaryFile<T>(string fileName, List<T> records) where T : IBinaryWritable
        //{
        //    using (FileStream fs = new FileStream(fileName, FileMode.Create))
        //    using (BinaryWriter bw = new BinaryWriter(fs))
        //    {
        //        foreach (var record in records)
        //        {
        //            record.WriteBinary(bw);
        //        }
        //    }
        //}


        public static byte[] StructureToByteArray<T>(T obj) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(obj, ptr, true);
                Marshal.Copy(ptr, arr, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            return arr;
        }
        

        public static void WriteFileLav(List<DATI_LAVORO> listaLav, string outputPath)
        {
            using (FileStream fs = new FileStream(outputPath, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs, Encoding.Unicode))
            {
                //writer.Write((ushort)0xFEFF); // BOM
                foreach (var lav in listaLav)
                {
                    byte[] recordBytes = StructureToByteArray(lav);
                    writer.Write(recordBytes);
                    
                }
            }
        }

        public static void WriteFileBar(List<DATI_BARRA> listaBarre, string outputPath)
        {
            using (FileStream fs = new FileStream(outputPath, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs, Encoding.Unicode))
            {
                // Se vuoi aggiungere un BOM all'inizio:
                writer.Write((ushort)0xFEFF);

                foreach (var barra in listaBarre)
                {
                    byte[] recordBytes = StructureToByteArray(barra);
                    writer.Write(recordBytes);
                }
            }

        }




        public static void WriteFilePez(List<DATI_PEZZO> listaPezzi, string outputPath)
        {
            using (FileStream fs = new FileStream(outputPath, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs, Encoding.Unicode))
            {
                //writer.Write((ushort)0xFEFF);
                foreach (var pez in listaPezzi)
                {
                    byte[] recordBytes = BinaryFileWriter.StructureToByteArray(pez);
                    writer.Write(recordBytes);
                }
            }
        }



        public static void WriteFileRes(List<DATI_PEZZO_RESTANTE> listaResidui, string outputPath)
        {
            using (FileStream fs = new FileStream(outputPath, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs, Encoding.Unicode))
            {
                //writer.Write((ushort)0xFEFF);
                foreach (var res in listaResidui)
                {
                    byte[] recordBytes = StructureToByteArray(res);
                    writer.Write(recordBytes);
                }
            }
        }



    }
}


