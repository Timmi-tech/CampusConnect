using ChatSystem_1.Domain.Entities.Models;
using ChatSystem_1.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatSystem_1.Infrastructure.Repository
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreatePost(Post post) => Create(post);

        public async Task<IEnumerable<Post>> GetAllPostsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .Include(p => p.User)
                .Include(p => p.PostImages)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

        public async Task<Post?> GetPostByIdAsync(int postId, bool trackChanges) =>
            await FindByCondition(p => p.Id == postId, trackChanges)
                .Include(p => p.User)
                .Include(p => p.PostImages)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId, bool trackChanges) =>
            await FindByCondition(p => p.UserId == userId, trackChanges)
                .Include(p => p.User)
                .Include(p => p.PostImages)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

        public void UpdatePost(Post post) => Update(post);

        public void DeletePost(Post post) => Delete(post);
    }
}