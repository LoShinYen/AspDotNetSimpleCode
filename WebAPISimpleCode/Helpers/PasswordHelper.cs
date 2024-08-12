using System.Security.Cryptography;

namespace WebAPISimpleCode.Helpers
{
    public static class PasswordHelper
    {
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var combinedPassword = password + salt;
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedPassword));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            string hashOfEnteredPassword = HashPassword(enteredPassword, storedSalt);
            return hashOfEnteredPassword == storedHash;
        }
    }
}
