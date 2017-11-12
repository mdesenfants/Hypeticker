using System;
using System.Text;

namespace Hypeticker.Utilities
{
    public static class Company
    {
        private static string[] consonants = new string[] { "b", "d", "f", "h", "j", "k", "l", "m", "n", "p", "r", "s", "t", "v", "w", "x", "z", "th", "ch", "kr", "tr", "br", "fr", "gr", "sh", "pr", "shr", "hr" };
        private static string[] vowels = new string[] { "a", "e", "i", "o", "u", "ae", "ie", "oi", "ea", "oo", "ou", "yi" };

        public static string GetCompany(string input)
        {
            var hash = Math.Abs(input.ToLower().GetHashCode());
            int sum = 0;
            var value = new StringBuilder(2 * sizeof(int));

            for (int i = 0; i < sizeof(int); i++)
            {
                var thisval = (hash & 0xf);
                sum += thisval;
                if (i % 2 == 0)
                {
                    value.Append(consonants[(thisval + sum) % consonants.Length]);
                }
                else
                {
                    value.Append(vowels[((thisval + sum) & 0xF) % vowels.Length]);
                }
                hash >>= 8;
            }

            return value.ToString().ToUpper();
        }
    }
}
