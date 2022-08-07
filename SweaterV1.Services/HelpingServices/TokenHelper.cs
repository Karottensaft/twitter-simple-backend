using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Services;

namespace SweaterV1.Services.HelpingServices
{
    public class TokenHelper
    {
        private readonly UserService _userService;

        public TokenHelper(UserService userService)
        {
            _userService = userService;
        }

        public async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            UserModelLoginDto userLogIn = await _userService.LoginAsync(login, password);
            if (userLogIn != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userLogIn.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, userLogIn.Role)
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        public async Task<TokenModel> GetToken(string username, string password)
        {
            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                return new TokenModel();
            }
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new TokenModel()
            {
                access_token = encodedJwt,
                username = identity.Name,
            };
            return response;
    }
    }
}
