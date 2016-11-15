using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IA.TrabalhoNB.TextDocs
{
    public static class StopWords
    {

        public static List<string> getStopwordsArray()
        {
            string ret = TextDocs.Resources.stopwords;
            string[] sw;
            sw = ret.Split("\r\n".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            return sw.ToList();
        }

        public static List<string> tokenWithoutSwAndLinks(string text)
        {
            List<string> words;
            string newText = text.ToLower();

            newText = removePunctuation(text);

            words = newText.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string s in getStopwordsArray())
            {
                words = words.Where(a => a.ToUpper() != s.ToUpper()).ToList();
            }

            words = words.Where(a => a.IndexOf("http") < 0).ToList();
            words = words.Where(a => a != "").ToList();

            return words;

        }

        private static string removeAccentuation(string text)
        {
            string with = "ÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÑÒÓÔÕÖÙÚÛÜÝàáâãäåçèéêëìíîïñòóôõöùúûüý";
            string without = "AAAAAACEEEEIIIINOOOOOUUUUYaaaaaaceeeeiiiinooooouuuuy";
            for (int k = 0; k < text.Length; k++)
                if (with.IndexOf(text[k]) > 0)
                    text = text.Replace(text[k], without[with.IndexOf(text[k])]);
            return text;
        }
        public static string removeSpecials(string text)
        {
            Regex rg = new Regex("[A-Za-z0-1]*");
            foreach (Match m in rg.Matches(text))
                if (m.Value != "")
                    return m.Value;
            return "";
        }
        public static string removePunctuation(string text)
        {
            return Regex.Replace(text, "[^\\w\\s]", " ");
        }
    }
}
