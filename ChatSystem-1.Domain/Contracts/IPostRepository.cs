using ChatSystem_1.Domain.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ChatSystem_1.Domain.Contracts
{
    public interface IPostRepository
    {
        void CreatePost(Post post);
        Task<IEnumerable<Post>> GetAllPostsAsync(bool trackChanges);
        Task<Post?> GetPostByIdAsync(int postId, bool trackChanges);
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId, bool trackChanges);
        void UpdatePost(Post post);
        void DeletePost(Post post);
    }
}