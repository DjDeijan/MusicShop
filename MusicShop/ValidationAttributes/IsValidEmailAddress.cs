using MusicShop.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MusicShop.ValidationAttributes
{
    public class IsValidEmailAddress : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string? emailAddress = value?.ToString();

            if (string.IsNullOrWhiteSpace(emailAddress))
                return false;

            return emailAddress.IsValidEmailAddress();
        }
    }
}
