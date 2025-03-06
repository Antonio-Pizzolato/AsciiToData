using AsciiToData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiToData.Services
{
    class ValidationService
    {
        public Models.ValidationResult ValidateRecords(List<CMP265Record> records)
        {
            var result = new Models.ValidationResult();

            //Implementa i controlli a cascata
            ValidateBarsLengths(records, result);
            ValidateAngles(records, result);
            ValidateMandatoryFields(records, result);
            result.IsValid = result.Errors.Count == 0;
            return result;
        }

        private void ValidateBarsLengths(List<CMP265Record> records, Models.ValidationResult result)
        {
            var currentBar = default(BarDataRecord);
            var currentPieces = new List<PieceDataRecord>();

            foreach (var record in records)
            {
                if (record is BarDataRecord bar)
                {
                    if (currentBar is not null)
                    {
                        CheckBarLength(currentBar, currentPieces, result);
                    }
                    currentBar = bar;
                    currentPieces.Clear();
                }
                else if (record is PieceDataRecord piece && currentBar is not null)
                {
                    currentPieces.Add(piece);
                }
            }

            //Controlla l'ultima barra
            if (currentBar is not null)
            {
                CheckBarLength(currentBar, currentPieces, result);
            }
        }

        private void CheckBarLength(BarDataRecord bar, List<PieceDataRecord> pieces, Models.ValidationResult result)
        {
            var totalExLength = pieces.Sum(p => p.ExLength);

            if (totalExLength > bar.BarLength)
            {
                result.Errors.Add(
                    $"BAR {bar.Code}: Lunghezza totale pezzi ({totalExLength}) " +
                    $"supera lunghezza barra ({bar.BarLength})"
                );
            }
        }

        private void ValidateAngles(List<CMP265Record> records, Models.ValidationResult result)
        {
            foreach (var piece in records.OfType<PieceDataRecord>())
            {

                if (piece.LTAngle < -45 || piece.LTAngle > 45)
                {
                    result.Errors.Add(
                        $"PEZZO {piece.PieceID}: Angolo di inclinazione sinistra ({piece.LTAngle}) " +
                        $"fuori range (-45, 45)"
                    );
                }
                if (piece.LPAngle < -45 || piece.LPAngle > 45)
                {
                    result.Errors.Add(
                        $"PEZZO {piece.PieceID}: Angolo di pivot sinistra ({piece.LPAngle}) " +
                        $"fuori range (-45, 45)"
                    );
                }
                if (piece.RTAngle < -45 || piece.RTAngle > 45)
                {
                    result.Errors.Add(
                        $"PEZZO {piece.PieceID}: Angolo di inclinazione destra ({piece.RTAngle}) " +
                        $"fuori range (-45, 45)"
                    );
                }
                if (piece.RPAngle < -45 || piece.RPAngle > 45)
                {
                    result.Errors.Add(
                        $"PEZZO {piece.PieceID}: Angolo di pivot destra ({piece.RPAngle}) " +
                        $"fuori range (-45, 45)"
                    );
                }


            }
        }

        private void ValidateMandatoryFields(List<CMP265Record> records, Models.ValidationResult result)
        {
            foreach (var bar in records.OfType<BarDataRecord>())
            {
                if (string.IsNullOrEmpty(bar.Code))
                {
                    result.Errors.Add($"BAR: Codice mancante");
                }
            }
        }

    }

} 
