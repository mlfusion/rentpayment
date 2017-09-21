using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rent.Business.Services;

namespace Rent.Web.ControllersWebApi
{
    public class UsersController : ApiController
    {
        private Rent.Business.Interfaces.IUser iUser;

        public UsersController()
        {
            iUser = new User();
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public IEnumerable<Entities.User> GetUsers(int id)
        {
            return iUser.SelectList(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}