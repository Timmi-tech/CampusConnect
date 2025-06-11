using ChatSystem_1.Application.DTOs;
using ChatSystem_1.Domain.Contracts;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ChatSystem_1.Application.DTOs
{
    public record CreatePostDto
    {
        public string Caption { get; set; } = string.Empty;
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
    public record PostDto
    {
        public int Id { get; set; }
        public string Caption { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
    public record UpdatePostDto
    {
        public string Caption { get; set; } = string.Empty;
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}