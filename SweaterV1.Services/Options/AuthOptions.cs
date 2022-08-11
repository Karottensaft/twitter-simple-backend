using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SweaterV1.Services.Options

{
    public class AuthOptions
    {
        public const string ISSUER = "Karottensaft";
            public const string AUDIENCE = "Sweater";
            const string KEY = "mysupersecret_secretkey!123";
            public const int LIFETIME = 1;
            public static SymmetricSecurityKey GetSymmetricSecurityKey()
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
            }
    }
}
