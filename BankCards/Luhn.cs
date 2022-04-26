using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCards
{
    internal static class Luhn
    {

        private static string Generate15Digit()
        {
            string nums = "0123456789";
            string str = "";
            Random rand = new Random();
            for (int i = 0; i < 15; i++)
            {
                str += nums[rand.Next(0, nums.Length - 1)];
            }
            return str;
        }
        private static string GetLastDigit(string number)
        {
            var sum = 0;
            var alt = true;
            var digits = number.ToCharArray();
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                var curDigit = (digits[i] - 48);
                if (alt)
                {
                    curDigit *= 2;
                    if (curDigit > 9)
                        curDigit -= 9;
                }
                sum += curDigit;
                alt = !alt;
            }
            if ((sum % 10) == 0)
            {
                return "0";
            }
            return (10 - (sum % 10)).ToString();
        }
        public static string GenerateNum()
        {
            string num = Luhn.Generate15Digit();
            return num + Luhn.GetLastDigit(num);
        }
    }
}
