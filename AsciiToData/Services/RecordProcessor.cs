using BinaryConverter.Models;
using BinaryConverter.Utils;
using System;
using System.Collections.Generic;
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
                string[] fields = trimmedLine.Split(',')
                                              .Select(f => f.Trim())
                                              .ToArray();

                if (fields.Length == 0)
                    continue;

                char recordType = fields[0].Length > 0 ? fields[0][0] : ' ';
                switch (recordType)
                {
                    case 'L':
                        if (fields.Length >= 3)
                        {
                            var lav = new LavoroRecord
                            {
                                num_lav = fields[1],
                                stato_lav = fields[2]
                            };
                            parsedRecords.Lavori.Add(lav);
                        }
                        break;
                    case 'B':
                        if (fields.Length >= 5)
                        {
                            var barra = new BarraRecord
                            {
                                cod_prof = fields[1],
                                col = fields[2],
                                nbarra_ass = ParserUtils.ParseShort(fields[3]),
                                lung_standard = ParserUtils.ParseFloat(fields[4]),
                                rif1 = fields[5],
                                rif2 = fields[6],
                                slats_ass = ParserUtils.ParseShort (fields[7]),
                                spess = ParserUtils.ParseFloat(fields[8])
                            };
                            parsedRecords.Barre.Add(barra);
                        }
                        break;
                    case 'P':
                        if (fields.Length >= 14)
                        {
                            var pezzo = new PezzoRecord
                            {
                                l_ext = ParserUtils.ParseFloat(fields[1]),
                                l_int = ParserUtils.ParseFloat(fields[2]),
                                angt_sx = ParserUtils.ParseFloat(fields[3]),
                                angp_sx = ParserUtils.ParseFloat(fields[4]),
                                angt_dx = ParserUtils.ParseFloat(fields[5]),
                                angp_dx = ParserUtils.ParseFloat(fields[6]),

                                num_pezzo = ParserUtils.ParseShort(fields[7]),
                                n_carrello = ParserUtils.ParseShort(fields[8]),
                                n_slot = ParserUtils.ParseShort(fields[9]),

                                n_unico_pezzo =ParserUtils.ParseLong(fields[10]),
                                
                                id = fields[11],
                                tag_speciale = fields[12],
                                ordine = fields[13],
                                cliente = fields[14],
                                pian_trav1 = fields[15],
                                pian_trav2 = fields[16],
                                pian_trav3 = fields[17],
                                rinf = fields[18],
                                fissaggio = fields[19],
                                cod_tip = fields[20],
                                f_acqua1 = fields[21],
                                f_acqua2 = fields[22],
                                f_acqua3 = fields[23],
                                note = fields[24],

                                q1 = ParserUtils.ParseFloat(fields[25]),
                                q2 = ParserUtils.ParseFloat(fields[26]),
                                q3 = ParserUtils.ParseFloat(fields[27]),
                                q4 = ParserUtils.ParseFloat(fields[28]),

                                info1 = fields[29],
                                info2 = fields[30],
                                info3 = fields[31],
                                info4 = fields[32],
                                info5 = fields[33]
                            };
                            parsedRecords.Pezzi.Add(pezzo);
                        }
                        break;
                    case 'R':
                        if (fields.Length >= 6)
                        {
                            var residuo = new ResiduoRecord
                            {
                                n_barra = ParserUtils.ParseShort(fields[1]),
                                l_ext = ParserUtils.ParseFloat(fields[2]),
                                l_int = ParserUtils.ParseFloat(fields[3]),
                                ang_sx = ParserUtils.ParseFloat(fields[4]),
                                ang_dx = ParserUtils.ParseFloat(fields[5]),
                                rif = fields.Length >= 7 ? fields[6] : string.Empty
                            };
                            parsedRecords.Residui.Add(residuo);
                        }
                        break;
                    default:
                        Console.WriteLine("Tipo record non riconosciuto: " + recordType);
                        break;
                }
            }

            return parsedRecords;
        }
    }
}
