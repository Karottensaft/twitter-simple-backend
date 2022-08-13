using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Services;

namespace SweaterV1.WebAPI.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("user/token")]
    public async Task<TokenModel> GetToken(UserModelAuthDto user)
    {
        return await _userService.GetToken(user);
    }

    [Authorize(Roles = "admin")]
    [HttpGet("user/all")]
    public async Task<IEnumerable<UserModel>> GetListOfUsers()
    {
        var user = await _userService.GerListOfUsers();
        return user;
    }

    [HttpGet("user/me")]
    public async Task<UserModelInformationDto> GetCurrentUser()
    {
        var user = await _userService.GetCurrentUser();
        return user;
    }

    [HttpGet("user/{username}")]
    public async Task<UserModelInformationDto> GetUser(string username)
    {
        var user = await _userService.GetUserByUsername(username);
        return user;
    }

    [AllowAnonymous]
    [HttpPost("user/registration")]
    public async Task<UserModelRegistrationDto> PostUser(UserModelRegistrationDto user)
    {
        await _userService.CreateUser(user);
        return user;
    }

    [Authorize]
    [HttpPut("user/user-settings")]
    public async Task<UserModelChangeDto> PutUser(UserModelChangeDto user)
    {
        await _userService.UpdateUser(user);
        return user;
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("user/delete")]
    public async Task DeleteUser(int userId)
    {
        await _userService.DeleteUser(userId);
    }
}