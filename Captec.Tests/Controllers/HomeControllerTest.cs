using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Captec;
using Captec.Models;
using Captec.Controllers;

namespace Captec.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void WordCloudValidURL()
        {
            HomeController controller = new HomeController();

            // valid url entered.
            var model = new WordCloudViewModel();
            model.URL = "https://www.captecsystems.com";

            var result = controller.WordCloud(model) as ViewResult;
            var viewmodel = (WordCloudViewModel)result.ViewData.Model;

            Assert.IsNotNull(result);
            Assert.IsNull(viewmodel.ErrorMessage);
            Assert.IsNotNull(viewmodel.WordCloud);
            Assert.AreEqual(30, viewmodel.Occurrences);
            Assert.AreEqual("https://www.captecsystems.com", viewmodel.URL);
        }

        [TestMethod]
        public void WordCloudInvalidURL()
        {
            HomeController controller = new HomeController();

            // invalid url entered.
            var model = new WordCloudViewModel();
            model.URL = "https://www.captec.com";

            var redirectResult = controller.WordCloud(model) as RedirectToRouteResult;
            var viewResult = controller.WordCloud() as ViewResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("WordCloud", redirectResult.RouteValues["action"]);
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("Sorry, we were unable to generate a word cloud for this URL. Please check the URL is valid and try again.", viewResult.TempData.Values.ElementAt(0));
        }

    }
}
