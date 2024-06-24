// Data/UserRepository.cs
using CallForBids.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CallForBids.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


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

                var query = "INSERT INTO [Users] (Email, Password, RoleId,SupplierId) VALUES (@Email, @Password, @RoleId,@SupplierId)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password); // TODO Note: Hash passwords in a real app!
                    command.Parameters.AddWithValue("@RoleId", 1); // 1 is the default "SUPPLIER" Role ID 
                    command.Parameters.AddWithValue("@SupplierId", user.SupplierId);
                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }
                
            }
        }
        public async Task<int> RegisterSupplierAsync(Suppliers supplier)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();


                var query = @"
                INSERT INTO [Suppliers] (Name, SupplierNumber, Address,Phone, Email)
                VALUES (@Name,@SupplierNumber,@Address,@Phone,@Email);
                SELECT CAST(scope_identity() AS int)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", supplier.Name);
                    command.Parameters.AddWithValue("@SupplierNumber", supplier.SupplierNumber);
                    command.Parameters.AddWithValue("@Address", supplier.Address);
                    command.Parameters.AddWithValue("@Phone", supplier.Phone);
                    command.Parameters.AddWithValue("@Email", supplier.Email);
                    var insertedId = (int)await command.ExecuteScalarAsync();
                    return insertedId;
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
