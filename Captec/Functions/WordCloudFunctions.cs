using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Captec.Models;
using Captec.Helpers;

namespace Captec.Functions
{
    public class WordCloudFunctions
    {

        public string GetSourceData(string url)
        {
            string content = string.Empty;

            var scraper = new ScreenScraper(url);
            content = scraper.DisplayData;

            return content;
        }

        public List<string> GetExclusions()
        {
            List<string> wordsToIgnore = new List<string>();

            var exclusions = new WordCloudIgnoreWords();
            wordsToIgnore = exclusions.WordsToRemove;

            return wordsToIgnore;
        }

        public Dictionary<string, int> CreateWordCloudDictionary(string content, List<string> exclusions, int occurrences)
        {
            Dictionary<string, int> wordCounter = new Dictionary<string, int>();

            var counter = new WordCloudDictionary(content);
            wordCounter = counter.WordCounter;

            if(wordCounter != null && (exclusions != null && exclusions.Count > 0))
            {
                wordCounter = counter.RemoveWordsFromWordCounter(wordCounter, exclusions);
            }

            if(wordCounter != null)
            {
                wordCounter = counter.GetTopNOccurrencesFromCounter(wordCounter, occurrences);
            }

            if(wordCounter != null)
            {
                wordCounter = counter.SortCounterByKeyAscending(wordCounter);
            }

            return wordCounter;
        }

        public MvcHtmlString CreateWordCloud(Dictionary<string,int> wordCounter)
        {
            MvcHtmlString wordCloud = MvcHtmlString.Empty;

            var formatter = new WordCloudFormatter(wordCounter);
            wordCloud = formatter.WordCloud;

            return wordCloud;
        }
    }
}