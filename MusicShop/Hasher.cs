using System.Security.Cryptography;
using System.Text;

namespace MusicShop
{
    public class Hasher
    {
        const int keySize = 64;
        const int iterations = 350000;
        static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public static string HashPasword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToBase64String(hash) + "$" + Convert.ToBase64String(salt);
        }

        static public bool VerifyPassword(string password, string hash, string salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, Convert.FromBase64String(salt), iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromBase64String(hash));
        }
    }
}
