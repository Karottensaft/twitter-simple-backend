using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace SweaterV1.Services.Extensions;

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.Claims
            .SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Sid).Value.ToString();
        return userId!;
    }
}