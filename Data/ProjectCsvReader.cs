using CallForBids.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CallForBids.Data
{
    public class ProjectCsvReader
    {
        public List<Projects> ReadCsvFile(string filePath)
        {
            var projects = new List<Projects>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ", ",
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                projects = csv.GetRecords<Projects>().ToList();
            }

            return projects;
        }
    }
}
