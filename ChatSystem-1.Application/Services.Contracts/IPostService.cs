using ChatSystem_1.Application.DTOs;
using ChatSystem_1.Domain.Contracts;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ChatSystem_1.Application.Services.Contracts
{
    public interface IPostService
    {
        Task<PostDto> CreatePostAsync(CreatePostDto createPostDto, string userId);
        Task<IEnumerable<PostDto>> GetAllPostsAsync(bool trackChanges);
        Task<PostDto?> GetPostByIdAsync(int postId, bool trackChanges);
        Task<IEnumerable<PostDto>> GetPostsByUserIdAsync(string userId, bool trackChanges);
        Task UpdatePostAsync(int postId, UpdatePostDto updatePostDto, bool trackChanges);
        Task DeletePostAsync(int postId, bool trackChanges);
    }
}