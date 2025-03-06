// Program.cs
using System;
using AsciiToData.Services;
using AsciiToData.Models;

class Program
{
    static void Main(string[] args)
    {
        string filePath;

        if (args.Length > 0)
        {
            filePath = args[0];
        }
        else
        {
            Console.Write("Inserisci il percorso del file: ");
            filePath = Console.ReadLine();
        }

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File non trovato!");
            return;
        }

        //if (Path.GetExtension(filePath).ToLower() != ".txt")
        //{
        //    Console.WriteLine("Il file deve avere estensione .txt");
        //    return;
        //}

        // 2. Inizializza il parser e il validatore
        var parser = new CMP265FileParser();
        var validator = new ValidationService();

        try
        {
            var records = parser.ParseFile(filePath);
            var validationResult = validator.ValidateRecords(records);

            if (!validationResult.IsValid)
            {
                Console.WriteLine("Errori di validazione:");
                foreach (var error in validationResult.Errors)
                {
                    Console.WriteLine($"- {error}");
                }
                return;
            }

            // Esempio: Stampa risultati
            Console.WriteLine($"Trovati {records.Count} record:");
            foreach (var record in records)
            {
                switch (record)
                {
                    case WorkDataRecord l:
                        Console.WriteLine($"L - Codice: {l.Code}, Operazione: {l.Status}");
                        break;
                    case BarDataRecord b:
                        Console.WriteLine($"B - Codice: {b.Code}, Colore: {b.Colour}, Nr. Barre: {b.BarsNr}, Lunghezza Barre: {b.BarLength}");
                        break;
                    case PieceDataRecord p:
                        Console.WriteLine($"P - Lunghezza Esterna: {p.ExLength}, Lunghezza Interna: {p.InLength}, Left Tilt Angle: {p.LTAngle}, Left Pivot Angle: {p.LPAngle}, Right Tilt Angle: {p.RTAngle}, Right Pivot Angle: {p.RPAngle}, Numero Pezzi: {p.NPieces}, Numero Carriage: {p.NCarriage}, Numero Slot: {p.NSlot}, Numero Unico Pezzo: {p.UniquePiece}, ID Pezzo: {p.PieceID}, Taglio Speciale: {p.SpecialCut}, Ordine: {p.Order}, Cliente: {p.Customer}, PosPlacCross1: {p.PosPlacCross1}, PosPlacCross2: {p.PosPlacCross2}, PosPlacCross1: {p.PosPlacCross2}, Rinforzo: {p.Reinforce}, Fissaggio: {p.Fixing}, Codice Tipo: {p.TypeCode}, HolesH2o1: {p.HolesH2o1}, HolesH2o2: {p.HolesH2o2}, HolesH2o3: {p.HolesH2o3}, Note: {p.Notes}, Q1: {p.Q1}, Q2: {p.Q2}, Q3: {p.Q3}, Q4: {p.Q4}, Info1: {p.Info1}, Info2: {p.Info2}, Info3: {p.Info3}, Info4: {p.Info4}, Info5: {p.Info5}"); 
                        break;
                    case LeftBarDataRecord r:
                        Console.WriteLine($"R - Lunghezza Esterna: {r.ExLength}, Lunghezza Interna: {r.InLength}, Angolo Sinistra: {r.LAngle}, Angolo Destra: {r.RAngle}, Reference: {r.Reference}");
                        break;
                    default:
                        Console.WriteLine($"Tipo sconosciuto: {record.RecordType}");
                        break;
                }
            }

            // Procedi con l'esportazione se tutto ok
            var excelPath = Path.ChangeExtension(filePath, ".xlsx");
            var pdfPath = Path.ChangeExtension(filePath, ".pdf");
            var csvPath = Path.ChangeExtension(filePath, ".csv");

            var excelExporter = new ExportExcel();
            var pdfExporter = new ExportPDF();
            var csvExporter = new ExportCSV();

            excelExporter.ExportToExcel(records, excelPath);
            pdfExporter.ExportToPDF(records, pdfPath);
            csvExporter.ExportToCsv(records, csvPath);

            Console.WriteLine($"\nFile esportati: \n- Excel: {excelPath} \n- PDF: {pdfPath} \n- CSV: {csvPath}");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore: {ex.Message}");
        }
    }
}