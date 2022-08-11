using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Extensions;
using SweaterV1.Services.Services;

namespace SweaterV1.WebAPI.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserProvider _userProvider;
    private readonly UserService _userService;

    public UserController(UserService userService, IUserProvider userProvider)
    {
        _userService = userService;
        _userProvider = userProvider;
    }

    [Authorize(Roles = "admin")]
    [HttpGet("user/all")]
    public async Task<IEnumerable<UserModel>> GetListOfUsersAsync()
    {
        var user = await _userService.GerListOfEntities();
        return user;
    }

    [HttpGet("user/me")]
    public async Task<UserModelInformationDto> GetUserAsync()
    {
        var userId = _userService.GetUserId();
        var user = await _userService.GetEntity(userId);
        return user;
    }

    [HttpGet("user/{username}")]
    public async Task<UserModelInformationDto> GetOtherUserAsync(string username)
    {
        var user = await _userService.GetEntityByName(username);
        return user;
    }

    [HttpPost("user/registration")]
    public async Task<UserModelRegistrationDto> PostUser(UserModelRegistrationDto user)
    {
        await _userService.CreateEntity(user);
        return user;
    }

    [Authorize]
    [HttpPut("user/user-settings")]
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