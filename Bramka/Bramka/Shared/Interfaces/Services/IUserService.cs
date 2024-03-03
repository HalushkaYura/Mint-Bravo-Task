﻿using Bramka.Shared.DTOs.UserDTO;
using Bramka.Shared.Models;

namespace Bramka.Server.Interfaces
{
    public interface IUserService 
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByEmailAsync(string email);
        Task<Guid> CreateUserAsync(UserRegistrationDTO newItem);
        Task<bool> UpdateUserAsync(Guid id, UserEditDTO updateItem);
        Task<bool> UpdateRefreshToken(RefreshToken refreshToken, User user);
        Task<User> GetLastUserAsync();
        Task<bool> DeleteUserAsync(Guid id);
        bool IsEmailExist(string email);

        //----------------------------------------------------------
        //Task UploadAvatar(UserImageUploadDTO imageDTO, string userId);
        //Task<string> GetUserImageAsync(string userId);
    }
}
