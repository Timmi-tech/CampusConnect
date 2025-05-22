using System;
namespace ChatSystem_1.Application.DTOs
{
    public record UserForRegistrationDto
    {
        public string Firstname {get; init;} = string.Empty;         
        public string Lastname {get; init;} = 
        string.Empty;
        public string Username {get; init;} = string.Empty;
        public string Password {get; init;}= string.Empty;
        public int MatricNumber {get; init;}    
        public string Email {get; init;} = string.Empty;
    }
} 