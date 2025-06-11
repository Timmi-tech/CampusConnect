using ChatSystem_1.Application.DTOs;
using ChatSystem_1.Domain.Contracts;
using ChatSystem_1.Domain.Entities.Models;
using ChatSystem_1.Application.Services.Contracts;
using ChatSystem_1.Application.Services;
using AutoMapper;
using ChatSystem_1.Domain;


namespace ChatSystem_1.Application.Services
{
    internal class PostService : IPostService
    {
        private readonly IRepositoryManager? _repository;
        private readonly ILoggerManager? _logger;
        private readonly IPhotoService? _photoService;
        private readonly IMapper _mapper;

        public PostService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IPhotoService photoService)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _photoService = photoService;

        }

        public async Task<PostDto> CreatePostAsync(CreatePostDto postDto, string userId)
        {
            if (postDto.Images == null || !postDto.Images.Any())
                throw new ArgumentException("At least one image is required.", nameof(postDto.Images));

            // Upload images
            var uploadResults = await _photoService.UploadImagesAsync(postDto.Images);

            // Map DTO to entity
            var postEntity = _mapper.Map<Post>(postDto);

            // Set the authenticated user's ID
            postEntity.UserId = userId;

            // Add uploaded image URLs to PostImages
            postEntity.PostImages = uploadResults
                .Where(r => r != null && !string.IsNullOrEmpty(r.Url?.ToString()))
                .Select(r => new PostImage { ImageUrl = r.Url.ToString() })
                .ToList();

            // Save the post to the database
            _repository.Post.CreatePost(postEntity);
            await _repository.SaveAsync();

            // Map and return the Post DTO (use a PostDto, not CreatePostDto)
            var postToReturn = _mapper.Map<PostDto>(postEntity);
            return postToReturn;
        }
        
        public async Task<IEnumerable<PostDto>> GetAllPostsAsync(bool trackChanges)
        {
            var postsFromDb = await _repository.Post.GetAllPostsAsync(trackChanges);
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(postsFromDb);
            return postsDto;
        }

        public async Task<PostDto?> GetPostByIdAsync(int postId, bool trackChanges)
        {
            var postFromDb = await _repository.Post.GetPostByIdAsync(postId, trackChanges);
            if (postFromDb == null)
                return null;
            var postDto = _mapper.Map<PostDto>(postFromDb);
            return postDto;
        }

        public async Task<IEnumerable<PostDto>> GetPostsByUserIdAsync(string userId, bool trackChanges)
        {
            var postsFromDb = await _repository.Post.GetPostsByUserIdAsync(userId, trackChanges);
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(postsFromDb);
            return postsDto;
        }

        public async Task UpdatePostAsync(int postId, UpdatePostDto updatePostDto, bool trackChanges)
        {
            var postFromDb = await _repository.Post.GetPostByIdAsync(postId, trackChanges);
            if (postFromDb == null)
                throw new KeyNotFoundException("Post not found");

            _mapper.Map(updatePostDto, postFromDb);
            _repository.Post.UpdatePost(postFromDb);
            await _repository.SaveAsync();
        }

        public async Task DeletePostAsync(int postId, bool trackChanges)
        {
            var postFromDb = await _repository.Post.GetPostByIdAsync(postId, trackChanges);
            if (postFromDb == null)
                throw new KeyNotFoundException("Post not found");

            _repository.Post.DeletePost(postFromDb);
            await _repository.SaveAsync();
        }
    }
}