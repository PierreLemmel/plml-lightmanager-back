using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace LightManager
{
    public static class Strings
    {
        public static string UncapitalizeFirstLetter(this string input)
        {
            char[] chars = input.ToCharArray();
            chars[0] = char.ToLower(chars[0]);

            return new string(chars);
        }

        public static string RemoveDiacritics(this string text)
        {
            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            char[] chars = normalized
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray();

            string result = new string(chars).Normalize(NormalizationForm.FormC);

            return result;
        }

        public static string TrimPunctuation(this string text)
        {
            int length = text.Length;

            int start = 0;
            for (int i = 0; i < length; i++)
            {
                char c = text[i];
                if (char.IsPunctuation(c) || char.IsWhiteSpace(c))
                    start++;
                else
                    break;
            }

            int end = 0;
            for (int i = length - 1; i >= 0; i--)
            {
                char c = text[i];
                if (char.IsPunctuation(c) || char.IsWhiteSpace(c))
                    end++;
                else
                    break;
            }

            return text.Substring(start, length - (end + start));
        }

        public static bool IsNullOrEmpty(this string input) => string.IsNullOrEmpty(input);
        public static bool IsNullOrWhiteSpace(this string input) => string.IsNullOrWhiteSpace(input);

        public static string RemoveAfter(this string input, string separator) => input.Split(separator).First();

        public static StringBuilder Append(this StringBuilder sb, params string[] inputs)
        {
            foreach (string input in inputs)
                sb.Append(input);

            return sb;
        }

        public static string RemoveEnd(this string input, string toRemove)
            => input.EndsWith(toRemove) ? input.Substring(0, input.Length - toRemove.Length) : input;

        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            chars.Reverse();

            return new string(chars);
        }

        public static string Join(this IEnumerable<string> elts, string separator) => string.Join(separator, elts);
    }
}