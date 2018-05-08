using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Captec.Models
{
    public class WordCloudViewModel
    {
        [DataType(DataType.Url)]
        [Required(ErrorMessage = "Please enter a valid URL.")]
        public string URL { get; set; }
        
        public int Occurrences { get; set; }

        public MvcHtmlString WordCloud { get; set; }

        public string ErrorMessage { get; set; }

        public WordCloudViewModel()
        {
            this.Occurrences = 30;
        }
    }
}