using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsciiToData.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AsciiToData.Services
{
    class ExportPDF
    {
        public void ExportToPDF(List<CMP265Record> records, string filePath)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Portrait());
                    page.Margin(2, Unit.Centimetre);

                    page.Header().AlignCenter().Text("CMP256 Record").FontSize(16).Bold().FontColor(Colors.Blue.Medium);

                    page.Content().PaddingVertical(15).Column(column =>
                    {
                        // Sezioni per ogni tipo di record
                        AddRecordSection(column, "Work Data", records.Where(r => r is WorkDataRecord).Cast<WorkDataRecord>().ToList());
                        AddRecordSection(column, "Bar Data", records.Where(r => r is BarDataRecord).Cast<BarDataRecord>().ToList());
                        AddRecordSection(column, "Piece Data", records.Where(r => r is PieceDataRecord).Cast<PieceDataRecord>().ToList());
                        AddRecordSection(column, "Left Bar Data", records.Where(r => r is LeftBarDataRecord).Cast<LeftBarDataRecord>().ToList());
                    });
                });
            }).GeneratePdf(filePath);
        }

        private void AddRecordSection<T>(ColumnDescriptor column, string sectionTitle, List<T> records) where T : CMP265Record
        {
            if (!records.Any()) return;

            column.Item().PaddingBottom(10).Column(sectionColumn =>
            {
                sectionColumn.Item().Text(sectionTitle)
                    .FontSize(14).SemiBold().FontColor(Colors.Grey.Darken3);

                sectionColumn.Item().Table(table =>
                {
                    // Intestazioni della tabella
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(); // Campo
                        columns.RelativeColumn(); // Valore
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Field").SemiBold();
                        header.Cell().Text("Value").SemiBold();
                    });

                    // Dettagli dei record
                    foreach (var record in records)
                    {
                        var properties = typeof(T).GetProperties();
                        foreach (var prop in properties)
                        {
                            table.Cell().Text(prop.Name);
                            table.Cell().Text(prop.GetValue(record)?.ToString() ?? "N/A");
                        }
                    }
                });
            });
        }

    }
}
