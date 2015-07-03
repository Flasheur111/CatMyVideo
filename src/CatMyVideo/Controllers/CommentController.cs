using CatMyVideo.Models;
using Storage.Video;
using Storage.WCF;
using Storage.WCF.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatMyVideo.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        #region init
        private ApplicationUserManager _userManager;

        public CommentController()
        {
        }

        public CommentController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        [Route("/Comment/Create/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, String content)
        {
            var video = Engine.BusinessManagement.Video.GetVideo(id);
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (video == null)
            {
                ViewBag.InvalidComment = true;
                return RedirectToAction("Display", "Video", new
                {
                    id = id,
                });
            }

            Engine.BusinessManagement.Comment.AddComment(new Engine.Dbo.Comment()
            {
                Message = content,
                Video = id,
                PostDate = DateTime.Now,
                User = Engine.BusinessManagement.User.FindUser(user.T_UserId),
            });
            return RedirectToAction("Display", "Video", id);
        }

        [Route("/Comment/Delete/{id}")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var comment = Engine.BusinessManagement.Comment.GetComment(id);
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (comment.User.Nickname == user.UserName || User.IsInRole("Admin,Moderator"))
            {
                Engine.BusinessManagement.Comment.DeleteComment(comment);
                return RedirectToAction("Display", "Video", new { id = comment.Video });
            }
            return RedirectToAction("Display", "Video", new { id = comment.Video, errorDeletedComment = true });
        }
    }
}