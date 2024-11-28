using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationFramework.Util.RandomGenerators.RandomStringHelpers
{
    class RandomStringsHelper
    {
        private static Random rnd = new Random();

        public static string GetRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public static decimal CurrencyToDecimal(string s)
        {
            return decimal.Parse(s, NumberStyles.Currency);

        }

        public static string GetRandomStringPassword()
        {
            var length = 3;

            var charsCaps = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var charsLower = "abcdefghijklmnopqrstuvwxyz";
            var charsNumbers = "123456789";
            var charsSpecial = "!@#$%^&*()_+";

            var stringCharsCaps = new char[length];
            var stringCharsLower = new char[length];
            var stringCharsNumbers = new char[length];
            var stringCharsSpecial = new char[length];

            var random = new Random();

            for (int i = 0; i < stringCharsCaps.Length; i++)
            {
                stringCharsCaps[i] = charsCaps[random.Next(charsCaps.Length)];
            }
            var finalStringCharsCaps = new String(stringCharsCaps);

            for (int i = 0; i < stringCharsLower.Length; i++)
            {
                stringCharsLower[i] = charsLower[random.Next(charsLower.Length)];
            }
            var finalStringCharsLower = new String(stringCharsLower);

            for (int i = 0; i < stringCharsNumbers.Length; i++)
            {
                stringCharsNumbers[i] = charsNumbers[random.Next(charsNumbers.Length)];
            }
            var finalStringCharsNumber = new String(stringCharsNumbers);

            for (int i = 0; i < stringCharsSpecial.Length; i++)
            {
                stringCharsSpecial[i] = charsSpecial[random.Next(charsSpecial.Length)];
            }
            var finalStringCharsSpecial = new String(stringCharsSpecial);

            var createdPassword = $"{finalStringCharsLower}{finalStringCharsCaps}{finalStringCharsSpecial}{finalStringCharsNumber}";
            return createdPassword;
        }
    }
}
