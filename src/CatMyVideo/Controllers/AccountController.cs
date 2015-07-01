using CatMyVideo.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CatMyVideo.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager)
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

        [AllowAnonymous]
        [Route("Account/Display/{nickname}", Name = "ShowProfile")]
        public ActionResult Display(string nickname, bool? updated = false)
        {
            if (String.IsNullOrEmpty(nickname))
                return RedirectToAction("Index", "Home");

            var user = Engine.BusinessManagement.User.FindUserByNickname(nickname);
            if (user == null)
                return RedirectToAction("Index", "Home");

            var videos = Engine.BusinessManagement.Video.ListUserVideos(user.Id);
            ViewData["videos"] = videos;

            ViewBag.Updated = updated;

            return View("Display", user);
        }

        [Route("Account/Edit/{nickname}")]
        public ActionResult Edit(string nickname)
        {
            // Check if edit is available to current user.
            var actualUser = UserManager.FindById(User.Identity.GetUserId());
            if (!User.IsInRole("Admin, Moderator") && actualUser.UserName != nickname)
                return RedirectToAction("Index", "Home");

            var user = Engine.BusinessManagement.User.FindUserByNickname(nickname);

            EditViewModel userModel = new EditViewModel()
            {
                Nickname = user.Nickname,
                Email = user.Mail,
                Description = user.Description,
            };
            return View("Edit", userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Account/Edit/{nickname}")]
        public async Task<ActionResult> Edit(string nickname, EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                // update entity in db.
                var oldUser = UserManager.FindByName(nickname);

                String email = oldUser.Email;
                var result = await UserManager.SetEmailAsync(oldUser.Id, model.Email);
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(model.NewPassword))
                        result = await UserManager.ChangePasswordAsync(oldUser.Id, model.Password, model.NewPassword);
                    if (result.Succeeded)
                    {

                        Engine.BusinessManagement.User.UpdateUser(new Engine.Dbo.User()
                        {
                            Id = oldUser.T_UserId,
                            AspNetUsersId = oldUser.Id,
                            Description = model.Description,
                            Mail = model.Email,
                            Nickname = model.Nickname,
                            Password = model.Password,
                        });
                        return RedirectToAction("Display", "Account", new RouteValueDictionary()
                        {
                            { "nickname", nickname },
                            { "updated", true }
                        });
                    }
                    else
                        result = await UserManager.SetEmailAsync(oldUser.Id, email);
                }
                AddErrors(result);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string nickname)
        {
            var actualUser = UserManager.FindById(User.Identity.GetUserId());
            if (!User.IsInRole("Admin, Moderator") && actualUser.UserName != nickname)
                return RedirectToAction("Index", "Home");

            // delete account and log off
            UserManager.Delete(actualUser);
            AuthenticationManager.SignOut();

            return RedirectToAction("Index", "Home");
        }
        #region Helpers
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
