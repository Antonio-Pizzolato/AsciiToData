// Program.cs
using BinaryConverter.Services;
using BinaryConverter.Utils;
using System;

class Program
{
    static void Main()
    {
        // Percorso del file di input
        string inputFile = "data.Ascii";

        try
        {
            IRecordProcessor recordProcessor = new RecordProcessor();
            var parsedRecords = recordProcessor.ProcessFile(inputFile);

            IBinaryFileWriter fileWriter = new BinaryFileWriter();
            fileWriter.WriteBinaryFile("output.lav", parsedRecords.Lavori);
            fileWriter.WriteBinaryFile("output.bar", parsedRecords.Barre);
            fileWriter.WriteBinaryFile("output.pez", parsedRecords.Pezzi);
            fileWriter.WriteBinaryFile("output.res", parsedRecords.Residui);

            Console.WriteLine("Conversione completata.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore: " + ex.Message);
        }
    }
}