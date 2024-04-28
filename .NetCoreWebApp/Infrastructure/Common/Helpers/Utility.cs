using Application.Interfaces;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Github.NetCoreWebApp.Infrastructure.Common.Helpers
{
    public class Utility : IUtility
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;
        public T DeepCopy<T>(T obj)
        {
            string json = JsonConvert.SerializeObject(obj);

            T copy = JsonConvert.DeserializeObject<T>(json);

            return copy;
        }
        public string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = PBKDF2(password, salt, Iterations, KeySize);

            byte[] hashBytes = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            byte[] storedHash = new byte[KeySize];
            Array.Copy(hashBytes, SaltSize, storedHash, 0, KeySize);

            byte[] computedHash = PBKDF2(password, salt, Iterations, KeySize);
            for (int i = 0; i < KeySize; i++)
            {
                if (computedHash[i] != storedHash[i])
                    return false;
            }
            return true;
        }

        private byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pbkdf2.GetBytes(outputBytes);
            }
        }
    }
}
