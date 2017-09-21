using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rent.Business.Services;
using Rent.Entities;
using LogError = Rent.Business.Services.LogError;
using UsersLog = Rent.Business.Services.UserLog;
using  RentPaymentNoticeSendLog = Rent.Business.Services.RentPaymentNoticeSendLog;

namespace Rent.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class LogController : Controller
    {
        private Business.Interfaces.IUserLog _objIUserLog;
        private Business.Interfaces.IRentPaymentNoticeSendLog _objIRentPaymentNoticeSendLog;
        private Business.Interfaces.ILogError objLogError;

        public LogController()
        {
            _objIUserLog = new UsersLog(new RentEntities());
            _objIRentPaymentNoticeSendLog = new RentPaymentNoticeSendLog(new RentEntities());
            objLogError = new LogError(new RentEntities());
        }

        public ActionResult Users()
        {
            return View(_objIUserLog.Select());
        }

        public ActionResult Web()
        {
            return View();
        }

        public ActionResult Notices()
        {
            return View(_objIRentPaymentNoticeSendLog.Select());
        }

        public ActionResult Errors()
        {
            return View(objLogError.Select());
        }
    }
}
