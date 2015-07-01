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

        [Route("/Video/Display/{id}")]
        public ActionResult Display(int id = 1, bool? updated = false)
        {
            var video = Engine.BusinessManagement.Video.GetVideo(id);
            if (video == null)
                return RedirectToAction("Index", "Home");

            ApplicationUser connectedUser = null;
            if (User.Identity.IsAuthenticated)
                connectedUser = UserManager.FindById(User.Identity.GetUserId());

            var user = Engine.BusinessManagement.User.FindUser(video.User);

            ViewBag.Username = user.Nickname;
            ViewBag.CanDelete = connectedUser != null && user.Nickname == connectedUser.UserName;
            ViewBag.CanEdit = connectedUser != null && (user.Nickname == connectedUser.UserName || User.IsInRole("Admin, Moderator"));

            ViewBag.Updated = updated;

            // TODO : Tags
            ViewData["tags"] = new List<String> { "amphionic", "reexecuted", "reckonable", "dioxane", "maggiore", "amymone", "justification", "direxit", "frederiksberg", "pleasant" }; // Generated random words, obviously.
            
            return View(video);
        }

        [Route("/Video/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            Engine.Dbo.Video video = Engine.BusinessManagement.Video.GetVideo(id);

            //if (video == null)
            //    return RedirectToAction("Index", "Home");

            return View(video);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(EditVideoViewModel model)
        {
            // TODO: Check if model state is valid.
            // Format tags
            model.Tags = "coucou , tu , veuxvoir, ma super, video, ";
            model._Tags = new List<String>(model.Tags.Split(','));
            model._Tags = model._Tags.Select(x => x.Trim()).ToList();
            // TODO: Update in database.

            return RedirectToAction("Display", "Video", new { id = model.Id, updated = true });
        }
    }
}