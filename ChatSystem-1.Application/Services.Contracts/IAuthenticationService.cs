using ChatSystem_1.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace ChatSystem_1.Application.Services.Contracts
{
    
    
    public interface IAuthenticationService
    { 
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration); 
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
    } 
}