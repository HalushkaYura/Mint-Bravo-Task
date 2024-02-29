﻿using Bramka.Server.Constans;
using Bramka.Server.Interfaces;
using Bramka.Shared.DTOs.UserDTO;
using Bramka.Shared.Models;
using Dapper;
using System.Data;

namespace Bramka.Server.Services
{
    public class UserService : IUserService
    {
        public async Task<Guid> CreateUserAsync(UserRegistrationDTO newItem)
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
                newItem.BirthDate,
                PasswordHash = passwordHash,
                newItem.RoleId
            });

            parameters.Add("@UserId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(DataBaseConstants.CreateUser,
                parameters,
                commandType: CommandType.StoredProcedure);

            var userId = parameters.Get<Guid>("@UserId");
            return userId;
        }
        private string HashPassword(string password)
        {
            if (password == null)
            {
                throw new NullReferenceException($"Error: password can not be null!");
            }
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public async Task<bool> DeleteUserAsync(Guid id)
        {

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            var row = await connection.ExecuteAsync(DataBaseConstants.DeleteUser,
                new { UserId = id }, commandType: CommandType.StoredProcedure);

            return row > 0;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            return await connection.QueryAsync<User>(DataBaseConstants.GetAllUsers);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
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

        public async Task<User?> GetLastUserAsync()
        {

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();


            return await connection.QueryFirstOrDefaultAsync<User>(DataBaseConstants.GetLastUser, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateUserAsync(Guid id, UserEditDTO updateItem)
        {

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            var rows = await connection.ExecuteAsync(DataBaseConstants.UpdateUser,
                new
                {
                    UserId = id,
                    updateItem.Name,
                    updateItem.Surname,
                    //updateItem.Email,
                    //PasswordHash = HashPassword(updateItem.Password),
                    updateItem.PhoneNumber,
                    updateItem.RoleId
                }, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public bool IsEmailExist(string email)
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                connection.Open();

                var result = connection.QueryFirstOrDefault<User>(DataBaseConstants.CheckExistEmail, new { Email = email }, commandType: CommandType.StoredProcedure);

                return result != null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking email existence: {ex.Message}");
            }
        }
    }
}
