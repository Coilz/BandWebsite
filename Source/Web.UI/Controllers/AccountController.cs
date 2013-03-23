using System;
using System.Web.Mvc;
using System.Web.Security;
using Ewk.BandWebsite.Resources;
using Ewk.BandWebsite.Web.Common.Models.Account;

namespace Ewk.BandWebsite.Web.UI.Controllers
{
    public class AccountController : ControllerBase
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) &&
                        returnUrl.Length > 1 &&
                        returnUrl.StartsWith("/") &&
                        !returnUrl.StartsWith("//") &&
                        !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", ExceptionMessages.InvalidCredentialsErrorMessage);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [Authorize]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [Authorize, HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", ErrorCodeToString(createStatus));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize, HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    var currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }

                ModelState.AddModelError("", ExceptionMessages.InvalidPasswordErrorMessage);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return ExceptionMessages.MembershipCreateStatus_DuplicateUserName;

                case MembershipCreateStatus.DuplicateEmail:
                    return ExceptionMessages.MembershipCreateStatus_DuplicateEmail;

                case MembershipCreateStatus.InvalidPassword:
                    return ExceptionMessages.MembershipCreateStatus_InvalidPassword;

                case MembershipCreateStatus.InvalidEmail:
                    return ExceptionMessages.MembershipCreateStatus_InvalidEmail;

                case MembershipCreateStatus.InvalidAnswer:
                    return ExceptionMessages.MembershipCreateStatus_InvalidAnswer;

                case MembershipCreateStatus.InvalidQuestion:
                    return ExceptionMessages.MembershipCreateStatus_InvalidQuestion;

                case MembershipCreateStatus.InvalidUserName:
                    return ExceptionMessages.MembershipCreateStatus_InvalidUserName;

                case MembershipCreateStatus.ProviderError:
                    return ExceptionMessages.MembershipCreateStatus_ProviderError;

                case MembershipCreateStatus.UserRejected:
                    return ExceptionMessages.MembershipCreateStatus_UserRejected;

                default:
                    return ExceptionMessages.GenericExceptionMessage;
            }
        }
        #endregion
    }
}