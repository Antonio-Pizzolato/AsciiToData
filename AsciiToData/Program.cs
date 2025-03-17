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
        string inputFile = "C:\\Users\\antonio.pizzolato\\source\\repos\\AsciiToData\\AsciiToData\\Data\\Antonio.txt";

        try
        {
            IRecordProcessor recordProcessor = new RecordProcessor();
            var parsedRecords = recordProcessor.ProcessFile(inputFile);

            //IBinaryFileWriter fileWriter = new BinaryFileWriter();
            //fileWriter.WriteBinaryFile("C:\\Users\\antonio.pizzolato\\source\\repos\\AsciiToData\\AsciiToData\\Data\\output.lav", parsedRecords.Lavori);
            //fileWriter.WriteBinaryFile("C:\\Users\\antonio.pizzolato\\source\\repos\\AsciiToData\\AsciiToData\\Data\\output.bar", parsedRecords.Barre);
            //fileWriter.WriteBinaryFile("C:\\Users\\antonio.pizzolato\\source\\repos\\AsciiToData\\AsciiToData\\Data\\output.pez", parsedRecords.Pezzi);
            //fileWriter.WriteBinaryFile("C:\\Users\\antonio.pizzolato\\source\\repos\\AsciiToData\\AsciiToData\\Data\\output.res", parsedRecords.Residui);

            // Specifica i percorsi di output
            string pathLav = @"C:\Users\antonio.pizzolato\source\repos\AsciiToData\AsciiToData\Data\output.lav";
            string pathBar = @"C:\Users\antonio.pizzolato\source\repos\AsciiToData\AsciiToData\Data\output.bar";
            string pathPez = @"C:\Users\antonio.pizzolato\source\repos\AsciiToData\AsciiToData\Data\output.pez";
            string pathRes = @"C:\Users\antonio.pizzolato\source\repos\AsciiToData\AsciiToData\Data\output.res";


            using (FileStream fs = new FileStream(pathBar, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs, Encoding.Unicode))
            {
                // Se desideri, scrivi il BOM
               // writer.Write((ushort)0xFEFF);

                foreach (var record in parsedRecords.Barre)
                {
                    DATI_BARRA.WriteDatiBarreRecord(writer, record);
                }
            }



            // Scrittura dei file
            //BinaryFileWriter.WriteFileLav(parsedRecords.Lavori, pathLav);
            //BinaryFileWriter.WriteFileBar(parsedRecords.Barre, pathBar);
            //BinaryFileWriter.WriteFilePez(parsedRecords.Pezzi, pathPez);
            //BinaryFileWriter.WriteFileRes(parsedRecords.Residui, pathRes);


            Console.WriteLine("Conversione completata.");

            //// Leggi file binari (assumendo che output.lav, output.bar, output.pez, output.res siano già stati creati)
            //var fileReader = new BinaryFileReader();

            //// Deserializzazione dei Lavori
            //var lavori = fileReader.ReadBinaryFile("C:\\Users\\antonio.pizzolato\\source\\repos\\AsciiToData\\AsciiToData\\Data\\output.lav", LavoroRecord.ReadBinary);
            //Console.WriteLine("Lavori deserializzati:");
            //foreach (var lav in lavori)
            //{
            //    Console.WriteLine($"NumLav: {lav.num_lav}, StatoLav: {lav.stato_lav}");
            //}

            //// Deserializzazione delle Barre
            //var barre = fileReader.ReadBinaryFile("C:\\Users\\antonio.pizzolato\\source\\repos\\AsciiToData\\AsciiToData\\Data\\output.bar", BarraRecord.ReadBinary);
            //Console.WriteLine("\nBarre deserializzate:");
            //foreach (var bar in barre)
            //{
            //    Console.WriteLine($"CodProf: {bar.cod_prof}, Col: {bar.col}, Tipo: {bar.nbarra_ass}, LungStandard: {bar.lung_standard}, {bar.rif1}, {bar.rif2}, {bar.slats_ass}, {bar.spess}");
            //}

            //// Deserializzazione dei Pezzi
            //var pezzi = fileReader.ReadBinaryFile("C:\\Users\\antonio.pizzolato\\source\\repos\\AsciiToData\\AsciiToData\\Data\\output.pez", PezzoRecord.ReadBinary);
            //Console.WriteLine("\nPezzi deserializzati:");
            //foreach (var pez in pezzi)
            //{
            //    Console.WriteLine($"LExt: {pez.l_ext}, LInt: {pez.l_int}, Ordine: {pez.angt_sx}, Cliente: {pez.angp_sx}, {pez.angt_dx},{pez.angp_dx},{pez.num_pezzo},{pez.n_carrello},{pez.n_slot},{pez.n_unico_pezzo},{pez.id},{pez.tag_speciale},{pez.ordine},{pez.cliente},{pez.pian_trav1},{pez.pian_trav2},{pez.pian_trav3}, {pez.rinf},{pez.fissaggio},{pez.cod_tip},{pez.f_acqua1},{pez.f_acqua2},{pez.f_acqua3},{pez.note},{pez.q1},{pez.q2},{pez.q3},{pez.q4},{pez.info1},{pez.info2},{pez.info3},{pez.info4},{pez.info5}");
            //}

            //// Deserializzazione dei Residui
            //var residui = fileReader.ReadBinaryFile("C:\\Users\\antonio.pizzolato\\source\\repos\\AsciiToData\\AsciiToData\\Data\\output.res", ResiduoRecord.ReadBinary);
            //Console.WriteLine("\nResidui deserializzati:");
            //foreach (var res in residui)
            //{
            //    Console.WriteLine($"NBarra: {res.n_barra}, Rif: {res.l_ext}, {res.l_int},{res.ang_sx},{res.ang_dx},{res.rif}");
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore: " + ex.Message);
        }
    }
}