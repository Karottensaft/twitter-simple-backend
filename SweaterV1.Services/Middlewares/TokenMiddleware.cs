using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Options;
using SweaterV1.Services.Services;

namespace SweaterV1.Services.Middlewares;

public class TokenMiddleware
{
    private readonly UserService _userService;

    public TokenMiddleware(UserService userService)
    {
        _userService = userService;
    }

    public async Task<TokenModel> GetToken(UserModelAuthDto user)
    {
        var userLogIn = await _userService.ValidateUser(user);
        var identity = GetIdentity(userLogIn);

        if (identity == null) throw new ArgumentNullException(nameof(identity), "User was null.");

        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            AuthOptions.Issuer,
            AuthOptions.Audience,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new TokenModel
        {
            AccessToken = encodedJwt
        };

        return response;
    }

    public static ClaimsIdentity GetIdentity(UserModelLoginDto data)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, data.Username),
            new(ClaimsIdentity.DefaultRoleClaimType, data.Role),
            new(JwtRegisteredClaimNames.Sid, data.UserId.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }
}