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
      // Fetch most viewed video of the day
      var lastVideo = Engine.BusinessManagement.Video.ListVideos(Engine.Dbo.Video.Order.ViewCountToday, false, 1);
      if (lastVideo.Any())
      {
        var mostViewed = lastVideo.First();

        // Description limit : 144 chars !
        if (mostViewed.Description.Length > 144)
          mostViewed.Description = mostViewed.Description.Substring(0, 144);

        ViewData["MostViewed"] = mostViewed;
      }

      // Fetch latest videos uploaded
      var latest = Engine.BusinessManagement.Video.ListVideos(Engine.Dbo.Video.Order.ViewCountTotal, true, 4);
      ViewData["Latest"] = latest;

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