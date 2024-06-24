// Data/UserRepository.cs
using CallForBids.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CallForBids.Models;


namespace CallForBids.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CallForBidsContext");
        }

        public async Task<bool> RegisterUserAsync(Users user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "INSERT INTO [Users] (Email, Password, RoleId) VALUES (@Email, @Password, @RoleId)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", user.Email);
                    //command.Parameters.AddWithValue("@Phone", user.Phone);
                    command.Parameters.AddWithValue("@Password", user.Password); // TODO Note: Hash passwords in a real app!
                    command.Parameters.AddWithValue("@RoleId", 1); // 1 is the default "CUSTOMER" Role ID 

                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
        }

        public async Task<Users> LoginUserAsync(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT Id, Email, Password FROM [Users] WHERE Email = @Email AND Password = @Password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password); // TODO Note: Hash passwords in a real app!

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Users
                            {
                                Id = reader.GetInt32(0),
                                Email = reader.GetString(1)
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
