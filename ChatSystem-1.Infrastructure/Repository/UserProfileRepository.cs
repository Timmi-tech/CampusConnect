using ChatSystem_1.Domain.Contracts;
using ChatSystem_1.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem_1.Infrastructure.Repository
{
    public class UserProfileRepository : RepositoryBase<User>, IUserProfileRepository
    {
        public UserProfileRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<User> GetUserProfileAsync(string userId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(userId), trackChanges).SingleOrDefaultAsync();

        
    }
}