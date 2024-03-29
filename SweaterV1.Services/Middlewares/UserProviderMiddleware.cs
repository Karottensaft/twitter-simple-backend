﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SweaterV1.Services.Middlewares;

public class UserProviderMiddleware : IUserProviderMiddleware
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserProviderMiddleware(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public int GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.Claims
            .SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Sid)
            .Value.ToString();
        return int.Parse(userId);
    }

    public string GetUsername()
    {
        var username = _httpContextAccessor.HttpContext?.User.Claims
            .SingleOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)
            .Value;
        return username;
    }
}