using System;

namespace Core.Utilities.Toolkit
{
    public static class RandomPassword
    {
        public static string CreateRandomPassword(int length = 14)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            var random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
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
