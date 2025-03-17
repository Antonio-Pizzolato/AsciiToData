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

                        var lav = new DATI_LAVORO
                        {
                            num_lav = ParserUtils.SafeGet(fields, 1),
                            stato_lav = ParserUtils.SafeGet(fields, 2),
                        };
                        parsedRecords.Lavori.Add(lav);

                        break;
                    case 'B':
                        var barra = new DATI_BARRA
                        {
                            num_barra = ParserUtils.SafeParseShort(fields, 0),
                            cod_prof = ParserUtils.SafeGet(fields, 1),
                            spess = ParserUtils.SafeParseShort(fields, 8),
                            col = ParserUtils.SafeGet(fields, 2),
                            descr = ParserUtils.SafeGet(fields, 4),
                            dummy2 = 0,
                            dummy3 = 0,
                            lung_standard = ParserUtils.SafeParseFloat(fields, 4),
                            tipo = ParserUtils.SafeParseShort(fields, 8),
                            vis_nb_ass = ParserUtils.SafeParseShort(fields, 9),
                            nbarra_ass = ParserUtils.SafeParseShort(fields, 3),
                            tagliata = ParserUtils.SafeParseShort(fields, 11),
                            rif = ParserUtils.SafeGet(fields, 5),
                            dummy = new byte[32], // Riempito con 0
                            dp = "",
                            pr = "",
                            next_barra = ""
                        };
                        parsedRecords.Barre.Add(barra);
                        break;
                    case 'P':
                        var pezzo = new DATI_PEZZO
                        {
                            num_pezzo = ParserUtils.SafeParseShort(fields, 1),
                            n_barra = ParserUtils.SafeParseShort(fields, 2),

                            angt_sx = ParserUtils.SafeParseShort(fields, 3),
                            angp_sx = ParserUtils.SafeParseShort(fields, 4),
                            angt_dx = ParserUtils.SafeParseShort(fields, 5),
                            angp_dx = ParserUtils.SafeParseShort(fields, 6),

                            l_ext = ParserUtils.SafeParseShort(fields, 7),
                            l_int = ParserUtils.SafeParseShort(fields, 8),

                            id = ParserUtils.SafeGet(fields, 9),

                            n_carrello = ParserUtils.SafeParseShort(fields, 10),
                            n_slot = ParserUtils.SafeParseShort(fields, 11),

                            tag_speciale = ParserUtils.SafeGet(fields, 12),

                            n_unico_pezzo = ParserUtils.SafeParseULong(fields, 13),

                            n = ParserUtils.SafeParseShort(fields, 14),
                            n_lav = ParserUtils.SafeParseShort(fields, 15),

                            ordine = ParserUtils.SafeGet(fields, 16),
                            cliente = ParserUtils.SafeGet(fields, 17),
                            pian_trav1 = ParserUtils.SafeGet(fields, 18),
                            pian_trav2 = ParserUtils.SafeGet(fields, 19),
                            pian_trav3 = ParserUtils.SafeGet(fields, 20),
                            rinf = ParserUtils.SafeGet(fields, 21),
                            fissaggio = ParserUtils.SafeGet(fields, 22),
                            cod_tip = ParserUtils.SafeGet(fields, 23),
                            f_acqua1 = ParserUtils.SafeGet(fields, 24),
                            f_acqua2 = ParserUtils.SafeGet(fields, 25),
                            f_acqua3 = ParserUtils.SafeGet(fields, 26),
                            note = ParserUtils.SafeGet(fields, 27),

                            taglia_doppia = ParserUtils.SafeParseShort(fields, 28),

                            q = new float[4],

                            info1 = ParserUtils.SafeGet(fields, 29),
                            info2 = ParserUtils.SafeGet(fields, 30),
                            info3 = ParserUtils.SafeGet(fields, 31),
                            info4 = ParserUtils.SafeGet(fields, 32),
                            info5 = ParserUtils.SafeGet(fields, 33),

                            dummy = new byte[20],
                            pointers = new byte[4],
                        };
                        parsedRecords.Pezzi.Add(pezzo);
                        break;
                    case 'R':
                        var residuo = new DATI_PEZZO_RESTANTE
                        {
                            n_barra = ParserUtils.SafeParseShort(fields, 1),
                            dummy2 = 0,

                            l_ext = ParserUtils.SafeParseFloat(fields, 2),
                            l_int = ParserUtils.SafeParseFloat(fields, 3),
                            angt_sx = ParserUtils.SafeParseFloat(fields, 4),
                            angp_sx = ParserUtils.SafeParseFloat(fields, 5),
                            angt_dx = ParserUtils.SafeParseFloat(fields, 6),
                            angp_dx = ParserUtils.SafeParseFloat(fields, 7),

                            dummy = new byte[8],

                            rif = ParserUtils.SafeGet(fields, 8),
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

    }
}
