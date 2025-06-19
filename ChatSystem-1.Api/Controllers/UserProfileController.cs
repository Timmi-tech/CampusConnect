using Microsoft.AspNetCore.Mvc;
using ChatSystem_1.Application.DTOs;
using ChatSystem_1.Application.Services.Contracts;
using ChatSystem_1.Api.ActionFilters;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ChatSystem_1.Api.Controllers
{
    [Route("api/UserProfile")]
    [ApiController]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly IServiceManager _service;

        public UserProfileController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userId = GetUserIdFromClaims();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User is not authenticated or token is invalid." });

            var user = await _service.UserProfileService.GetUserProfileAsync(userId, trackChanges: false);return Ok(user);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "User ID is required." });
            var user = await _service.UserProfileService.GetUserProfileAsync(id, trackChanges: false);
            if (user == null)
                return NotFound(new { message = "User not found." });
        return Ok(user);
        }
        [HttpPut]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(
        Summary = "Uploads a photo",
        Description = "Upload an image file"
        )]
        public async Task<IActionResult> UpdateUserProfile([FromForm] UserUpdateProfileDto userUpdateProfile)
        {
            if (userUpdateProfile is null)
            {
                return BadRequest("UserProfileForUpdateDto object is null");
            }
            var userId = GetUserIdFromClaims();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User is not authenticated or token is invalid." });

            await _service.UserProfileService.UpdateProfileAsync(userId, userUpdateProfile, trackChanges: true);

            return NoContent();
        }

        private string? GetUserIdFromClaims()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
