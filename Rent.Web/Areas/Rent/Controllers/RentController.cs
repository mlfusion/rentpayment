using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rent.Business.Services;
using Rent.Entities;
using Rent.Entities.Models;
using RentPayment = Rent.Business.Services.RentPayment;
using User = Rent.Business.Services.User;
using UsersPassword = Rent.Business.Services.UsersPassword;
using Microsoft.Reporting.WebForms;

namespace Rent.Web.Areas.Rent.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class RentController : Controller
    {

        private Business.Interfaces.IRentPayment objRentPayment;
        private Business.Interfaces.IUserPassword objUserPassword;
        private Business.Interfaces.IUser _objUser;
        private Business.Interfaces.IEmail _objEmail;
        private Business.Interfaces.ISqlSelectDb _objSqlDb;

        public RentController()
        {
            objRentPayment = new RentPayment(new Entities.RentEntities());
            objUserPassword = new UsersPassword(new Entities.RentEntities());
            _objUser = new User(new Entities.RentEntities());
            _objEmail = new Email();
            _objSqlDb = new SqlSelectDb();
        }

        //
        // GET: /Rent/Rent/
        [AllowAnonymous]
        public ActionResult Index(string searchString, int? page, int? itemsPerPage = 20)
        {
            try
            {
                IEnumerable<Entities.RentPayment> _objRentPayment = null;

                // if password is system default, redirect to update password
                if (objUserPassword.DefaultPassword((int) Session["UID"]))
                    return RedirectToAction("Edit", "Password", new {id = (int) Session["UID"]});

                // log active
                Models.LogModels.CreateUserLog("Rent_Index", (int) Session["UID"], Request.UserHostAddress);

                // paging object
                Entities.Models.Paging objPaging = new Paging();
                objPaging.Search = searchString;
                objPaging.CurrentPage = (int) (page == null ? 0 : page);
                objPaging.PageSize = (int) itemsPerPage;

                //if (User.IsInRole("Admin"))
                //{
                //    objPaging.ActionId = 1;
                //    objPaging.Id = 0;
                //    _objRentPayment = _objSqlDb.RentPaymentSelectBySearch(objPaging);
                //}
                //else if (User.IsInRole("Manager"))
                //{
                //    objPaging.ActionId = 3;
                //    objPaging.Id = (int)Session["UID"];
                //    _objRentPayment = _objSqlDb.RentPaymentSelectBySearch(objPaging);                  
                //}
                //else
                //{
                    objPaging.ActionId = 0;
                    objPaging.Id = (int) Session["UID"];
                    _objRentPayment = objRentPayment.Select(objPaging);
               // }

                return View(_objRentPayment);
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int) Session["UID"]);

                return RedirectToAction("Login");
            }
        }

        //
        // GET: /Rent/Rent/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            return View(objRentPayment.Select(id));
        }

        //
        // GET: /Rent/Rent/Create
        public ActionResult Create()
        {
            // log active
            Models.LogModels.CreateUserLog("Rent_Create", (int)Session["UID"], Request.UserHostAddress);

            // bind ddl
            ViewBag.UserList = new SelectList(_objUser.SelectList((int)Session["UID"]), "UID", "Name", null);
            return View();
        }

        //
        // POST: /Rent/Rent/Create
        [HttpPost]
        public ActionResult Create(Entities.RentPayment obj)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                    return View();

                objRentPayment.Insert(obj);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int)Session["UID"]);

                return View();
            }
        }

        //
        // GET: /Rent/Rent/Edit/5
        public ActionResult Edit(int id)
        {
            // log active
            Models.LogModels.CreateUserLog("Rent_Edit", (int)Session["UID"], Request.UserHostAddress);

            return View(objRentPayment.Select(id));
        }

        //
        // POST: /Rent/Rent/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Entities.RentPayment obj)
        {
            try
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Create"); // View();

                // TODO: Add update logic here
                objRentPayment.Update(obj);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int)Session["UID"]);

                return View();
            }
        }


        //
        // POST
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                //if ((int) Session["UID"] != null)
                //{
                //    // log active
                //    Models.LogModels.CreateUserLog("Rent_Login", (int)Session["UID"], Request.UserHostAddress);
                //    return RedirectToAction("Index");
                //}

                //Session["UID"] = null;
                //System.Web.Security.FormsAuthentication.SignOut();

                ViewBag.ReturnUrl = returnUrl;
                return View();

            }
            catch (Exception exception)
            {
                //Models.LogModels.CreateUserLog("Rent_Login", 1, Request.UserHostAddress);

                // log error
                Business.Services.LogError.Insert(exception, 0);

                Session["UID"] = null;
                System.Web.Security.FormsAuthentication.SignOut();

                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
        }

        //POST
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Entities.UsersPassword obj, string returnUrl)
        {
            // if (!ModelState.IsValid)
            //     return View();

            try
            {
                var objUser = objUserPassword.Login(obj);
                if (objUser <= 0)
                {
                    TempData["Msg"] = "Email or Password was not found.";
                    return View();
                }

                // log active
                Models.LogModels.CreateUserLog("Rent_LoginSuccess", 1, Request.UserHostAddress);

                // User is Authentication
                System.Web.Security.FormsAuthentication.SetAuthCookie(obj.User.Email.ToLower(), false);

                //set session id
                Session.Add("UID", objUser);

                //redirect page if returnUrl is not null
                return RedirectToLocal(returnUrl);

                //return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, 0);

                TempData["Msg"] = "Error: " + exception.Message;
                return View();
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            // rest session id
            // log active
            Models.LogModels.CreateUserLog("Rent_LogOff", (int)Session["UID"], Request.UserHostAddress);

            Session["UID"] = null;
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        //
        [AllowAnonymous]
        public ActionResult Receipt(int id)
        {
            try
            {
                var obj = objRentPayment.Select(id);

                ViewBag.Address = obj.User.Address + " " + obj.User.City + ", " + obj.User.State + " " +
                                  obj.User.ZipCode;
                return (View(obj));
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int)Session["UID"]);

                return RedirectToAction("Index");
            }
        }                           

        //
        [AllowAnonymous]
        public FileResult Print(int id)
        {
            // log active               
            Models.LogModels.CreateUserLog("Rent_Print", (int) Session["UID"], Request.UserHostAddress);

            LocalReport localReport = new LocalReport();
            localReport.Refresh();

            localReport.ReportPath = Server.MapPath("~/Report/PaymentReceipt.rdlc");

            IEnumerable<Entities.RentPayment> payments = new[] {objRentPayment.Select(id)};
            IEnumerable<Entities.User> users = new[] {objRentPayment.Select(id).User};

            ReportDataSource reportDataSource1 = new ReportDataSource("PaymentDataSet", payments);
            ReportDataSource reportDataSource2 = new ReportDataSource("UsersDataSet", users);

            localReport.DataSources.Add(reportDataSource1);
            localReport.DataSources.Add(reportDataSource2);

            localReport.Refresh();

            //  var reportype = 
            string mimeType, encoding;
            string fileNameExtension = "pdf";
            Warning[] warnings;
            string[] streams;
            byte[] reneredBytes;

            reneredBytes = localReport.Render("PDF", "", out mimeType, out encoding, out fileNameExtension, out streams,
                out warnings);
            Response.AddHeader("content-disposition",
                "attachment; filename=RentPaymentReceipt_" + id + "." + fileNameExtension);

            return File(reneredBytes, fileNameExtension);
        }

        // [AllowAnonymous]
        //public ActionResult InvoicePDF(int id)
        //{
        //    ViewBag.Address = "1321 Hampton Hill ct Lawrenceville, GA 30044";
        //    return View(objRentPayment.Select(id));
        //}

        public ActionResult Search(string id)
        {
            return null;
        }

        public ActionResult RentPaymentEmail(int id)
        {
            // log active
            Models.LogModels.CreateUserLog("Rent_RentPaymentEmail", (int)Session["UID"], Request.UserHostAddress);

            TempData["Msg"] = _objEmail.RentPaymentEmail(id);
            return RedirectToAction("Index");// View("Index");
        }
    }
}
