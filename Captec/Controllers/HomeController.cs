using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Captec.Models;
using Captec.Functions;

namespace Captec.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult WordCloud()
        {
            var errMsg = TempData["ErrorMessage"] as string;
            if (!string.IsNullOrEmpty(errMsg))
                return View(new WordCloudViewModel { ErrorMessage = errMsg } );

            return View(new WordCloudViewModel());
        }

        [HttpPost]
        public ActionResult WordCloud(WordCloudViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelState.Clear();

            var wcFunctions = new Captec.Functions.WordCloudFunctions();

            try
            {
                string sourceData = wcFunctions.GetSourceData(model.URL);

                List<string> wordsToRemove = wcFunctions.GetExclusions();

                Dictionary<string, int> wordCounter = wcFunctions.CreateWordCloudDictionary(sourceData, wordsToRemove, model.Occurrences);

                model.WordCloud = wcFunctions.CreateWordCloud(wordCounter);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "Sorry, we were unable to generate a word cloud for this URL. Please check the URL is valid and try again.";
                return RedirectToAction("WordCloud");
            }

            if (MvcHtmlString.IsNullOrEmpty(model.WordCloud))
                model.ErrorMessage = "Sorry, we were unable to generate a word cloud for this URL. Please check the URL is valid and try again.";

            return View(model);
        }
    }
}