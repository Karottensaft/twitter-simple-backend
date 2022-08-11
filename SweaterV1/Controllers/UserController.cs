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

    [Authorize(Roles = "admin")]
    [HttpGet("user/all")]
    public async Task<IEnumerable<UserModel>> GetListOfUsersAsync()
    {
        var user = await _userService.GerListOfEntities();
        return user;
    }

    [HttpGet("user/{userId}")]
    public async Task<UserModelInformationDto> GetUserAsync(int userId)
    {
        var user = await _userService.GetEntity(userId);
        return user;
    }

    [HttpPost("user/registration")]
    public async Task<UserModelRegistrationDto> PostUser(UserModelRegistrationDto user)
    {
        await _userService.CreateEntity(user);
        return user;
    }

    [Authorize]
    [HttpPut("user/{userId}/user-settings")]
    public async Task<UserModelChangeDto> PutUser(UserModelChangeDto user, int userId)
    {
        await _userService.UpdateEntity(user, userId);
        return user;
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("user/{userId}/delete")]
    public async Task DeleteUser(int userId)
    {
        await _userService.DeleteEntity(userId);
    }
}