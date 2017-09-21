using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rent.Business.Services;
using Rent.Entities;
using User = Rent.Business.Services.User;

namespace Rent.Web.Areas.Rent.Controllers
{
    [Authorize(Roles="Admin, User, Manager")]
    public class UserController : Controller
    {
        private Business.Interfaces.IUser _objUser;
        private Business.Interfaces.IUserPassword _objUserPassword;

        public UserController()
        {
            _objUser = new User(new RentEntities());
            _objUserPassword = new Business.Services.UsersPassword(new RentEntities());
        }
        //
        // GET: /Rent/User/Edit/5
        public ActionResult Edit(int? id)
        {
            // log active
            Models.LogModels.CreateUserLog("User_Edit", (int)Session["UID"], Request.UserHostAddress);

            if (id == null)
                return View();

            var objUser = _objUser.Select((int)Session["UID"]);
            return View(objUser);
        }

        //
        // POST: /Rent/User/Edit/5
        [HttpPost]
        public ActionResult Edit(Entities.User obj)
        {
            try
            {
                ModelState.Remove("Created");
                ModelState.Remove("UserRoleId");
                ModelState.Remove("Modifed");
                ModelState.Remove("Active");

                if (!ModelState.IsValid)
                    return View();

                var _user = _objUser.Select(obj.Uid);
                _user.Name = obj.Name;
                _user.Email = obj.Email;
                _user.Phone = obj.Phone;
                _objUser.Update(_user);

                TempData["Msg"] = "Info has been updated.";
                return View();// RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int)Session["UID"]);

                TempData["Msg"] = "Error occured. Please try again later.";
                return View();
            }
        }
    }
}
