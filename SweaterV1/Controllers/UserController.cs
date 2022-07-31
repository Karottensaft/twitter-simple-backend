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

    [HttpGet]
    public async Task<IEnumerable<UserModel>> GetListAsync()
    {
        var user = await _userService.GerListOfEntities();
        return user;
    }

    [HttpGet("{id}")]
    public async Task<UserModelInformationDto> GetAsync(int id)
    {
        var user = await _userService.GetEntity(id);
        return user!;
    }

    [HttpPost]
    public async Task<UserModelRegistrationDto> Post(UserModelRegistrationDto user)
    {
        if (user == null) BadRequest();
        await _userService.CreateEntity(user!);
        return user!;
    }

    [HttpPut("{id}")]
    public async Task<UserModelChangeDto> Put(UserModelChangeDto user, int id)
    {
        await _userService.UpdateEntity(user, id);
        return user;
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _userService.DeleteEntity(id);
    }
}