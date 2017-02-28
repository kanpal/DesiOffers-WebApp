using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebLogic.Authentication
{
    class PasswordHasher
    {
        private const int SaltByteSize = 0x10;
        private const int HashByteSize = 0x10;
        private const int HasingIterationsCount = 0x3e8;

        public static string HashPassword(string password)
        {
            // Variation of original
            //http://stackoverflow.com/questions/20621950/asp-net-identity-default-password-hasher-how-does-it-work-and-is-it-secure

            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, SaltByteSize, HasingIterationsCount))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(SaltByteSize + HashByteSize);
            }
            byte[] dst = new byte[(SaltByteSize + 2 * HashByteSize) + 1];
            Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSize);
            Buffer.BlockCopy(buffer2, 0, dst, SaltByteSize + 1, 2 * HashByteSize);
            return Convert.ToBase64String(dst);
        }

        // TODO: Verify the changes here
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != ((SaltByteSize + 2 * HashByteSize) + 1)) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[SaltByteSize];
            Buffer.BlockCopy(src, 1, dst, 0, SaltByteSize);
            byte[] buffer3 = new byte[SaltByteSize + HashByteSize];
            Buffer.BlockCopy(src, SaltByteSize + 1, buffer3, 0, (SaltByteSize + HashByteSize));
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, HasingIterationsCount))
            {
                buffer4 = bytes.GetBytes((SaltByteSize + HashByteSize));
            }
            return AreHashesEqual(buffer3, buffer4);
        }

        private const int SaltByteSizeV1 = 24;
        private const int HashByteSizeV1 = 24;
        private const int HasingIterationsCountV1 = 10101;

        public static string HashPasswordV1(string password)
        {
            // http://stackoverflow.com/questions/19957176/asp-net-identity-password-hashing

            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, SaltByteSizeV1, HasingIterationsCountV1))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(HashByteSizeV1);
            }
            byte[] dst = new byte[(SaltByteSizeV1 + HashByteSizeV1) + 1];
            Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSizeV1);
            Buffer.BlockCopy(buffer2, 0, dst, SaltByteSizeV1 + 1, HashByteSizeV1);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPasswordV1(string hashedPassword, string password)
        {
            byte[] _passwordHashBytes;

            int _arrayLen = (SaltByteSizeV1 + HashByteSizeV1) + 1;

            if (hashedPassword == null)
            {
                return false;
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            byte[] src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != _arrayLen) || (src[0] != 0))
            {
                return false;
            }

            byte[] _currentSaltBytes = new byte[SaltByteSizeV1];
            Buffer.BlockCopy(src, 1, _currentSaltBytes, 0, SaltByteSizeV1);

            byte[] _currentHashBytes = new byte[HashByteSizeV1];
            Buffer.BlockCopy(src, SaltByteSizeV1 + 1, _currentHashBytes, 0, HashByteSizeV1);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, _currentSaltBytes, HasingIterationsCountV1))
            {
                _passwordHashBytes = bytes.GetBytes(SaltByteSizeV1);
            }

            return AreHashesEqual(_currentHashBytes, _passwordHashBytes);
        }

        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }
}
