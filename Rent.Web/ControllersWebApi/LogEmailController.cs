using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rent.Entities;
using Rent.Entities.Models;
using LogEmail = Rent.Business.Services.LogEmail;

namespace Rent.Web.ControllersWebApi
{
    [Authorize(Roles = "Admin, Manager")]
    public class LogEmailController : ApiController
    {
        private Business.Interfaces.ILogEmail iLogEmail;

        public LogEmailController()
        {
            iLogEmail = new LogEmail(new RentEntities());
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IEnumerable<Entities.LogEmail> Post(Entities.Models.Paging obj)
        {
            IEnumerable<Entities.LogEmail> objLogEmails = null;

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
                objLogEmails = iLogEmail.Select(objPaging);
            }

            return objLogEmails;
        }
    }
}