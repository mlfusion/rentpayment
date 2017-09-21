using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rent.Business.Services;
using Rent.Entities;
using UserNotification = Rent.Business.Services.UserNotification;

namespace Rent.Web.Areas.Rent.Controllers
{
    [Authorize(Roles="Admin, User, Manager")]
    public class NotificationController : Controller
    {
        private Business.Interfaces.IUserNotification _iUserNotification;

        public NotificationController()
        {
            _iUserNotification = new UserNotification(new RentEntities());
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                //return View();
                return View(_iUserNotification.SelectByUid((int)Session["UID"]));
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int)Session["UID"]);

                TempData["Msg"] = "Error: " + exception.Message;
                return RedirectToAction("Index", "Rent");
            }
        }

        [HttpPost]
        public ActionResult Edit(Entities.UsersNotification obj)
        {
            try
            {
                if (obj.UserNotificationId <= 0)
                    _iUserNotification.Insert(obj);
                else
                    _iUserNotification.Update(obj);

                TempData["Msg"] = "Notification has been updated.";
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int)Session["UID"]);

                TempData["Msg"] = "Error: " + exception.Message;
            }
            return View(obj);
        }
	}
}