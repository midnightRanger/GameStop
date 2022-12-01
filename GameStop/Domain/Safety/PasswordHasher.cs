using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace GameStop.Models.Safety;

public static class PasswordHasher
{
   public static string HashPassword(string password)
   {
      using (var sha256 = SHA256.Create())
      {
         var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
         var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
         return hash; 
      }
   }
}
