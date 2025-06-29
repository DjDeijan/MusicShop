using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MusicShop.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsAtLeastOneLetterAndDigit(this string s)
        {
            return Regex.Match(s, @"(?=.*?[0-9])(?=.*?[A-Za-z]).+").Success;
        }

        public static bool ContainsLatinCharsAndDigits(this string s)
        {
            return Regex.Match(s, @"^[A-Za-z0-9]+$").Success;
        }

        public static bool IsValidEmailAddress(this string emailAddress)
        {
            try
            {
                new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
