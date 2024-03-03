using Bramka.Server.Constans;
using Bramka.Server.Interfaces;
using Bramka.Shared.DTOs.UserDTO;
using Bramka.Shared.Models;
using Dapper;
using System.Data;

namespace Bramka.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IDbConnection _connection;
        public UserService(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }
        public async Task<Guid> CreateUserAsync(UserRegistrationDTO newItem)
        {
            var passwordHash = HashPassword(newItem.Password);

            var parameters = new DynamicParameters(new
            {
                newItem.Name,
                newItem.Surname,
                newItem.Email,
                newItem.PhoneNumber,
                newItem.BirthDate,
                PasswordHash = passwordHash,
                RoleId = await GetIdRoleAsync()
            }) ;

            parameters.Add("@UserId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            await _connection.ExecuteAsync(DataBaseConstants.CreateUser,
                parameters,
                commandType: CommandType.StoredProcedure);

            var userId = parameters.Get<Guid>("@UserId");
            return userId;
        }

        private async Task<int> GetIdRoleAsync()
        {
            return await _connection.ExecuteScalarAsync<int>(DataBaseConstants.GetRoleIdByName,
                new { Name = "user" }, commandType: CommandType.StoredProcedure);
        }

        private string HashPassword(string password)
        {
            if (password == null)
            {
                throw new NullReferenceException($"Error: password can not be null!");
            }
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            if (await CheckUserExistAsync(userId))
            {
                throw new InvalidOperationException("User does not exist.");

            }
            var row = await _connection.ExecuteAsync(DataBaseConstants.DeleteUser,
                new { UserId = userId }, commandType: CommandType.StoredProcedure);

            return row > 0;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {

            return await _connection.QueryAsync<User>(DataBaseConstants.GetAllUsers);
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            if (await CheckUserExistAsync(userId))
            {
                throw new InvalidOperationException("User does not exist.");

            }
            var user = await _connection.QueryFirstOrDefaultAsync<User>(DataBaseConstants.GetUserById,
                                                        new { UserId = userId },
                                                        commandType: CommandType.StoredProcedure);
            if (user == null)
            {
                throw new NullReferenceException($"Error error not found user for id");
            }
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _connection.QueryFirstAsync<User>(
                DataBaseConstants.GetUserByEmail,
                new { Email = email },
                commandType: CommandType.StoredProcedure);

            if ( user == null )
            {
                throw new NullReferenceException($"Error. Not found user by email");
            }
            return user;
        }

        public async Task<User?> GetLastUserAsync()
        {

            return await _connection.QueryFirstOrDefaultAsync<User>(DataBaseConstants.GetLastUser, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateUserAsync(Guid userId, UserEditDTO updateItem)
        {
            if (await CheckUserExistAsync(userId))
            {
                throw new InvalidOperationException("User does not exist.");

            }
            var rows = await _connection.ExecuteAsync(DataBaseConstants.UpdateUser,
                new
                {
                    UserId = userId,
                    updateItem.Name,
                    updateItem.Surname,
                    //updateItem.Email,
                    //PasswordHash = HashPassword(updateItem.Password),
                    updateItem.PhoneNumber,
                    updateItem.RoleId
                }, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<bool> UpdateRefreshToken(RefreshToken refreshToken, User user)
        {
            var rows = await _connection.ExecuteAsync(DataBaseConstants.UpdateRefreshToken,
            new
            {
                user.UserId,
                user.Email,
                RefreshToken = refreshToken.Token,
                TokenCreated = refreshToken.Created,
                TokenExpires = refreshToken.Expires
            }, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public bool IsEmailExist(string email)
        {
            try
            {
                var result = _connection.QueryFirstOrDefault<User>(DataBaseConstants.CheckExistEmail, new { Email = email }, commandType: CommandType.StoredProcedure);

                return result != null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking email existence: {ex.Message}");
            }
        }

        private async Task<bool> CheckUserExistAsync(Guid userId)
        {
            var user = await _connection.QueryFirstOrDefaultAsync(DataBaseConstants.GetUserById,
                        new { UserId = userId },
                        commandType: CommandType.StoredProcedure);

            return user == null;
        }
    }
}
