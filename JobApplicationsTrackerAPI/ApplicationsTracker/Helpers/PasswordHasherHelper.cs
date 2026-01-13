using Microsoft.AspNetCore.Identity;

namespace ApplicationsTracker.Helpers
{
    public static class PasswordHasherHelper
    {
        private static readonly PasswordHasher<string> _passwordHasher = new();

        public static string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null!, password);
        }
        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null!, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
