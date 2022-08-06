using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Services;

namespace SweaterV1.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "admin")]
    [HttpGet("users")]
    public async Task<IEnumerable<UserModel>> GetListAsync()
    {
        var user = await _userService.GerListOfEntities();
        return user;
    }

    [HttpGet("user/{id}")]
    public async Task<UserModelInformationDto> GetAsync(int id)
    {

        var user = await _userService.GetEntity(id);
        return user!;
    }

    [HttpPost("registration")]
    public async Task<UserModelRegistrationDto> Post(UserModelRegistrationDto user)
    {

        if (user == null) BadRequest();
        await _userService.CreateEntity(user!);
        return user!;
    }

    [Authorize]
    [HttpPut("user/{id}/user-settings")]
    public async Task<UserModelChangeDto> Put(UserModelChangeDto user, int id)
    {
        await _userService.UpdateEntity(user, id);
        return user;
    }

    [Authorize]
    [HttpDelete("user/{id}/deleter")]
    public async Task Delete(int id)
    {
        await _userService.DeleteEntity(id);
    }
}