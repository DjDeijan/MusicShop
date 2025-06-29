using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MusicShop.ValidationAttributes
{
    public class UpToTwoDecimalPlaces : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string? s = value?.ToString();

            if (string.IsNullOrWhiteSpace(s))
                return false;

            return Regex.Match(s, @"^[0-9]*(\,[0-9]{1,2})?$").Success;
        }
    }
}
