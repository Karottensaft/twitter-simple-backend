using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SweaterV1.Services.HelpingServices

{
    public class AuthOptions
    {
        public const string ISSUER = "Karottensaft"; // издатель токена
            public const string AUDIENCE = "Sweater"; // потребитель токена
            const string KEY = "testkey";   // ключ для шифрации
            public const int LIFETIME = 1; // время жизни токена - 1 минута
            public static SymmetricSecurityKey GetSymmetricSecurityKey()
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
            }
    }
}
