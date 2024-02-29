using Bramka.Shared.DTOs.UserDTO;
using Bramka.Shared.Models;

namespace Bramka.Server.Interfaces
{
    public interface IUserService 
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<Guid> CreateUserAsync(UserRegistrationDTO newItem);
        Task<bool> UpdateUserAsync(Guid id, UserEditDTO updateItem);
        Task<User> GetLastUserAsync();
        Task<bool> DeleteUserAsync(Guid id);
        bool IsEmailExist(string email);

        //----------------------------------------------------------
        //Task UploadAvatar(UserImageUploadDTO imageDTO, string userId);
        //Task<string> GetUserImageAsync(string userId);
    }
}
