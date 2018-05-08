using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace Captec.Helpers
{
    public class ScreenScraper
    {
        private string displayData = string .Empty;
        public string DisplayData
        {
            get { return displayData; }
        }

        public ScreenScraper(string url)
        {
            SetDisplayData(url);
        }

        private void SetDisplayData(string url)
        {
            HtmlDocument htmlDoc = GetHtmlDocumentFromUrl(url);
            if (htmlDoc != null)
                displayData = ParseHtmlDocumentText(htmlDoc);
        }

        /// <summary>
        /// HtmlAgilityPack to return an HtmlDocument from a valid Url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private HtmlDocument GetHtmlDocumentFromUrl(string url)
        {
            HtmlDocument htmlDoc = null;

            try
            {
                HtmlWeb web = new HtmlWeb();
                htmlDoc = web.Load(url);
            }
            catch (Exception ex)
            {
                throw;
            }

            return htmlDoc;
        }

        /// <summary>
        /// HtmlAgilityPack to parse an HtmlDocument for text and return a string of text.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string ParseHtmlDocumentText(HtmlDocument htmlDoc)
        {
            if (htmlDoc == null)
            {
                throw new ArgumentNullException("ParseHtmlDocumentText");
            }

            string text = string.Empty;

           
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("./descendant-or-self::*[not(self::script or self::style)]/text()[not(normalize-space(.)='')]"))
            {
                if (!string.IsNullOrEmpty(node.InnerText.Trim()))
                {
                    var temp = HtmlEntity.DeEntitize(node.InnerText);
                    text += temp.Trim() + " ";
                }
            }

            return text;
        }
    }
}