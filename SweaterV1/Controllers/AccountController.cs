using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Extensions;

namespace SweaterV1.WebAPI.Controllers;

[ApiController]
public class AccountController : Controller
{
    private readonly TokenMiddleware _tokenHelper;

    public AccountController(TokenMiddleware tokenHelper)
    {
        _tokenHelper = tokenHelper;
    }

    [HttpPost("user/token")]
    public async Task<TokenModel> Token(UserModelAutentificationDto data)
    {
        var response = await _tokenHelper.GetToken(data);
        if (response == null)
        {
            throw new Exception("");
        }
        return response;
    }
}