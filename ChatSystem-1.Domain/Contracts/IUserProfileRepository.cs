using ChatSystem_1.Domain.Entities.Models;

namespace ChatSystem_1.Domain.Contracts
{
    public interface IUserProfileRepository
    {
        Task<User> GetUserProfileAsync(string userId, bool trackChanges);
    }
}