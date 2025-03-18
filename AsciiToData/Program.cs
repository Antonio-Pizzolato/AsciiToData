// Program.cs
using BinaryConverter.Models;
using BinaryConverter.Services;
using BinaryConverter.Utils;
using System;
using System.Text;

class Program
{
    static void Main()
    {
        // Percorso del file di input
        string inputFile = "C:\\Users\\antonio.pizzolato\\source\\repos\\AsciiToData\\AsciiToData\\Data\\Antonio2.txt";

        try
        {
            IRecordProcessor recordProcessor = new RecordProcessor();
            var parsedRecords = recordProcessor.ProcessFile(inputFile);

            // Specifica i percorsi di output
            string pathLav = @"C:\Users\antonio.pizzolato\source\repos\AsciiToData\AsciiToData\Data\output.lav";
            string pathBar = @"C:\Users\antonio.pizzolato\source\repos\AsciiToData\AsciiToData\Data\output.bar";
            string pathPez = @"C:\Users\antonio.pizzolato\source\repos\AsciiToData\AsciiToData\Data\output.pez";
            string pathRes = @"C:\Users\antonio.pizzolato\source\repos\AsciiToData\AsciiToData\Data\output.res";


            using (FileStream fs = new FileStream(pathLav, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs, Encoding.Unicode))
            {
                // Se desideri, scrivi il BOM
                // writer.Write((ushort)0xFEFF);

                foreach (var lavoro in parsedRecords.Lavori)
                {
                    DATI_LAVORO.WriteDatiLavoroRecord(writer, lavoro);
                }
            }

            using (FileStream fs = new FileStream(pathBar, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs, Encoding.Unicode))
            {
                // Se desideri, scrivi il BOM
               // writer.Write((ushort)0xFEFF);

                foreach (var barra in parsedRecords.Barre)
                {
                    DATI_BARRA.WriteDatiBarreRecord(writer, barra);
                }
            }

            using (FileStream fs = new FileStream(pathPez, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs, Encoding.Unicode))
            {
                // Se desideri, scrivi il BOM
                // writer.Write((ushort)0xFEFF);

                foreach (var pezzo in parsedRecords.Pezzi)
                {
                    DATI_PEZZO.WriteDatiPezziRecord(writer, pezzo);
                }
            }

            using (FileStream fs = new FileStream(pathRes, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs, Encoding.Unicode))
            {
                // Se desideri, scrivi il BOM
                // writer.Write((ushort)0xFEFF);

                foreach (var residuo in parsedRecords.Residui)
                {
                    DATI_PEZZO_RESTANTE.WriteDatiResiduoRecord(writer, residuo);
                }
            }


            Console.WriteLine("Conversione completata.");

        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore: " + ex.Message);
        }
    }
}