using CallForBids.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;
namespace CallForBids.Data
{
    public class SubmissionRepository
    {
        private readonly string _connectionString;

        public SubmissionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CallForBidsContext");
        }

        public async Task<bool> insertSubmissionAsync(Submissions sub)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "INSERT INTO [Submissions] (BidId, UserId, Date, State, Description) VALUES (@BidId, @UserId, @Date, @State, @Description)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BidId", sub.BidId);
                    command.Parameters.AddWithValue("@UserId", sub.UserId);
                    command.Parameters.AddWithValue("@Date", sub.Date);
                    command.Parameters.AddWithValue("@State", sub.State); 
                    command.Parameters.AddWithValue("@Description", sub.Description); 

                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
        }

        public async Task<List<Submissions>> GetSubmissionsAsync(int userId)
        {
            var subms = new List<Submissions>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                        SELECT s.Id, s.BidId, s.UserId, s.Date, s.State, s.Description, b.Title
                        FROM Submissions s
                        JOIN Bids b on b.Id = s.BidId
                        WHERE UserId = @UserId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            subms.Add(new Submissions
                            {
                                Id = reader.GetInt32(0),
                                BidId = reader.GetInt32(1),
                                UserId = reader.GetInt32(2),
                                Date = reader.GetDateTime(3),
                                State = reader.GetByte(4),
                                Description = reader.GetString(5),
                                BidTitle = reader.GetString(6),

                            });
                        }
                    }
                }
            }
            return subms;
        }


    }
}
