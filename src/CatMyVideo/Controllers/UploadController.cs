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
    public class UploadController : Controller
    {
        #region init
        private ApplicationUserManager _userManager;

        public UploadController()
        {
        }

        public UploadController(ApplicationUserManager userManager)
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

        // GET : Index
        [HttpGet]
        public ActionResult Index()
        {
            string ListFormats = "";
            FormatChecker.GetFormats().ForEach(x => ListFormats += "." + x + ",");
            ViewData["Format"] = ListFormats.Substring(0, ListFormats.Length - 2);
            return View();
        }

        // POST: Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadViewModel model)
        {
            if (!model.Tags.Split(' ').All(x => x.Length <= 20))
                ModelState.AddModelError("Tags", "Tags must be 20 charaters long and contains only number, letter and -");
            if (ModelState.IsValid && model.File != null)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                string fileExtension = Path.GetExtension(model.File.FileName).Substring(1);
                try
                {
                    RemoteFileInfo fileInfo = new RemoteFileInfo();
                    fileInfo.Stream = model.File.InputStream;
                    fileInfo.InputFormat = fileExtension;

                    Engine.Dbo.Video video = new Engine.Dbo.Video()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        User = Engine.BusinessManagement.User.FindUser(user.T_UserId),
                        UploadDate = DateTime.Now,
                    };

                    fileInfo.IdVideo = Engine.BusinessManagement.Video.AddVideo(video);

                    Engine.BusinessManagement.Tag.AddTags(model.Tags.Split().Distinct().Select(x => new Engine.Dbo.Tag() { Name = x }), fileInfo.IdVideo);

                    ClientManager.UploadVideo(fileInfo);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    return RedirectToAction("Index", "Upload");
                }

                return RedirectToAction("Display", "Account", user.UserName);
            }

            return View("Index", model);
        }
    }
}
