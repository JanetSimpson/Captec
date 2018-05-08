using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Captec.Helpers
{
    /// <summary>
    /// Transforms source data into a dictionary of distinct words with count of each word and provides methods for removing words and sorting.
    /// </summary>
    public class WordCloudDictionary
    {
        private Dictionary<string, int> wordCounter;
        public Dictionary<string, int> WordCounter
        {
            get { return wordCounter;  }
        }

        public WordCloudDictionary(){ }

        public WordCloudDictionary(string sourceData)
        {
            PopulateWordCounter(sourceData);
        }

        private Dictionary<string,int> PopulateWordCounter(string sourceData)
        {
            List<string> words = GetWholeWords(sourceData);

            if (words != null && words.Count > 0)
                wordCounter = CreateWordCounter(words);
  
            return wordCounter;
        }

        /// <summary>
        /// Remove numbers and unwanted punctuation to get whole words.
        /// </summary>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        private List<string> GetWholeWords(string sourceData)
        {
            List<string> words = new List<string>();

            var text = sourceData.Split(' ').Where(x => !string.IsNullOrEmpty(x));

            foreach (string part in text)
            {
                if (!string.IsNullOrEmpty(part))
                {
                    Regex regex = new Regex(@"\b[a-zA-Z]+\b");
                    Match match = regex.Match(part);
                    if (match.Success)
                    {
                        string filtered = Regex.Replace(part, @"^\p{P}", "");
                        filtered = Regex.Replace(filtered, @"\p{P}$", "");
                        words.Add(filtered.ToLower().Trim());
                    }
                }
            }

            return words;
        }

        private Dictionary<string, int> CreateWordCounter(List<string> list)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            dictionary = (from item in list
                          group item by item
                          into g
                          select g
                           ).ToDictionary(g => g.Key, g => g.Count());

            return dictionary;
        }

        #region Public methods

        public Dictionary<string, int> RemoveWordsFromWordCounter(Dictionary<string, int> dictionary, List<string> list)
        {
            if (list == null || dictionary == null)
            {
                throw new ArgumentNullException("RemoveWordsFromWordCounter");
            }

            foreach (string item in list)
            {
                dictionary.Remove(item);
            }

            return new Dictionary<string, int>(dictionary);
        }

        public Dictionary<string, int> GetTopNOccurrencesFromCounter(Dictionary<string, int> dictionary, int occurrence)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("GetTopNOccurrencesFromCounter");
            }

            var dict = (from kvp in dictionary
                        orderby kvp.Value descending
                        select kvp
                        ).Take(occurrence).ToDictionary(p => p.Key, p => p.Value);

            return new Dictionary<string, int>(dict);
        }

        public Dictionary<string, int> SortCounterByKeyAscending(Dictionary<string, int> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("SortCounterByKeyAscending");
            }

            var dict = (from kvp in dictionary
                        orderby kvp.Key
                        select kvp).ToDictionary(p => p.Key, p => p.Value);

            return new Dictionary<string, int>(dict);
        }
        #endregion

    }
}