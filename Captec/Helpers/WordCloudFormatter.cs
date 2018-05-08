using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Captec.Helpers
{
    public class WordCloudFormatter
    {
        private MvcHtmlString wordCloud = MvcHtmlString.Empty;
        public MvcHtmlString WordCloud
        {
            get { return wordCloud; }
        }

        public WordCloudFormatter(Dictionary<string, int> wordCounter)
        {
            PopulateWordCloud(wordCounter);
        }

        private void PopulateWordCloud(Dictionary<string, int> wordCounter)
        {
            var min = wordCounter.Min(t => t.Value);
            var max = wordCounter.Max(t => t.Value);
            var dist = (max - min) / 3;

            var links = new StringBuilder();

            foreach (var tag in wordCounter)
            {
                string tagClass;

                if (tag.Value == max)
                {
                    tagClass = "largest";
                }
                else if (tag.Value > (min + (dist * 2)))
                {
                    tagClass = "large";
                }
                else if (tag.Value > (min + dist))
                {
                    tagClass = "medium";
                }
                else if (tag.Value == min)
                {
                    tagClass = "smallest";
                }
                else
                {
                    tagClass = "small";
                }

                links.AppendFormat("<a href=\"#{0}\" title=\"{1}\" class=\"{2}\">{1}</a>{3}",
                                    tag.Key, tag.Key, tagClass, Environment.NewLine);
            }

            var div = new TagBuilder("div");
            div.MergeAttribute("class", "tag-cloud");
            div.InnerHtml = links.ToString();

            wordCloud =  MvcHtmlString.Create(div.ToString());

        }
    }
}