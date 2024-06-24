
using CallForBids.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Drawing.Printing;

namespace CallForBids.Data;

public class BidsRepository
{
    private readonly string _connectionString;

    public BidsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("CallForBidsContext");
    }
    public async Task<int> GetTotalBidsesCountAsync(string searchTerm = null, int? officeId = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var query = @"
            SELECT COUNT(*)
            FROM Bids
            WHERE (@SearchTerm IS NULL OR Title LIKE '%' + @SearchTerm + '%')
            AND (@OfficeId IS NULL OR OfficeId = @OfficeId)";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SearchTerm", (object)searchTerm ?? DBNull.Value);
                command.Parameters.AddWithValue("@OfficeId", (object)officeId ?? DBNull.Value);

                var totalItems = (int)await command.ExecuteScalarAsync();
                return totalItems;
            }
        }
    }
    public async Task<IEnumerable<Bids>> GetBidsesAsync(int pageNumber, int pageSize, string searchTerm = null, int? officeId = null)
    {
        var Bidses = new List<Bids>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var query = @"
            SELECT b.Id, b.PublicationDate, b.LimitDate, b.Title, b.Number, b.Description, b.FiscalYear, o.Location, o.Address, p.Title, b.IsAvailable
            FROM Bids b
            JOIN Offices o ON b.OfficeId = o.Id
            JOIN Projects p ON b.ProjectId = p.Id
            WHERE (@SearchTerm IS NULL OR b.Title LIKE '%' + @SearchTerm + '%')
            AND (@OfficeId IS NULL OR b.OfficeId = @OfficeId)
            ORDER BY b.Id 
            OFFSET @Offset ROWS 
            FETCH NEXT @PageSize ROWS ONLY";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                command.Parameters.AddWithValue("@PageSize", pageSize);
                command.Parameters.AddWithValue("@SearchTerm", (object)searchTerm ?? DBNull.Value);
                command.Parameters.AddWithValue("@OfficeId", (object)officeId ?? DBNull.Value);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Bidses.Add(new Bids
                        {
                            Id = reader.GetInt32(0),
                            PublicationDate = reader.GetDateTime(1),
                            LimitDate = reader.GetDateTime(2),
                            Title = reader.GetString(3),
                            Number = reader.GetString(4),
                            Description = reader.GetString(5),
                            FiscalYear = reader.GetString(6),
                            Location = reader.GetString(7),
                            Address = reader.GetString(8),
                            ProjectTitle = reader.GetString(9),
                            IsAvailable = reader.GetByte(10) != 0,
                        });
                    }
                }
            }
        }

        return Bidses;
    }
    //public async Task<Bids> GetBidsByIdAsync(int BidsId)
    //{
    //    Bids? Bids = null;
    //    using (var connection = new SqlConnection(_connectionString))
    //    {
    //        await connection.OpenAsync();

    //        var query = @"
    //        SELECT d.Id, d.Name, d.Description, d.Price, d.CategoryId, c.Name AS CategoryName, d.IsAvailable
    //        FROM Bids d
    //        JOIN Category c ON d.CategoryId = c.Id
    //        WHERE d.Id = @BidsId
    //        ORDER BY d.Id ";

    //        using (var command = new SqlCommand(query, connection))
    //        {
    //            command.Parameters.AddWithValue("@BidsId", BidsId);
    //            using (var reader = await command.ExecuteReaderAsync())
    //            {
    //                while (await reader.ReadAsync())
    //                {
    //                    Bids = new Bids
    //                    {
    //                        Id = reader.GetInt32(0),
    //                        Name = reader.GetString(1),
    //                        Description = reader.GetString(2),
    //                        Price = reader.GetDecimal(3),
    //                        CategoryId = reader.GetInt32(4),
    //                        CategoryName = reader.GetString(5),
    //                        IsAvailable = reader.GetBoolean(6)
    //                    };
    //                }
    //            }
    //        }
    //    }

    //    return Bids;
    //}

    //public async Task AddBidsAsync(Bids Bids)
    //{
    //    using (var connection = new SqlConnection(_connectionString))
    //    {
    //        await connection.OpenAsync();

    //        var query = "INSERT INTO Bids (Name, Description, Price) VALUES (@Name, @Description, @Price)";
    //        using (var command = new SqlCommand(query, connection))
    //        {
    //            command.Parameters.AddWithValue("@Name", Bids.Name);
    //            command.Parameters.AddWithValue("@Description", Bids.Description);
    //            command.Parameters.AddWithValue("@Price", Bids.Price);

    //            await command.ExecuteNonQueryAsync();
    //        }
    //    }
    //}
}

