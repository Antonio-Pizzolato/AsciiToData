using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using AsciiToData.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

public class ExportExcel
{
    public void ExportToExcel(List<CMP265Record> records, string filePath)
    {
        using (var workbook = new XLWorkbook())
        {
            // Foglio "Dati Aggregati"
            AddAggregatedWorksheet(workbook, "Dati Aggregati", records);

            // Fogli separati per ogni tipo di record (opzionale)
            AddWorksheet(workbook, "Work Data", records.Where(r => r is WorkDataRecord).Cast<WorkDataRecord>().ToList());
            AddWorksheet(workbook, "Bar Data", records.Where(r => r is BarDataRecord).Cast<BarDataRecord>().ToList());
            AddWorksheet(workbook, "Piece Data", records.Where(r => r is PieceDataRecord).Cast<PieceDataRecord>().ToList());
            AddWorksheet(workbook, "Left Bar Data", records.Where(r => r is LeftBarDataRecord).Cast<LeftBarDataRecord>().ToList());

            // Salva il file Excel
            workbook.SaveAs(filePath);
        }
    }

    private void AddWorksheet<T>(XLWorkbook workbook, string sheetName, List<T> records) where T : CMP265Record
    {
        if (!records.Any()) return;

        var worksheet = workbook.Worksheets.Add(sheetName);
        worksheet.Style.NumberFormat.Format = "0###";

        // Ottieni le proprietà della classe
        var properties = typeof(T).GetProperties();

        // Intestazioni
        for (int i = 0; i < properties.Length; i++)
        {
            worksheet.Cell(1, i + 1).Value = properties[i].Name;
        }

        // Dati
        for (int row = 0; row < records.Count; row++)
        {
            for (int col = 0; col < properties.Length; col++)
            {
                var value = properties[col].GetValue(records[row]);
                var cell = worksheet.Cell(row + 2, col + 1);

                // Gestione esplicita dei tipi
                switch (value)
                {
                    case int intValue:
                        cell.Value = intValue;
                        cell.Style.NumberFormat.Format = "00"; // Formato a 2 cifre
                        break;

                    case double doubleValue:
                        cell.Value = doubleValue;
                        cell.Style.NumberFormat.Format = "00.00"; // Formato per decimali
                        break;

                    case float floatValue:
                        cell.Value = floatValue;
                        cell.Style.NumberFormat.Format = "00.00"; // Formato per decimali
                        break;

                    case decimal decimalValue:
                        cell.Value = decimalValue;
                        cell.Style.NumberFormat.Format = "00.00"; // Formato per decimali
                        break;

                    case string stringValue:
                        cell.Value = stringValue;
                        break;

                    default:
                        cell.Value = "N/A"; // Valore predefinito per tipi non gestiti
                        break;
                }
                // Formattazione automatica
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
            }
        }
    }

    private void AddAggregatedWorksheet(XLWorkbook workbook, string sheetName, List<CMP265Record> records)
    {
        var worksheet = workbook.Worksheets.Add(sheetName);
        worksheet.Style.NumberFormat.Format = "0000";

        // Intestazioni
        worksheet.Cell(1, 1).Value = "Tipo";
        worksheet.Cell(1, 2).Value = "Dettagli";

        int row = 2; // Inizia dalla riga 2
        

        foreach (var record in records)
        {
            switch (record)
            {
                case WorkDataRecord work:
                    worksheet.Cell(row, 1).Value = "L";
                    worksheet.Cell(row, 2).Value = $"{work.Code},{work.Status}";
                    row++;
                    break;

                case BarDataRecord bar:
                    worksheet.Cell(row, 1).Value = "B";
                    worksheet.Cell(row, 2).Style.NumberFormat.Format = "00";
                    worksheet.Cell(row, 2).Value = $"{bar.Code},{bar.Colour},{bar.BarsNr},{bar.BarLength},{bar.Ref1},{bar.Ref2},{bar.SlatsTogether},{bar.Thickness}";
                    row++;
                    break;

                case PieceDataRecord piece:
                    worksheet.Cell(row, 1).Value = "P";
                    worksheet.Cell(row, 2).Style.NumberFormat.Format = "00";
                    worksheet.Cell(row, 2).Value = $"{piece.ExLength},{piece.InLength},{piece.LTAngle},{piece.LPAngle}," +
                        $"{piece.RTAngle},{piece.RPAngle},{piece.NPieces},{piece.NCarriage},{piece.NSlot},{piece.UniquePiece},{piece.PieceID},{piece.SpecialCut},{piece.Order}," +
                        $"{piece.Customer},{piece.PosPlacCross1},{piece.PosPlacCross2},{piece.PosPlacCross3},{piece.Reinforce},{piece.Fixing},{piece.TypeCode},{piece.HolesH2o1},{piece.HolesH2o2},{piece.HolesH2o3},{piece.Notes}," +
                        $"{piece.Q1},{piece.Q2},{piece.Q3},{piece.Q4},{piece.Info1},{piece.Info2},{piece.Info3},{piece.Info4},{piece.Info5}";
                    row++;
                    break;

                case LeftBarDataRecord leftBar:
                    worksheet.Cell(row, 1).Value = "R";
                    worksheet.Cell(row, 2).Value = $"{leftBar.ExLength},{leftBar.InLength},{leftBar.LAngle},{leftBar.RAngle},{leftBar.Reference}";
                    row++;
                    break;
            }
        }

        // Formattazione automatica
        worksheet.Columns().AdjustToContents();
        worksheet.Rows().AdjustToContents();
    }


}
