using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CatMyVideo.Models;
using System.Collections.Generic;

namespace CatMyVideo.Controllers
{
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

    [Authorize]
    [Route("Account/Edit/{nickname}")]
    public ActionResult Edit(string nickname)
    {
      // TODO: Check if edit is available to current user.

      // Mock user
      EditViewModel user = new EditViewModel()
      {
        Nickname = "Seika",
        Email = "nocteaestiva@gmail.com",
      };

      // TODO: replace the code above by this one, when removing mock.
      //var user = Engine.BusinessManagement.User.FindUserByNickname(nickname);
      return View("Edit", user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Update(EditViewModel model)
    {
      if (ModelState.IsValid)
      {
        // TODO: check if model is valid.
        // TODO: update entity in db.
        return Display(model.Nickname, true);
      }

      return Display(null, false);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(string nickname)
    {
      // TODO : delete account and log off 
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
