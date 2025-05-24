using ChatSystem_1.Api.ActionFilters;
using ChatSystem_1.Application.DTOs;
using ChatSystem_1.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ChatSystem_1.Api.Controllers
{
[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase 
{
    private readonly IServiceManager _service;

    public TokenController(IServiceManager service)
    {
        _service = service;
    }
    [HttpPost("refresh")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
     var tokenDtoReturn = await _service.AuthenticationService.RefreshToken(tokenDto);

     return Ok(tokenDtoReturn);
    }
}
}