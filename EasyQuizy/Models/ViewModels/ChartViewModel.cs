using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyQuizy.Models.ViewModels
{
    public class ChartViewModel
    {
        public IEnumerable<string> Names { get; set; }
        public IEnumerable<double> Results { get; set; }
        public SelectList FormedQuizes { get; set; }
        public SelectList Groups { get; set; }
    }
}