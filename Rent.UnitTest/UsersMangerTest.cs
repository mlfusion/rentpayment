using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rent.Business.Services;
using Rent.Entities;

namespace Rent.UnitTest
{
      [TestClass]
    public class UsersMangerTest
    {
          [TestMethod]
          public void AddorUpdateMethod()
          {
              // UsersManager - Added 6/6/2016
              {
                  Entities.UsersManager objUsersManager = new UsersManager();
                  objUsersManager.ManagerId = 1002;
                  objUsersManager.Uid = 1017;

                  Business.Interfaces.IUserManager userManager = new UserManager();
                  userManager.AddorUpdate(objUsersManager);

                  Assert.IsNotNull("Pass");
              }
          }
    }
}
