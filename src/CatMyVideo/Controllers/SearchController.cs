using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatMyVideo.Controllers
{
  public class SearchController : Controller
  {
    // GET: Search
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Browse()
    {
      var query = HttpContext.Request.Params.Get("query");

      if (query == null)
        return RedirectToAction("Index", "Home");

      ViewBag.query = query;
      /* Fancy stuff -- implement that later.
       * var page = 0;
       * const int NB_VIDEO_BY_PAGE = 20;
       * var pageParam = HttpContext.Request.Params.Get("p");
       * if (pageParam != null)
       *  Int32.TryParse(pageParam, out page); */

      // Fetch videos by tags
      var tags = query.Split(' ').Select(x => new Engine.Dbo.Tag() { Name = x }).ToList();
      var videosTags = Engine.BusinessManagement.Video.ListVideosByTags(tags, true).ToList();
      ViewData["videos_tags"] = videosTags;

      // Fetch videos by user
      var videosUser = Engine.BusinessManagement.Video.ListVideosByAuthor(query, true);
      ViewData["videos_user"] = videosUser;

      // Fetch videos by name
      var videosName = Engine.BusinessManagement.Video.ListVideosByName(query, true).ToList();
      ViewData["videos_name"] = videosName;

      return View();
    }
  }
}