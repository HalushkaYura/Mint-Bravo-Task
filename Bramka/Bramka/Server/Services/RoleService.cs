using Bramka.Server.Constans;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Models;
using Dapper;
using System.Data;

namespace Bramka.Server.Services
{
    public class RoleService : IRoleService
    {
        private readonly IDbConnection _connection;
        public RoleService(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _connection.QueryAsync<Role>(DataBaseConstants.GetAllRoles);
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            var role = await _connection.QueryFirstOrDefaultAsync<Role>(DataBaseConstants.GetRoleById,
                                                        new { RoleId = roleId },
                                                        commandType: CommandType.StoredProcedure);
            return role!;
        }
    }
}
