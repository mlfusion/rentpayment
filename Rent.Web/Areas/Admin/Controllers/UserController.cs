using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.Util;
using Microsoft.Ajax.Utilities;
using PagedList;
using Rent.Business.Interfaces;
using Rent.Business.Services;
using Rent.Entities;
using Rent.Entities.Models;
using Rent.Web.Areas.Rent;
//using System.Transactions;
using User = Rent.Business.Services.User;
using UserRole = Rent.Business.Services.UserRole;
using UsersPassword = Rent.Business.Services.UsersPassword;
using LogEmail = Rent.Business.Services.LogEmail;
using RentPayment = Rent.Entities.RentPayment;

namespace Rent.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class UserController : Controller
    {

        private Business.Interfaces.IUser _objUser;
        private Business.Interfaces.IRoles _objRole;
        private Business.Interfaces.IUserPassword _objUserPassword;
        private Business.Interfaces.IUserLog _objIUserLog;
        private Business.Interfaces.ILogEmail _oblLogEmail;
        private Business.Interfaces.ISqlSelectDb _objSqlDb;
        private int itemsPagePage = 5;

        public UserController()
        {
            _objUser = new User(new RentEntities());
            _objRole = new Roles(new RentEntities());
            _objUserPassword = new UsersPassword(new RentEntities());
            _objIUserLog = new UserLog(new RentEntities());
            _oblLogEmail = new LogEmail(new RentEntities());
            _objSqlDb = new SqlSelectDb();
        }

        //
        // GET: /Admin/User/
        public ActionResult Index(string searchString, int? page = 1, int? searchPage = 0)
        {
            page = page - 1;
            // paging object
            Entities.Models.Paging objPaging = new Paging();
            objPaging.Search = searchString;
            objPaging.CurrentPage =(int) (searchString != null ? page = 0 : (int) (page == null ? 0 : page));
            objPaging.PageSize = searchString != null ? 20 : itemsPagePage;
            objPaging.Id = (int) Session["UID"];
            var obj = _objSqlDb.UserSelectBySearch(objPaging);

            // log active
            Models.LogModels.CreateUserLog("User_Index", (int)Session["UID"], Request.UserHostAddress);

            ViewBag.Page = page + 1;

            return (ActionResult) View(obj); //: PartialView("_pvListIndex", obj); 
        }

        public ActionResult PartialViewIndex(string searchString, int? page = 1)
        {
            page = page - 1;

            // paging object
            Entities.Models.Paging objPaging = new Paging();
            objPaging.Search = searchString;
            objPaging.CurrentPage = (int)(page == null ? 0 : page);
            objPaging.PageSize = itemsPagePage;
            objPaging.Id = (int)Session["UID"];
            var obj = _objSqlDb.UserSelectBySearch(objPaging);

            // log active
            Models.LogModels.CreateUserLog("User_PartialViewIndex", (int)Session["UID"], Request.UserHostAddress);

            ViewBag.Page = page + 1;

            return PartialView("~/Areas/Admin/Views/User/PartialViews/_pvListIndex.cshtml", obj);
        }

        //
        // GET: /Admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        //
        // GET: /Admin/User/Create
        public ActionResult Create()
        {
            ViewBag.RoleList = new SelectList(_objRole.SelectList((int) Session["UID"]), "RoleId", "Name", null);

            // Managers List - Add 6/2/2016
            ViewBag.ManagerList = new SelectList(_objRole.ManagersList(), "Uid", "Name", null);

            // log active
            Models.LogModels.CreateUserLog("User_Create", (int)Session["UID"], Request.UserHostAddress);
            return View();
        }

        //
        // POST: /Admin/User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Entities.User obj)
        {
            //ModelState.Remove("class.property");
            if (_objUser.Validate(obj,"insert"))
            {
                TempData["Msg"] = "Email address already exist.";
               // ModelState.AddModelError("User.Email", "Email address already exist.");
               // return RedirectToAction("Create");

                // Roles List
                //ViewBag.RoleList = new SelectList(_objRole.SelectList((int) Session["UID"]), "RoleId", "Name", null);

                // Managers List - Add 6/2/2016
               // ViewBag.ManagerList = new SelectList(_objRole.ManagersList(), "Uid", "Name", null);

                return RedirectToAction("Create");// View();
            }
             
            try
            {
                obj.ManagerId = User.IsInRole("Admin") ? obj.ManagerId > 0 ? obj.ManagerId : 0 : (int) Session["UID"];
                _objUser.Create(obj);

                TempData["Msg"] = "New users has been created.";
                return RedirectToAction("Create");
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int)Session["UID"]);

                TempData["Msg"] = "Error: " + exception.Message;
                return RedirectToAction("Create");// View();
            }
        }

        //
        // GET: /Admin/User/Edit/5
        public ActionResult Edit(int? id, int page = 0)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Page = page == null ? 0 : page;
            ViewBag.RoleList = new SelectList(_objRole.SelectList((int) Session["UID"]), "RoleId", "Name");

            // Managers List - Add 6/2/2016
            ViewBag.ManagerList = new SelectList(_objRole.ManagersList(), "Uid", "Name", null);

            //ViewBag.LogEmailByUid = _oblLogEmail.SelectByUid(id).OrderByDescending(x => x.Created);

            // log active
            Models.LogModels.CreateUserLog("User_Edit", (int)Session["UID"], Request.UserHostAddress);

            var obj = _objUser.Select(id);

            // store UserAccess active status in ViewBag
            var active = obj.UsersAccesses.FirstOrDefault();
            ViewBag.UserAccessActive = active != null && active.Active;

            return View(obj);
        }

        //
        // POST: /Admin/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Entities.User obj, int page)
        {
            try
            {
                //ModelState.Remove("class.property");
                if (_objUser.Validate(obj, "update"))
                {
                    TempData["Msg"] = "Email address already exist.";
                    return RedirectToAction("Edit");
                }

                _objUser.Update(obj);

                if (User.IsInRole("Admin"))
                {
                   Entities.UsersPassword usersPassword = new Entities.UsersPassword();
                    var objUsersPassword = _objUserPassword.Select(obj.Uid);

                    if (objUsersPassword != null)
                    {
                        objUsersPassword.Password = obj.Password;
                        objUsersPassword.Uid = obj.Uid;
                        _objUserPassword.Update(objUsersPassword);
                    }

                    // UsersManager - Added 6/6/2016
                    {
                        Entities.UsersManager objUsersManager = new UsersManager();
                        objUsersManager.ManagerId = (int) obj.ManagerId;
                        objUsersManager.Uid = obj.Uid;

                        Business.Interfaces.IUserManager userManager = new UserManager();
                        userManager.AddorUpdate(objUsersManager);
                    }
                }

                TempData["Msg"] = "User updated successfully.";

                //RedirectToLocal(page.ToString(), "Edit");
                return RedirectToAction("Edit", new {page = page});
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int)Session["UID"]);

                TempData["Msg"] = "Error: " + exception.Message;
                return RedirectToAction("Edit");
            }
        }

        private ActionResult RedirectToLocal(string returnUrl, string controller)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(controller);
            }
        }

        //
        // POST: /Admin/User/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, int page)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                // log active
                Models.LogModels.CreateUserLog("User_Delete", (int)Session["UID"], Request.UserHostAddress);

                // TODO: Add delete logic here
                _objUser.Delete(id);

                //TempData["Msg"] = "User has been deleted.";
                return RedirectToAction("PartialViewIndex", new { page = page });
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int)Session["UID"]);

                TempData["Msg"] = "Error: " + exception.Message;
                return RedirectToAction("PartialViewIndex");
            }
        }

        [HttpPost]
        public ActionResult Reactive(int? id, int page)
        {
            try
            {
                // log active
                Models.LogModels.CreateUserLog("User_Reactive", (int)Session["UID"], Request.UserHostAddress);

                // TODO: Add delete logic here
                var obj = _objUser.Select(id);
                obj.Active = true;
                _objUser.Update(obj);

                //TempData["Msg"] = "User has been reactived.";

                return RedirectToAction("PartialViewIndex", new { page = page });
            }
            catch (Exception exception)
            {
                // log error
                Business.Services.LogError.Insert(exception, (int)Session["UID"]);

                TempData["Msg"] = "Error: " + exception.Message;
                return RedirectToAction("PartialViewIndex", new { page = page });
            }
        }

        public ActionResult Search(string searchData)
        {
            return null;
        }

        public ActionResult _pvEditRentPaymentPartial()
        {
            Entities.RentPayment objRentPayment = new RentPayment();
            return PartialView("~/PartialViews/_pvEditRentPaymentPartial", objRentPayment);
        }

        public ActionResult _pvListRentPaymentPartial()
        {
            //Entities.RentPayment objRentPayment = new RentPayment();
            return PartialView("~/Areas/Admin/Views/User/PartialViews/_pvListRentPaymentPartial");
        }
    }
}
