using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Captec.Models
{
    public class WordCloudIgnoreWords
    {
        private List<string> wordsToRemove;
        public List<string> WordsToRemove
        {
            get { return wordsToRemove; }
        }

        public WordCloudIgnoreWords()
        {
            SetWordsToRemove();
        }

        private void SetWordsToRemove()
        {
            List<string> words = new List<string> { "a", "about", "above", "across", "after", "against", "all", "along", "an", "and", "any", "at", "by", "can", "do", "don't", "eg", "e.g", "e.g.", "find", "for", "from", "how", "i", "if", "in", "into", "is", "it", "more", "no", "of", "on", "or", "out", "some", "take", "the", "to", "use", "with", "who", "yes", "you", "your" };
            wordsToRemove = words;
        }

    }
}