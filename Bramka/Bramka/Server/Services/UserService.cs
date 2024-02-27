using Bramka.Server.Constans;
using Bramka.Server.Interfaces;
using Bramka.Shared.DTOs.UserDTO;
using Bramka.Shared.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Bramka.Server.Services
{
    public class UserService : IUserService
    {
        public async Task<Guid> CreateUserAsync(UserRegistrationDTO newItem)
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();

                var passwordHash = HashPassword(newItem.Password);

                var parameters = new DynamicParameters(new
                {
                    newItem.Name,
                    newItem.Surname,
                    newItem.Email,
                    newItem.PhoneNumber,
                    PasswordHash = passwordHash,
                    CreatedAt = DateTime.Now,
                    newItem.RoleId
                });

                parameters.Add("@UserId", dbType: DbType.Guid, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(DataBaseConstants.CreateUser,
                    parameters,
                    commandType: CommandType.StoredProcedure);

                var userId = parameters.Get<Guid>("@UserId");
                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating user: {ex.Message}");
            }
        }
        private string HashPassword(string password)
        {
            if(password == null) 
            {
                throw new NullReferenceException($"Error: password can not be null!");
            }
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();

                    var row = await connection.ExecuteAsync(DataBaseConstants.DeleteUser,
                        new { UserId = id }, commandType: CommandType.StoredProcedure);

                    return row > 0;
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting user: {ex.Message}");
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();

                return await connection.QueryAsync<User>(DataBaseConstants.GetAllUsers);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all users: {ex.Message}");
            }
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
           try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();

                var user = await connection.QueryFirstOrDefaultAsync<User>(DataBaseConstants.GetUserById,
                                                            new { UserId = id },
                                                            commandType: CommandType.StoredProcedure);
                if (user == null)
                {
                    throw new NullReferenceException($"Error error not found user for id");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error not found user for id: {ex.Message}");
            }
        }

        public async Task<User> GetLastUserAsync()
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();


                return await connection.QueryFirstOrDefaultAsync<User>(DataBaseConstants.GetLastUser, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all users: {ex.Message}");
            }
        }

        public async Task<bool> UpdateUserAsync(Guid id, UserEditDTO updateItem)
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();

                var rows =  await connection.ExecuteAsync(DataBaseConstants.UpdateUser, 
                    new { UserId = id,
                        updateItem.Name,
                        updateItem.Surname,
                        updateItem.Email,
                        updateItem.PhoneNumber,
                        updateItem.RoleId
                    }, commandType: CommandType.StoredProcedure);
                return rows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating users: {ex.Message}");
            }
        }

        public bool IsEmailExist(string email)
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                connection.Open();

                var result =  connection.QueryFirstOrDefault<User>(DataBaseConstants.CheckExistEmail, new { Email = email }, commandType: CommandType.StoredProcedure);

                return result != null; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking email existence: {ex.Message}");
            }
        }
    }
}
