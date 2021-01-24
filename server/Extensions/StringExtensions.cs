using System.Linq;
using System.Text.RegularExpressions;

namespace SmartAuth.Extensions
{
    public static class StringExtensions
    {
        const string CPF_PATTERN = @"[0-9]{3}\s?\.\s?[0-9]{3}\s?\.\s?[0-9]{3}\s?\-\s?[0-9]{2}";
        const string RG_PATTERN = @"[0-9]{8,9}";
        const string DATE_PATTERN = @"\d{2}\/\d{2}\/\d{4}";

        public static string ExtractCpf(this string text)
            => Regex
                .Match(text, CPF_PATTERN)
                .Value.Trim().Replace(" ", "");

        public static string ExtractRg(this string text)
            => Regex
                .Match(text, RG_PATTERN)
                .Value;

        public static string[] ExtractDates(this string text)
            => Regex
                .Matches(text, DATE_PATTERN)
                .Select(x => x.Value)
                .ToArray();

        public static string ExtractNome(this string text)
        {
            var refIndex = text.IndexOf("DOC.");
            var subText = text.Substring(0, refIndex);
            var textParts = subText.Split("&");
            var nome = textParts.Where(x => x.Length > 5).Last();

            return nome.Trim('\r','\n');
        }
    }
}
