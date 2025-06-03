using ChatSystem_1.Domain.Contracts;
using ChatSystem_1.Domain.Entities.Models;

namespace ChatSystem_1.Domain.Contracts
{
    public interface IRepositoryManager
    {
        IUserProfileRepository User {get;}
        Task SaveAsync();
    }
}