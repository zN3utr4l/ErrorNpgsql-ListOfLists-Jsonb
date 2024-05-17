using System.Globalization;
using System.Text.RegularExpressions;

namespace ErrorNpgsql.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            Regex camelCaseRegex = new(@"([a-z0-9])([A-Z])", RegexOptions.None, TimeSpan.FromSeconds(5));

            return camelCaseRegex
                .Replace(input, "$1_$2")
                .ToLower(CultureInfo.InvariantCulture);
        }
    }
}