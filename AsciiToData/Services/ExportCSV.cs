using AsciiToData.Models;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AsciiToData.Services
{
    class ExportCSV
    {
        public void ExportToCsv(List<CMP265Record> records, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                var firstRecord = records.FirstOrDefault();
                if (firstRecord is null)
                    return;

                //var headers = GetHeaders(firstRecord);
                //writer.WriteLine(string.Join(",", headers));

                foreach (var record in records)
                {
                    var values = GetRecordValues(record);

                    // Unisci e pulisci le virgole finali
                    string line = string.Join(",", values).TrimEnd(',');

                    writer.WriteLine(line);
                }
            }
        }

        //private List<string> GetHeaders(CMP265Record record)
        //{
        //    // Ottieni le proprietà comuni a tutti i record
        //    var headers = new List<string>();
        //    var properties = record.GetType().GetProperties();

        //    return properties.Select(p => p.Name).ToList();
        //}

        private List<string> GetRecordValues(CMP265Record record)
        {
            var values = new List<string>();
            var properties = record.GetType().GetProperties();

            // Aggiungi prima il RecordType
            var recordTypeProperty = properties.First(p => p.Name == "RecordType");
            values.Add(recordTypeProperty.GetValue(record)?.ToString() ?? "");

            foreach (var prop in properties.Where(p => p.Name != "RecordType"))
            {
                var value = prop.GetValue(record);

                if (value is null)
                {
                    values.Add("");
                }
                else if(value is double d)
                {
                    values.Add(d.ToString("00"));
                }
                else if (value is int i)
                {
                    values.Add(i.ToString("00"));
                }
                else
                {        
                    values.Add(value.ToString());
                }
            }

            return values;
        }
    }
}
