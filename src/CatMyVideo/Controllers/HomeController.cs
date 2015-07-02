using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Cryptography;
using System.Text;

namespace CatMyVideo.Controllers
{
  public class HomeController : Controller
  {
    private ApplicationUserManager _userManager;

    public HomeController()
    {

    }

    public HomeController(ApplicationUserManager userManager)
    {
      UserManager = userManager;
    }

    public ApplicationUserManager UserManager
    {
      get
      {
        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        _userManager = value;
      }
    }

    public ActionResult Index()
    {
      // Mock videos
      var MostViewed = new Engine.Dbo.Video()
      {
        Title = "Space Night & CC-Musik: So könnte es aussehen",
        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
        ViewCount = 42,
        UploadDate = new DateTime(),
      };

      var list = new List<Engine.Dbo.Video>();
      for (int i = 0; i < 4; i++)
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