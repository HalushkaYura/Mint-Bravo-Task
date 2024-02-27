using Bramka.Server.Interfaces;
using Bramka.Shared.Models;

namespace Bramka.Shared.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(int id);
        //Task<bool> CreateRole(Role newRole);
        //Task<bool> UpdateRole(int id, Role updateRole);
    }
}
