using ChatSystem_1.Application.DTOs;

namespace ChatSystem_1.Application.Services.Contracts
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> GetUserProfileAsync(string userId, bool trackChanges);
        Task UpdateProfileAsync(string userId, UserUpdateProfileDto userUpdateProfileDto, bool trackChanges);
    }
}