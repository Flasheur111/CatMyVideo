using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatMyVideo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var MostViewed = new Engine.Dbo.Video()
            {
                Title = "Space Night & CC-Musik: So könnte es aussehen",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                ViewCount = 42,
                UploadDate = new DateTime(),
            };
            var list = new List<Engine.Dbo.Video>();
            list.Add(MostViewed);
            list.Add(MostViewed);
            list.Add(MostViewed);
            list.Add(MostViewed);
            ViewData["MostViewed"] = MostViewed;
            ViewData["Recommanded"] = list;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}