﻿using System.Security.Cryptography;

namespace SweaterV1.Services.Extensions
{

    static class PBKDF2HashMiddleware
    {
        private const int SaltLength = 24;
        private const int DerivedKeyLength = 24;

        public static string CreatePasswordHash(string password, int iterationCount = 15013)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            byte[] salt = GenerateRandomSalt(SaltLength);
            byte[] hashValue = GenerateHashValue(password, salt, iterationCount);
            byte[] iterationCountByteArr = BitConverter.GetBytes(iterationCount);
            var valueToSave = new byte[SaltLength + DerivedKeyLength + iterationCountByteArr.Length];
            Buffer.BlockCopy(salt, 0, valueToSave, 0, SaltLength);
            Buffer.BlockCopy(hashValue, 0, valueToSave, SaltLength, DerivedKeyLength);
            Buffer.BlockCopy(iterationCountByteArr, 0, valueToSave, salt.Length + hashValue.Length,
                iterationCountByteArr.Length);
            return Convert.ToBase64String(valueToSave);
        }

        private static byte[] GenerateRandomSalt(int saltLength)
        {
            using (var csprng = new RNGCryptoServiceProvider())
            {
                var salt = new byte[saltLength];
                csprng.GetBytes(salt);
                return salt;
            }
        }

        private static byte[] GenerateHashValue(string password, byte[] salt, int iterationCount)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterationCount))
            {
                return pbkdf2.GetBytes(DerivedKeyLength);
            }
        }

        public static bool VerifyPassword(string passwordGuess, string passwordHash)
        {
            //ingredient #1: password salt byte array
            var salt = new byte[SaltLength];

            //ingredient #2: byte array of password
            var actualPasswordByteArr = new byte[DerivedKeyLength];

            //convert actualSavedHashResults to byte array
            byte[] actualSavedHashResultsBtyeArr = Convert.FromBase64String(passwordHash);

            //ingredient #3: iteration count
            int iterationCountLength =
                actualSavedHashResultsBtyeArr.Length - (salt.Length + actualPasswordByteArr.Length);
            byte[] iterationCountByteArr = new byte[iterationCountLength];
            Buffer.BlockCopy(actualSavedHashResultsBtyeArr, 0, salt, 0, SaltLength);
            Buffer.BlockCopy(actualSavedHashResultsBtyeArr, SaltLength, actualPasswordByteArr, 0,
                actualPasswordByteArr.Length);
            Buffer.BlockCopy(actualSavedHashResultsBtyeArr, (salt.Length + actualPasswordByteArr.Length),
                iterationCountByteArr, 0, iterationCountLength);
            byte[] passwordGuessByteArr =
                GenerateHashValue(passwordGuess, salt, BitConverter.ToInt32(iterationCountByteArr, 0));
            return ConstantTimeComparison(passwordGuessByteArr, actualPasswordByteArr);
        }

        private static bool ConstantTimeComparison(byte[] passwordGuess, byte[] actualPassword)
        {
            uint difference = (uint)passwordGuess.Length ^ (uint)actualPassword.Length;
            for (var i = 0; i < passwordGuess.Length && i < actualPassword.Length; i++)
            {
                difference |= (uint)(passwordGuess[i] ^ actualPassword[i]);
            }

            return difference == 0;
        }
    }
}