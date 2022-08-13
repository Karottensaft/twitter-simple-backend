using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Options;

namespace SweaterV1.Services.Middlewares;

public class TokenMiddleware
{
    private readonly IMapper _mapper;
    public TokenMiddleware(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TokenModel GetToken(UserModelLoginDto user)
    {
        var userMapped = user;
        var identity = GetIdentity(_mapper.Map <UserModelLoginDto> (userMapped));

        if (identity == null) throw new ArgumentNullException(nameof(identity), "User was null.");

        var now = DateTime.UtcNow;

        var jwtSecurityToken = new JwtSecurityToken(
            AuthOptions.Issuer,
            AuthOptions.Audience,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
        var encodedJwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        var response = new TokenModel
        {
            AccessToken = encodedJwtToken
        };

        return response;
    }

    public static ClaimsIdentity GetIdentity(UserModelLoginDto data)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, data.Username),
            new(ClaimsIdentity.DefaultRoleClaimType, data.Role),
            new Claim(JwtRegisteredClaimNames.Sid, data.UserId.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }
}