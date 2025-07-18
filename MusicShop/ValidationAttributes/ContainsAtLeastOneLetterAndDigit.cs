﻿using MusicShop.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MusicShop.ValidationAttributes
{
    public class ContainsAtLeastOneLetterAndDigit : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string? s = value?.ToString();

            if (string.IsNullOrWhiteSpace(s))
                return false;

            return s.ContainsAtLeastOneLetterAndDigit();
        }
    }
}
