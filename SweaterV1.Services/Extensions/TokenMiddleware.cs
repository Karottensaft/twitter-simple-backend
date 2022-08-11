using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Options;
using SweaterV1.Services.Services;

namespace SweaterV1.Services.Extensions;

public class TokenMiddleware
{
    private readonly UserService _userService;

    public TokenMiddleware(UserService userService)
    {
        _userService = userService;
    }

    public async Task<TokenModel> GetToken(UserModelAuthDto data)
    {
        var userLogIn = await _userService.ValidateUser(data);
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
        //var userPayload = new { id = userLogIn.UserId };
        //jwt.Payload["user"] = userPayload;
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new TokenModel
        {
            AccessToken = encodedJwt,
            //Name = identity.Name!
        };

        return response;
    }

    public static ClaimsIdentity GetIdentity(UserModelLoginDto data)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, data.Username),
            new(ClaimsIdentity.DefaultRoleClaimType, data.Role),
            new Claim(JwtRegisteredClaimNames.Sid, data.UserId.ToString()),
        };
        
        var claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }
}