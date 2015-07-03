using CatMyVideo.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace CatMyVideo.Controllers
{
    public class VideoController : Controller
    {
        #region init
        private ApplicationUserManager _userManager;

        public VideoController()
        {
        }

        public VideoController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }
        #endregion

        [Route("/Video/Display/{id}")]
        public ActionResult Display(int id = 1, bool? updated = false, bool? errorDeleted = false)
        {
            var video = Engine.BusinessManagement.Video.GetVideo(id, true);
            if (video == null)
                return RedirectToAction("Index", "Home");

            var user = video.User;
            ViewBag.Username = user.Nickname;
            ViewData["tags"] = Engine.BusinessManagement.Tag.ListTagsByVideoId(video.Id);
            ViewData["comments"] = Engine.BusinessManagement.Comment.ListCommentByVideoId(video.Id, 20, 0);
            ViewData["numberComments"] = Engine.BusinessManagement.Comment.CountCommentsByVideoId(video.Id);

            if (video.Encodes.Count == 0)
                return View("Error", video);

            ApplicationUser connectedUser = null;
            if (User.Identity.IsAuthenticated)
                connectedUser = UserManager.FindById(User.Identity.GetUserId());

            ViewBag.CanDelete = connectedUser != null && user.Nickname == connectedUser.UserName || User.IsInRole("Admin") || User.IsInRole("Moderator");
            ViewBag.CanEdit = connectedUser != null && (user.Nickname == connectedUser.UserName || User.IsInRole("Admin") || User.IsInRole("Moderator"));

            Engine.BusinessManagement.Video.IncrementViewCount(video.Id);

            ViewBag.Updated = updated;
            ViewBag.ErrorDeleted = errorDeleted;

            return View("Index", video);
        }

        [Route("/Video/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            Engine.Dbo.Video video = Engine.BusinessManagement.Video.GetVideo(id);

            if (video == null)
                return RedirectToAction("Index", "Home");

            // TODO: check user rights

            EditVideoViewModel model = new EditVideoViewModel() {
              Id = video.Id,
              Title = video.Title,
              Description = video.Description,
            };

            var tags = Engine.BusinessManagement.Tag.ListTagsByVideoId(video.Id).Select(x => x.Name).ToArray();
            model.Tags = String.Join(" ", tags);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EditVideoViewModel model)
        {
            if (!model.Tags.Split(' ').All(x => x.Length <= 20))
                ModelState.AddModelError("Tags", "Tags must be 20 charaters long and contains only number, letter and -");
            if (ModelState.IsValid)
            {
                var oldVideo = Engine.BusinessManagement.Video.GetVideo(model.Id);
                Engine.BusinessManagement.Video.UpdateVideo(new Engine.Dbo.Video()
                    {
                        Id = model.Id,
                        UploadDate = oldVideo.UploadDate,
                        ViewCountToday = oldVideo.ViewCountToday,
                        ViewCountTotal = oldVideo.ViewCountTotal,
                        Title = model.Title,
                        Description = model.Description,
                        User = oldVideo.User,
                        Encodes = oldVideo.Encodes,
                        Comments = oldVideo.Comments,
                    });
                Engine.BusinessManagement.Tag.AddTags(model.Tags.Split().Distinct().Select(x => new Engine.Dbo.Tag() { Name = x }), model.Id);

                return RedirectToAction("Display", "Video", new { id = model.Id, updated = true });    
            }
            return View("Edit", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var video = Engine.BusinessManagement.Video.GetVideo(id);
            if (video != null && (video.User.AspNetUsersId == user.Id || User.IsInRole("Admin,Moderator")))
            {
                try
                {
                    var driver = new Storage.MongoFS.Driver();
                    List<Engine.Dbo.Encode> encodes = Engine.BusinessManagement.Encode.ListEncode(id, true) as List<Engine.Dbo.Encode>;
                    foreach (Engine.Dbo.Encode encode in encodes)
                    {
                        Engine.BusinessManagement.Encode.DeleteEncode(encode);
                    }
                    Engine.BusinessManagement.Video.DeleteVideo(id);
                    driver.DeleteStreamAndThumb(Convert.ToString(id), encodes);
                    return RedirectToAction("Display", "Account", new { nickname = user.UserName });
                }
                catch (Exception)
                { }
            }
            return RedirectToAction("Display", "Video", new { id = id, errorDeleted = true });
        }
    }
}