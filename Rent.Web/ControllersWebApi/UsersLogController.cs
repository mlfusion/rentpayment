using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rent.Business.Services;
using Rent.Entities;
using Rent.Entities.Models;

namespace Rent.Web.ControllersWebApi
{
    [Authorize(Roles = "Admin, Manager")]
    public class UsersLogController : ApiController
    {
        private Business.Interfaces.IUserLog iUserLog;

        public UsersLogController()
        {
            iUserLog = new UserLog(new RentEntities());
        }

        public string Get(int id)
        {
            return "Hello, " + id;
        }

        // GET api/<controller>/5
        public IEnumerable<Entities.UsersLog> Post(Entities.Models.Paging obj)
        {
            IEnumerable<Entities.UsersLog> obUsersLogs = null;

            // paging object
            Entities.Models.Paging objPaging = new Paging();
            objPaging.Search = obj.Search;
            objPaging.CurrentPage = (obj.CurrentPage == null ? 0 : obj.CurrentPage);
            objPaging.PageSize = obj.PageSize;

            //if (User.IsInRole("Admin"))
            //{
            //    objPaging.ActionId = 1;
            //    objPaging.Id = obj.Id;
            //    _objRentPayment = _objSqlDb.RentPaymentSelectBySearch(objPaging);
            //}
            //else
            {
                objPaging.ActionId = 2;
                objPaging.Id = obj.Id;
                obUsersLogs = iUserLog.Select(objPaging);
            }

            return obUsersLogs;
        }
    }
}