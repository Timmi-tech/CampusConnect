using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace ChatSystem_1.Application.DTOs
{
    public class FileUploadDto
    {
        [Required]
        public IFormFile? File { get; set; }
    }
}
