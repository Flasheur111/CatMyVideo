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
            BusinessManagement.User.AddUser(new Dbo.User() { Description = "test", Mail = "a@a.fr", Nickname = "nicky nicky", Password = "lolilol", Type = Dbo.Role.Classic });
            var toto = BusinessManagement.User.ListAdminUsers();
            toto = BusinessManagement.User.ListAllUsers();
            var test = BusinessManagement.User.FindUser(toto.First().Id);

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