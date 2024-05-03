using Common.Interfaces;
using System.Reflection;
using System.Security.Cryptography;

namespace Github.NetCoreWebApp.Infrastructure.Common.Helpers
{
    public class Utility : IUtility
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;
        public TU DeepCopy<T, TU>(T source, TU dest)
        {
            var sourceFields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();
            var destFields = typeof(TU).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();
            foreach (var sourceField in sourceFields)
            {
                if (destFields.Any(x => x.Name == sourceField.Name))
                {
                    var f = destFields.First(x => x.Name == sourceField.Name);
                    f.SetValue(dest, sourceField.GetValue(source));
                }
            }

            return dest;
        }
        public bool IsCoordinateInsideCircle(double lat1, double lon1, double lat2, double lon2, double radius)
        {
            double dLat = Math.PI * (lat2 - lat1) / 180.0;
            double dLon = Math.PI * (lon2 - lon1) / 180.0;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(Math.PI * lat1 / 180.0) * Math.Cos(Math.PI * lat2 / 180.0) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = 6371 * c; // Radius of the Earth in kilometers

            return distance <= radius;
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
