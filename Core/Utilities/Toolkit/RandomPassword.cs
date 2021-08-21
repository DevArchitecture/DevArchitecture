using System;

namespace Core.Utilities.Toolkit
{
    /// <summary>
    /// One time password generator for mobile login etc.
    /// </summary>
    public static class RandomPassword
    {
        public static string CreateRandomPassword(int length = 14)
        {
            var validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            var random = new Random();

            var chars = new char[length];
            for (var i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return new string(chars);
        }

        public static int RandomNumberGenerator(int min = 100000, int max = 999999)
        {
            var random = new Random();
            return random.Next(min, max);
        }
    }
}