using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.IdentityModel.Tokens;
using SweaterV1.Domain.Models;
using SweaterV1.Services.HelpingServices;
using SweaterV1.Services.Services;
using SweaterV1.Services.HelpingServices;

namespace SweaterV1.WebAPI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        private readonly TokenHelper _tokenHelper;

        public AccountController(UserService userService, TokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("/token")]
        public async Task<TokenModel> Token(string username, string password)
        {
            var response = await _tokenHelper.GetToken(username, password);
            return response;
        }
    }
}
