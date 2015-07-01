using CatMyVideo.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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

        // GET: Video/{id}
        [Route("/Video/{id}")]
        public ActionResult Index(int id = 1, bool? updated = false)
        {
            var video = Engine.BusinessManagement.Video.GetVideo(id);
            if (video == null)
                return RedirectToRoute("Index", "Home");

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
            return View(video);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(Engine.Dbo.Video model)
        {
            // Check if model state is valid.
            // Update in database.
            ViewBag.Updated = true;
            return RedirectToAction("Index", new { id = model.Id });
        }
    }
}