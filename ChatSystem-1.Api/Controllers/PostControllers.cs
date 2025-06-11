using ChatSystem_1.Application.DTOs;
using ChatSystem_1.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatSystem_1.Api.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    [Authorize]
    public class PostControllers : ControllerBase
    {
        private readonly IServiceManager _service;

        public PostControllers(IServiceManager service)
        {
            _service = service;
        }

        // Create a post
        [HttpPost]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Create a post", Description = "Create a new post with images")]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto createPostDto)
        {
            if (createPostDto is null)
                return BadRequest("CreatePostDto object is null");

            var userId = GetUserIdFromClaims();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User is not authenticated or token is invalid." });

            var createdPost = await _service.PostService.CreatePostAsync(createPostDto, userId);

            return CreatedAtAction(nameof(GetPostById), new { postId = createdPost.Id }, createdPost);
        }

        // Get all posts
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _service.PostService.GetAllPostsAsync(trackChanges: false);
            return Ok(posts);
        }

        // Get a single post by ID
        [HttpGet("{postId:int}", Name = "GetPostById")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            var post = await _service.PostService.GetPostByIdAsync(postId, trackChanges: false);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        // Get posts for the authenticated user
        [HttpGet("me")]
        public async Task<IActionResult> GetMyPosts()
        {
            var userId = GetUserIdFromClaims();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User is not authenticated or token is invalid." });

            var posts = await _service.PostService.GetPostsByUserIdAsync(userId, trackChanges: false);
            return Ok(posts);
        }

        // Update a post
        [HttpPut("{postId:int}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdatePost(int postId, [FromForm] UpdatePostDto updatePostDto)
        {
            await _service.PostService.UpdatePostAsync(postId, updatePostDto, trackChanges: true);
            return NoContent();
        }

        // Delete a post
        [HttpDelete("{postId:int}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            await _service.PostService.DeletePostAsync(postId, trackChanges: false);
            return NoContent();
        }

        private string? GetUserIdFromClaims()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}