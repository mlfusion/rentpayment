using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rent.Business.Services;
using Rent.Entities;
using System.Web.Mvc;
using UserLog = Rent.Business.Services.UserLog;

namespace Rent.Web.Models
{
    public class LogModels
    {
        private static Business.Interfaces.IUserLog _objIUserLog;

        public LogModels()
        {
            _objIUserLog = new UserLog(new RentEntities());
        }

        public static void CreateUserLog(string page, int uid, string ipAddress)
        {
           Business.Interfaces.IUserLog _objIUserLog = new UserLog(new RentEntities());
            // log active
            Entities.UsersLog usersLog = new UsersLog();
            usersLog.IpAddress = ipAddress;
            usersLog.Uid = uid == 1 ? 1 : uid;
            usersLog.Page = page;
            _objIUserLog.Insert(usersLog);
        }

    }
}