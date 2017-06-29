using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TextAnalysis.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Default server api page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Text Analysis";

            return View();
        }
    }
}
