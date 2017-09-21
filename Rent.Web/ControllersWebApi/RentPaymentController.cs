using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Rent.Business.Services;
using Rent.Entities;
using Rent.Entities.Models;
using RentPayment = Rent.Business.Services.RentPayment;
using User = Rent.Business.Services.User;


namespace Rent.Web.ControllersWebApi
{
    [Authorize(Roles = "Admin, Manager")]
    public class RentPaymentController : ApiController
    {
        private Rent.Business.Interfaces.IRentPayment iRentPayment;
        private Rent.Business.Interfaces.IUser iUser;

        public RentPaymentController()
        {
            iRentPayment = new RentPayment(new RentEntities());
            iUser = new User();
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            return Ok(iRentPayment.SelectByUidList(id));
        }

        [HttpGet]
        public IHttpActionResult GetUsers(int id)
        {
            return Ok(iUser.SelectList(id));
        }

        // POST api/<controller>
        public IHttpActionResult Post(Entities.Models.Paging obj)
        {
            IEnumerable<Entities.RentPayment> _objRentPayment = null;

            // log active
            Models.LogModels.CreateUserLog("Web Api Post - RentPayment", 1, HttpContext.Current.Request.UserHostAddress);

            // paging object
            Entities.Models.Paging objPaging = new Paging();
            
            objPaging.Search = obj.Search;
            objPaging.CurrentPage = (obj.CurrentPage == null ? 0 : obj.CurrentPage);
            objPaging.PageSize = obj.PageSize;
            objPaging.ActionId = 2;
            objPaging.Id = obj.Id;


            var viewModel = iRentPayment.Select(objPaging).ToList().Select(Mapper.Map<Entities.RentPayment, Rent.Web.ViewModels.RentPaymentViewModel>);
            
            return Ok(viewModel);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(Entities.RentPayment objRentPayment)
        {
            if (!ModelState.IsValid)
                return new BadRequestErrorMessageResult("Error occured during saving data.",null);

            // log active
            Models.LogModels.CreateUserLog("Web Api Put - RentPayment", 1, HttpContext.Current.Request.UserHostAddress);

            var rentPayment = objRentPayment.RentPaymentId > 0 ? iRentPayment.Select(objRentPayment.RentPaymentId) : new Entities.RentPayment();
            rentPayment.Payment = objRentPayment.Payment;
            rentPayment.PaymentDate = objRentPayment.PaymentDate;
            rentPayment.Active = objRentPayment.Active;
            rentPayment.Uid = objRentPayment.Uid;

            if (objRentPayment.RentPaymentId > 0)
                iRentPayment.Update(rentPayment);
            else
                iRentPayment.Insert(rentPayment);

            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            iRentPayment.Delete(id);

            return Ok();
        }
    }
}