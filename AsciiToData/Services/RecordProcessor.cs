using BinaryConverter.Models;
using BinaryConverter.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConverter.Services
{
    public class RecordProcessor : IRecordProcessor
    {
        public ParsedRecords ProcessFile(string filePath)
        {
            var parsedRecords = new ParsedRecords();

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File non trovato: " + filePath);
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string trimmedLine = ParserUtils.RemoveTrailingCommas(line.Trim());
                string[] fields = line.Split(',')
                                              .Select(f => f.Trim())
                                              .ToArray();

                if (fields.Length == 0)
                    continue;

                char recordType = fields[0].Length > 0 ? fields[0][0] : ' ';
                switch (recordType)
                {
                    case 'L':
                        
                        var lav = new LavoroRecord
                        {
                            num_lav = ParserUtils.SafeGet(fields, 1),
                            stato_lav = ParserUtils.SafeGet(fields, 2),
                        };
                        parsedRecords.Lavori.Add(lav);
                        
                        break;
                    case 'B':
                        var barra = new BarraRecord
                        {
                            cod_prof = ParserUtils.SafeGet(fields, 1),
                            col = ParserUtils.SafeGet(fields, 2),
                            nbarra_ass = ParserUtils.SafeParseShort(fields, 3),
                            lung_standard = ParserUtils.SafeParseFloat(fields, 4),
                            rif1 = ParserUtils.SafeGet(fields, 5),
                            rif2 = ParserUtils.SafeGet(fields, 6),
                            slats_ass = ParserUtils.SafeParseShort (fields, 7),
                            spess = ParserUtils.SafeParseShort(fields, 8)
                        };
                        parsedRecords.Barre.Add(barra);
                        break;
                    case 'P':
                        var pezzo = new PezzoRecord
                        {
                            l_ext = ParserUtils.SafeParseFloat(fields, 1),
                            l_int = ParserUtils.SafeParseFloat(fields, 2),
                            angt_sx = ParserUtils.SafeParseFloat(fields, 3),
                            angp_sx = ParserUtils.SafeParseFloat(fields, 4),
                            angt_dx = ParserUtils.SafeParseFloat(fields, 5),
                            angp_dx = ParserUtils.SafeParseFloat(fields, 6),

                            num_pezzo = ParserUtils.SafeParseShort(fields, 7),
                            n_carrello = ParserUtils.SafeParseShort(fields, 8),
                            n_slot = ParserUtils.SafeParseShort(fields, 9),

                            n_unico_pezzo =ParserUtils.ParseLong(fields[10]),
                                
                            id = ParserUtils.SafeGet(fields, 11),
                            tag_speciale = ParserUtils.SafeGet(fields, 12),
                            ordine = ParserUtils.SafeGet(fields, 13),
                            cliente = ParserUtils.SafeGet(fields, 14),
                            pian_trav1 = ParserUtils.SafeGet(fields, 15),
                            pian_trav2 = ParserUtils.SafeGet(fields, 16),
                            pian_trav3 = ParserUtils.SafeGet(fields, 17),
                            rinf = ParserUtils.SafeGet(fields, 18),
                            fissaggio = ParserUtils.SafeGet(fields, 19),
                            cod_tip = ParserUtils.SafeGet(fields, 20),
                            f_acqua1 = ParserUtils.SafeGet(fields, 21),
                            f_acqua2 = ParserUtils.SafeGet(fields, 22),
                            f_acqua3 = ParserUtils.SafeGet(fields, 23),
                            note = ParserUtils.SafeGet(fields, 24),

                            q1 = ParserUtils.SafeParseFloat(fields, 25),
                            q2 = ParserUtils.SafeParseFloat(fields, 26),
                            q3 = ParserUtils.SafeParseFloat(fields, 27),
                            q4 = ParserUtils.SafeParseFloat(fields, 28),

                            info1 = ParserUtils.SafeGet(fields, 29),
                            info2 = ParserUtils.SafeGet(fields, 30),
                            info3 = ParserUtils.SafeGet(fields, 31),
                            info4 = ParserUtils.SafeGet(fields, 32),
                            info5 = ParserUtils.SafeGet(fields, 33)
                        };
                        parsedRecords.Pezzi.Add(pezzo);
                        break;
                    case 'R':
                        var residuo = new ResiduoRecord
                        {
                            n_barra = ParserUtils.SafeParseShort(fields, 1),
                            l_ext = ParserUtils.SafeParseFloat(fields, 2),
                            l_int = ParserUtils.SafeParseFloat(fields, 3),
                            ang_sx = ParserUtils.SafeParseFloat(fields, 4),
                            ang_dx = ParserUtils.SafeParseFloat(fields, 5),
                            rif = fields.Length >= 7 ? fields[6] : string.Empty
                        };
                        parsedRecords.Residui.Add(residuo);
                        break;
                    default:
                        Console.WriteLine("Tipo record non riconosciuto: " + recordType);
                        break;
                }
            }

            return parsedRecords;
        }

        // Metodi helper per la sicurezza
        private static string SafeGet(string[] fields, int index)
        {
            return fields.Length > index ? fields[index] : string.Empty;
        }

        private static short SafeParseShort(string[] fields, int index, short defaultValue = 0)
        {
            string? rawValue = SafeGet(fields, index).Trim();
            return short.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out short result)
                ? result
                : defaultValue;
        }

        private static float? SafeParseFloat(string[] fields, int index, float? defaultValue = null)
        {
            string? rawValue = SafeGet(fields, index).Trim();
            return float.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out float result)
                ? result
                : defaultValue;

        }

        private static long SafeParseLong(string[] fields, int index, long defaultValue = 0)
        {
            string? rawValue = SafeGet(fields, index).Trim();
            return long.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out long result)
                ? result
                : defaultValue;
        }
    }
}
