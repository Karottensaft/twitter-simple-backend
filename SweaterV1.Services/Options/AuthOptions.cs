using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SweaterV1.Services.Options;

public class AuthOptions
{
    public const string Issuer = "Karottensaft";
    public const string Audience = "Sweater";
    private const string Key = "mysupersecret_secretkey!123";
    public const int Lifetime = 30;

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}