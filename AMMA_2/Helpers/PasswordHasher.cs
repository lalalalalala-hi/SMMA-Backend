using System.Security.Cryptography;

namespace AMMAAPI.Helpers
{
    public class PasswordHasher
    {
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private static readonly int SaltSize = 16;
        private static readonly int HashSize = 20;
        private static readonly int Iterations = 10000;

        public static string HashPassword(string password)
        {
            byte[] salt;
            rng.GetBytes(salt = new byte[SaltSize]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            byte[] hashBytes = new byte[SaltSize + HashSize];
            System.Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            System.Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return System.Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = System.Convert.FromBase64String(hashedPassword);

            byte[] salt = new byte[SaltSize];
            System.Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
