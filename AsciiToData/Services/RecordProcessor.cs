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
        private short currentBarCount = 0; // Contatore per le barre
        private short currentPieceCount = 0; // Contatore per i pezzi
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
                        currentBarCount++;
                        var barra = new DATI_BARRA
                        {
                            num_barra = currentBarCount,
                            cod_prof = ParserUtils.SafeGet(fields, 1),
                            spess = 0,
                            col = ParserUtils.SafeGet(fields, 2),
                            descr = "",
                            dummy2 = 0,
                            dummy3 = 0,
                            lung_standard = ParserUtils.SafeParseFloat(fields, 4) / 10.0f,
                            tipo = 0,
                            vis_nb_ass = 0,
                            nbarra_ass = ParserUtils.SafeParseShort(fields, 3),
                            tagliata = 0,
                            rif = "",
                            dummy = new byte[32], // Riempito con 0
                            dp = "",
                            pr = "",
                            next_barra = ""
                        };
                        parsedRecords.Barre.Add(barra);
                        break;
                    case 'P':
                        currentPieceCount++;
                        var pezzo = new DATI_PEZZO
                        {
                            num_pezzo = currentPieceCount,
                            n_barra = currentBarCount,

                            angt_sx = ParserUtils.SafeParseFloat(fields, 3) / 10.0f,
                            //angp_sx = ParserUtils.SafeParseFloat(fields, 4) / 10.0f,  // TODO: Togli i due angoli in più e vedi se viene tutto uguale al file ANTONIO.PEZ e RES senza i due angoli in più
                            angt_dx = ParserUtils.SafeParseFloat(fields, 4) / 10.0f,
                            //angp_dx = ParserUtils.SafeParseFloat(fields, 6) / 10.0f,

                            l_ext = ParserUtils.SafeParseFloat(fields, 1) / 10.0f,
                            l_int = ParserUtils.SafeParseFloat(fields, 2) / 10.0f,

                            id = ParserUtils.SafeGet(fields, 9),

                            n_carrello = ParserUtils.SafeParseShort(fields, 6),
                            n_slot = ParserUtils.SafeParseShort(fields, 7),

                            tag_speciale = ParserUtils.SafeGet(fields, 10),

                            n_unico_pezzo = ParserUtils.SafeParseUInt(fields, 8),

                            n = ParserUtils.SafeParseShort(fields, 5),
                            n_lav = 0,

                            ordine = ParserUtils.SafeGet(fields, 11),
                            cliente = ParserUtils.SafeGet(fields, 12),
                            pian_trav1 = ParserUtils.SafeGet(fields, 13),
                            pian_trav2 = ParserUtils.SafeGet(fields, 14),
                            pian_trav3 = ParserUtils.SafeGet(fields, 15),
                            rinf = ParserUtils.SafeGet(fields, 16),
                            fissaggio = ParserUtils.SafeGet(fields, 17),
                            cod_tip = ParserUtils.SafeGet(fields, 18),
                            f_acqua1 = ParserUtils.SafeGet(fields, 19),
                            f_acqua2 = ParserUtils.SafeGet(fields, 20),
                            f_acqua3 = ParserUtils.SafeGet(fields, 21),
                            note = ParserUtils.SafeGet(fields, 22),

                            taglia_doppia = 0,

                            q = [ParserUtils.SafeParseFloat(fields, 23) / 10.0f, ParserUtils.SafeParseFloat(fields, 24) / 10.0f, ParserUtils.SafeParseFloat(fields, 25) / 10.0f, ParserUtils.SafeParseFloat(fields, 26) / 10.0f],

                            //info1 = ParserUtils.SafeGet(fields, 27),
                            //info2 = ParserUtils.SafeGet(fields, 28),
                            //info3 = ParserUtils.SafeGet(fields, 29),
                            //info4 = ParserUtils.SafeGet(fields, 30),
                            //info5 = ParserUtils.SafeGet(fields, 31),

                            dummy = new byte[20],
                            pointers = new byte[8],



                            //num_pezzo = currentPieceCount,
                            //n_barra = currentBarCount,

                            //angt_sx = ParserUtils.SafeParseFloat(fields, 1) / 10.0f,
                            ////angp_sx = ParserUtils.SafeParseFloat(fields, 4) / 10.0f,  // TODO: Togli i due angoli in più e vedi se viene tutto uguale al file ANTONIO.PEZ e RES senza i due angoli in più
                            //angt_dx = ParserUtils.SafeParseFloat(fields, 2) / 10.0f,
                            ////angp_dx = ParserUtils.SafeParseFloat(fields, 6) / 10.0f,

                            //l_ext = ParserUtils.SafeParseFloat(fields, 3) / 10.0f,
                            //l_int = ParserUtils.SafeParseFloat(fields, 4) / 10.0f,

                            //id = ParserUtils.SafeGet(fields, 5),

                            //n_carrello = ParserUtils.SafeParseShort(fields, 6),
                            //n_slot = ParserUtils.SafeParseShort(fields, 7),

                            //tag_speciale = ParserUtils.SafeGet(fields, 8),

                            //n_unico_pezzo = ParserUtils.SafeParseUInt(fields, 9),

                            //n = ParserUtils.SafeParseShort(fields, 10),
                            //n_lav = ParserUtils.SafeParseShort(fields, 11),

                            //ordine = ParserUtils.SafeGet(fields, 12),
                            //cliente = ParserUtils.SafeGet(fields, 13),
                            //pian_trav1 = ParserUtils.SafeGet(fields, 14),
                            //pian_trav2 = ParserUtils.SafeGet(fields, 15),
                            //pian_trav3 = ParserUtils.SafeGet(fields, 16),
                            //rinf = ParserUtils.SafeGet(fields, 17),
                            //fissaggio = ParserUtils.SafeGet(fields, 18),
                            //cod_tip = ParserUtils.SafeGet(fields, 19),
                            //f_acqua1 = ParserUtils.SafeGet(fields, 20),
                            //f_acqua2 = ParserUtils.SafeGet(fields, 21),
                            //f_acqua3 = ParserUtils.SafeGet(fields, 22),
                            //note = ParserUtils.SafeGet(fields, 23),

                            //taglia_doppia = ParserUtils.SafeParseShort(fields,24),

                            //q = [ParserUtils.SafeParseFloat(fields, 25) / 10.0f, ParserUtils.SafeParseFloat(fields, 26) / 10.0f, ParserUtils.SafeParseFloat(fields, 27) / 10.0f, ParserUtils.SafeParseFloat(fields, 28) / 10.0f],

                            ////info1 = ParserUtils.SafeGet(fields, 27),
                            ////info2 = ParserUtils.SafeGet(fields, 28),
                            ////info3 = ParserUtils.SafeGet(fields, 29),
                            ////info4 = ParserUtils.SafeGet(fields, 30),
                            ////info5 = ParserUtils.SafeGet(fields, 31),

                            //dummy = new byte[20],
                            //pointers = new byte[8],
                        };
                        parsedRecords.Pezzi.Add(pezzo);
                        break;
                    case 'R':
                        var residuo = new DATI_PEZZO_RESTANTE
                        {
                            n_barra = 1,
                            dummy2 = 0,

                            l_ext = ParserUtils.SafeParseFloat(fields, 1) / 10.0f,
                            l_int = ParserUtils.SafeParseFloat(fields, 2) / 10.0f,
                            angt_sx = ParserUtils.SafeParseFloat(fields, 3) / 10.0f,
                            //angp_sx = ParserUtils.SafeParseFloat(fields, 4) / 10.0f,
                            angt_dx = ParserUtils.SafeParseFloat(fields, 4) / 10.0f,
                            //angp_dx = ParserUtils.SafeParseFloat(fields, 6) / 10.0f,

                            dummy = new byte[8],

                            // Antonio2.txt ha un campo "1" prima di rif cosa è?
                            rif = ParserUtils.SafeGet(fields, 5),
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
