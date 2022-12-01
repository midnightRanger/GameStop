using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GameStop.Models;

public class AuthOptions {

    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "someSecretKeyEncryptor2122";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    
}