using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rent.Business.Services;

namespace Rent.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class RegistrationController : Controller
    {
        private Business.Interfaces.IUserAccess objUserAccess;
        private Business.Interfaces.IEmail objEmail;

        public RegistrationController()
        {
            objUserAccess = new UserAcess();
            objEmail = new Email();
        }

        //
        // GET: /Rent/Registration/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registration(Entities.Models.AccessCode obj)
        {
            try
            {
                // log active
                Models.LogModels.CreateUserLog("Registration_Registration", (int)Session["UID"], Request.UserHostAddress);

                var r = objUserAccess.Update(obj);

                return Content(r);
            }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }
        }

        [HttpPost]
        public ActionResult SendRegistration(Entities.User obj)
        {
            try
            {
                // log active
                Models.LogModels.CreateUserLog("Registration_SendRegistration", (int)Session["UID"], Request.UserHostAddress);

                return Content(objEmail.UserRegistrationEmail(obj.Uid));
                //return Content("Registration email send successfully." + obj.Uid); 
            }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }
        }
    }
}