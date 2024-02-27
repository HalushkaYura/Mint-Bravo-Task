﻿using Bramka.Server.Constans;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Models;
using Dapper;
using System.Data;

namespace Bramka.Server.Services
{
    public class RoleService : IRoleService
    {
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();

                return await connection.QueryAsync<Role>(DataBaseConstants.GetAllRoles);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all roles: {ex.Message}");
            }
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();

                var role =  await connection.QueryFirstOrDefaultAsync<Role>(DataBaseConstants.GetRoleById,
                                                            new { RoleId = roleId },
                                                            commandType: CommandType.StoredProcedure);
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting  role: {ex.Message}");
            }
        }
    }
}
