using Boilerplate_ASPNet_API.Domain.Entities;

namespace Boilerplate_ASPNet_API.Application.Common.Extensions;


public static class StringExtensions
{
    public static int ToIntOrThrow(this string value)
    {
        if (int.TryParse(value, out int result))
        {
            return result;
        }

        throw new ArgumentException($"Status '{value}' tidak valid.");
    }
}