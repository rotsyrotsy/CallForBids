using CallForBids.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CallForBids.Data
{
    public class BidCsvReader
    {
        private readonly CallForBidsContext _context;
        public BidCsvReader(CallForBidsContext context)
        {
            _context = context;
        }
        public List<Bids> ReadCsvFile(string filePath)
        {
            var bids = new List<Bids>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var office = _context.Offices.Where(c => c.Code == csv.GetField<string>("Office")).FirstOrDefault();
                    var project = _context.Projects.Where(c => c.Poet == csv.GetField<string>("Poet")).FirstOrDefault();
                    var bid = new Bids
                    {
                        Title = csv.GetField<string>("Title"),
                        Number = csv.GetField<string>("Number"),
                        Description = csv.GetField<string>("Description"),
                        PublicationDate = csv.GetField<DateTime>("Publication"),
                        LimitDate = csv.GetField<DateTime>("Deadline"),
                        OfficeId = office.Id,
                        ProjectId = project.Id,
                        FiscalYear = csv.GetField<string>("FY")
                    };
                    bids.Add(bid);
                }
            }

            return bids;
        }
    }
}
