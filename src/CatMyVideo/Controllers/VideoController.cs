using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatMyVideo.Controllers
{
  public class VideoController : Controller
  {
    // GET: Video
    public ActionResult Index(int id = 1)
    {
      Engine.Dbo.Video video = Engine.BusinessManagement.Video.GetVideo(id);
      ViewBag.Updated = false;
      return View();
    }

    [Route("Video/Edit/{id}")]
    public ActionResult Edit(int id)
    {
      Engine.Dbo.Video video = Engine.BusinessManagement.Video.GetVideo(id);
      return View(video);
    }

    public ActionResult Update(Engine.Dbo.Video model)
    {
      // Check if model state is valid.
      // Update in database.
      ViewBag.Updated = true;
      return RedirectToAction("Index", new { id = model.Id });
    }
  }
}