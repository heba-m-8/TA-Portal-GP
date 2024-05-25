using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TAManagment.Application.Dtos;
using TAManagment.Application.IServices;
using TAManagment.Infrastructure.Services;

namespace TAManagment.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService; 
    }


    [HttpPost]
    [Route("Logiin")]
    [OpenApiOperation("Login")]
    public async Task<string> Authentication([FromBody] LoginDto loginDto)
    {
        var token = await _authService.Authentication(loginDto);
        return token; 
    }


    [HttpPost]
    [Route("Register")]
    [OpenApiOperation("Register")]

    public async Task<int> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _authService.Register(registerDto);
        return result;
    }


    [HttpPost]
    [Route("ChangePassword")]
    [OpenApiOperation("ChangePassword")]
    public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var newPassword = await _authService.ChangePassword(changePasswordDto);
        return newPassword;
    }

    [HttpGet]
    [Route("GetUserDetails")]
    [OpenApiOperation("GetUserDetails")]
    public async Task<UserDto> GetUserDetails(int userId)
    {
        var user = await _authService.GetUserDetails(userId);
        return user;
    }


    [HttpGet]
    [Route("CheckPassowrd")]
    [OpenApiOperation("CheckPassowrd")]
    public Task<bool> CheckPassowrd(int userId, string password)
    {
        var isExist =  _authService.CheckPassowrd(userId, password);
        return isExist;
    }

}
