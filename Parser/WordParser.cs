using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser
{
    public class WordParser
    {
        public IList<string> GetUniqueWords(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return new List<string>();

            content = RemoveSpecialCharacters(content.Trim());
            return content.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                          .Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
        }

        public IList<string> GetUniqueWordsInAlphabeticalOrder(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return new List<string>();

            var uniqueWords = GetUniqueWords(content).ToList();
            if (uniqueWords.Count() == 0)
                return uniqueWords;

            uniqueWords.Sort(String.Compare);
            return uniqueWords;
        }

        public IList<string> RemoveIgnoreWords(IList<string> words, IList<string> ignoreWords)
        {
            if (ignoreWords == null || ignoreWords.Count() == 0)
                return words;

            return words.Where(word => !ignoreWords.Contains(word.ToLower())).ToList();            
        }

        private string RemoveSpecialCharacters(string content)
        {
            var sb = new StringBuilder();
            foreach (var c in content)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ')
                    sb.Append(c);
            }
            return sb.ToString();
        }
    }
}