using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BackendServices.Helper.Utils
{
    public static class PasswordUtils
    {
        private static readonly PasswordHasher<object> _hasher  = new PasswordHasher<object>();

        public static string HashPassword(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string password, string palainPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, password, palainPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
