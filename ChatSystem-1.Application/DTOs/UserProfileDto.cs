using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ChatSystem_1.Application.DTOs
{
    public record UserProfileDto
    {
        public string Firstname { get; init; } = string.Empty;
        public string Lastname { get; init; } = string.Empty;
        public int Matricnumber { get; init; }
        public string email { get; init; } = string.Empty;
        public string ProfileImageUrl { get; init; } = string.Empty;

    }
    public record UserUpdateProfileDto
    {
        public string Firstname { get; init; } = string.Empty;
        public string Lastname { get; init; } = string.Empty;
        public IFormFile? Image { get; init; }

    }

}