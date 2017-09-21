using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rent.Business.Services;
using Rent.Entities;
using UsersPassword = Rent.Business.Services.UsersPassword;
using User = Rent.Business.Services.User;
using Rent.Web.Models;

namespace Rent.Web.Areas.Rent.Controllers
{
    [Authorize(Roles = "Admin, User, Manager")]
    public class PasswordController : Controller
    {
        private Business.Interfaces.IUserPassword _objUserPassword;
        private Business.Interfaces.IUser _objUser;
        private Business.Interfaces.IEmail _objEmail;

        public PasswordController()
        {
           _objUserPassword = new UsersPassword(new RentEntities());
           _objUser = new User(new RentEntities());
            _objEmail = new Email();
        }

        //
        // GET: /Rent/Password/Edit/5
        public ActionResult Edit()
        {
            // log active
           Models.LogModels.CreateUserLog("Password_Edit",(int) Session["UID"], Request.UserHostAddress);

           if ((int)Session["UID"] == null)
                return RedirectToAction("Index", "Rent", new { area = "Rent" });

            var obj = _objUserPassword.Select((int)Session["UID"]);

            ViewBag.DefaultPassword = obj.Password;

            obj.Password = null;

            return View(obj);
        }

        /* Bug TFS: 2393
         * Revision Date: 8/12/2015
         * Developer: Beau
         * Description: Add AddedBy as new paramemter
         */

        //
        // POST: /Rent/Password/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Entities.UsersPassword obj)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                _objUserPassword.Update(obj);
                TempData["Msg"] = "Password was been updated.";
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int)Session["UID"]);

                TempData["Msg"] = "Error: " + exception.Message;
            }
            return RedirectToAction("Edit", "Password", obj.Uid); // View(obj);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(Entities.User obj)
        {
            try
            {
                // log active
                Models.LogModels.CreateUserLog("Password_ForgotPassword", 1, Request.UserHostAddress);

                // TODO: Add delete logic here
                TempData["Msg"] = _objEmail.ForgotPasswordEmail(obj);
                return RedirectToAction("Login", "Rent"); 
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int)Session["UID"]);

                return RedirectToAction("Login", "Rent"); 
            }
        }
    }
}
