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
      // Mock
      ViewBag.Username = "Seika";
      ViewBag.CanDelete = true;
      ViewBag.CanEdit = true;
      var video = new Engine.Dbo.Video()
      {
        Id = 1,
        Title = "Wolf Mountain",
        ViewCount = 1329211,
        Description = "Integer vehicula sagittis ipsum, in tempor arcu sollicitudin eget. Suspendisse efficitur, elit a eleifend tempor, enim velit accumsan elit, eu vehicula risus orci sit amet sem. Maecenas non risus eget lectus feugiat gravida tincidunt vel libero. Proin ut sem lacus. Proin odio felis, venenatis ac sodales id, rutrum sit amet lectus. Suspendisse ut sem et elit ornare dignissim ac in nisi. In at nisi arcu. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin faucibus mi varius lorem lobortis, lobortis pretium ipsum accumsan. Morbi sit amet risus risus.",
      };

      // Set privileges
      //Engine.Dbo.Video video = Engine.BusinessManagement.Video.GetVideo(id);
      //var user = Engine.BusinessManagement.User.FindUser(video.Id);
      //ViewBag.Username = user.Nickname;
      //ViewBag.CanDelete = user.Type > Engine.Dbo.User.Role.Classic || User.Identity.Name == user.Nickname;
      //ViewBag.CanEdit = user.Type > Engine.Dbo.User.Role.Modo || User.Identity.Name == user.Nickname;

      ViewBag.Updated = false;
      return View(video);
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