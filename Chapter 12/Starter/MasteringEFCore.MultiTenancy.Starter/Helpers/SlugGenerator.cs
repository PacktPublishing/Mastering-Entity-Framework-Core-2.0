using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Starter.Helpers
{
    public static class SlugGenerator
    {
        public static string Generate(this string phrase)
        {
            var phraseWithoutAccent = phrase.RemoveAccent().ToLower();
            // invalid chars           
            phraseWithoutAccent = Regex.Replace(phraseWithoutAccent, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            phraseWithoutAccent = Regex.Replace(phraseWithoutAccent, @"\s+", " ").Trim();
            // cut and trim 
            phraseWithoutAccent = phraseWithoutAccent.Substring(0, phraseWithoutAccent.Length <= 45 ? phraseWithoutAccent.Length : 45).Trim();
            phraseWithoutAccent = Regex.Replace(phraseWithoutAccent, @"\s", "-"); // hyphens   
            return phraseWithoutAccent;
        }

        internal static string RemoveAccent(this string inputText)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(inputText);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
