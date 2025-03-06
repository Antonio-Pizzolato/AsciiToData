using AsciiToData.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiToData.Services
{
    public class CMP265FileParser()
    {
        public List<CMP265Record> ParseFile(string filePath)
        {
            var records = new List<CMP265Record>();

            using (var reader = new StreamReader(filePath, Encoding.ASCII))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var fields = ParseLine(line);
                    var record = CreateRecord(fields);
                    if (record != null)
                    {
                        records.Add(record);
                    }
                }
                return records;
            }
        }

        private string[] ParseLine(string line)
        {

            return line.Split(new[] { ',' }, StringSplitOptions.None);
            
        }

        private CMP265Record? CreateRecord(string[] fields)
        {
            if (fields.Length == 0) return null;

            return fields[0].ToUpper() switch
            {
                "L" => new WorkDataRecord
                {
                    RecordType = "L",
                    Code = SafeGet(fields, 1),
                    Status = SafeGet(fields, 2)  // in teoria non mi serve quel controllo perché se il campo è lasciato vuoto avrò x,,y
                },
                "B" => new BarDataRecord
                {
                    RecordType = "B",
                    Code = SafeGet(fields, 1),
                    Colour = SafeGet(fields, 2),
                    BarsNr = SafeParseDouble(fields, 3),
                    BarLength = SafeParseDouble(fields, 4),
                    Ref1 = SafeGet(fields, 5),
                    Ref2 = SafeGet(fields, 6),
                    Thickness = SafeParseFloat(fields, 8)
                },
                "P" => new PieceDataRecord
                {
                    RecordType = "P",
                    ExLength = SafeParseDouble(fields, 1),
                    InLength = SafeParseDouble(fields, 2),
                    LTAngle = SafeParseDouble(fields, 3),
                    LPAngle = SafeParseDouble(fields, 4),
                    RTAngle = SafeParseDouble(fields, 5),
                    RPAngle = SafeParseDouble(fields, 6),
                    NPieces = SafeParseDouble(fields, 7),
                    NCarriage = SafeParseDouble(fields, 8),
                    NSlot = SafeParseDouble(fields, 9),
                    UniquePiece = SafeParseDouble(fields, 10),
                    PieceID = SafeGet(fields, 11),
                    SpecialCut = SafeGet(fields, 12),
                    Order = SafeGet(fields, 13),
                    Customer = SafeGet(fields, 14),
                    PosPlacCross1 = SafeGet(fields, 15),
                    PosPlacCross2 = SafeGet(fields, 16),
                    PosPlacCross3 = SafeGet(fields, 17),
                    Reinforce = SafeGet(fields, 18),
                    Fixing = SafeGet(fields, 19),
                    TypeCode = SafeGet(fields, 20),
                    HolesH2o1 = SafeGet(fields, 21),
                    HolesH2o2 = SafeGet(fields, 22),
                    HolesH2o3 = SafeGet(fields, 23),
                    Notes = SafeGet(fields, 24),
                    Q1 = SafeParseDouble(fields, 25, null),
                    Q2 = SafeParseDouble(fields, 26, null),
                    Q3 = SafeParseDouble(fields, 27, null),
                    Q4 = SafeParseDouble(fields, 28),
                    Info1 = SafeGet(fields, 29),
                    Info2 = SafeGet(fields, 30),
                    Info3 = SafeGet(fields, 31),
                    Info4 = SafeGet(fields, 32),
                    Info5 = SafeGet(fields, 33)

                },
                "R" => new LeftBarDataRecord
                {
                    RecordType = "R",
                    ExLength = SafeParseDouble(fields, 1),
                    InLength = SafeParseDouble(fields, 2),
                    LAngle = SafeParseDouble(fields, 3),
                    RAngle = SafeParseDouble(fields, 4),
                    Reference = SafeGet(fields, 5),
                },
                _ => null
            };
        }

        private static BarDataRecord HandleBarDataRecord(string[] fields)
        {

            var record = new BarDataRecord() { RecordType = fields[0] };
            try
            {
                record.RecordType = "B";
                record.Code = SafeGet(fields, 1);
                record.Colour = SafeGet(fields, 2);
                record.BarsNr = SafeParseDouble(fields, 3);
                record.BarLength = SafeParseDouble(fields, 4);
                record.Ref1 = SafeGet(fields, 5);
                record.Ref2 = SafeGet(fields, 6);
                record.Thickness = SafeParseFloat(fields, 8);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw;
            }
            return record;
        }


        // Metodi helper per la sicurezza
        private static string SafeGet(string[] fields, int index)
        {
            return fields.Length > index ? fields[index] : string.Empty;
        }

        private static int SafeParseInt(string[] fields, int index, int defaultValue = 0)
        {
            string rawValue = SafeGet(fields, index).Trim();
            return int.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out int result)
                ? result
                : defaultValue;
        }


        private static double? SafeParseDouble(string[] fields, int index, double? defaultValue = null)
        {
            string rawValue = SafeGet(fields, index).Trim();

            if (string.IsNullOrWhiteSpace(rawValue))
                return null;
            
            if (!double.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                Console.WriteLine($"Valore non valido '{rawValue}' nel campo {index}. Usato default: {defaultValue}");
            }
            return result;
        }

        private static float? SafeParseFloat(string[] fields, int index, float? defaultValue = null)
        {
            string rawValue = SafeGet(fields, index).Trim();
            return float.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out float result)
                ? result
                : defaultValue;

        }

    }
}
