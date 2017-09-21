using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Rent.Business.Interfaces;
using Rent.DataAccess.Repositories;
using Rent.DataAccess.SqlDataAccess;
using Rent.Entities;

namespace Rent.Business.Services
{
   public class User : IUser, IDisposable
    {
       // DataAccess
        private Rent.DataAccess.Interfaces.IGenericRepository<Entities.User> repository = null;

        public User()
        {
            repository = new GenericRepository<Entities.User>(new RentEntities());
        }

        public User(RentEntities context)
        {
            repository = new GenericRepository<Entities.User>(context);
        }

        public IEnumerable<Entities.User> Select()
        {
            return repository.Select();
        }

        public Entities.User Select(object id)
        {
            var r = repository.Select(id);

            // Pre set default values
            r.RoleId = r.UsersRoles.FirstOrDefault().RoleId;
            r.Password = r.UsersPasswords.FirstOrDefault().Password;
            r.EmailNotification = (bool) r.UsersNotifications.FirstOrDefault().Email;
            r.PhoneNotification = (bool) r.UsersNotifications.FirstOrDefault().Phone;

            if (r.UsersManagers.Count > 0)
                r.ManagerId = r.UsersManagers.FirstOrDefault().ManagerId;

            return r;
        }

        public IEnumerable<Entities.User> SelectList(object id)
        {
            Rent.DataAccess.SqlDataAccess.ISqlDbHelper iSqlDbHelper = new SqlDbHelper();

            var storeProc = "UsersList_Select";

            SqlParameter[] objSqlParameters = new SqlParameter[1];
            objSqlParameters[0] = new SqlParameter("@Uid", id);

            var d = iSqlDbHelper.ExecuteDataTable(storeProc, CommandType.StoredProcedure, objSqlParameters);
            IEnumerable<Entities.User> users = d.AsEnumerable().Select(x => new Entities.User()
            {
                Uid = x.Field<int>("Uid"),
                Name = x.Field<string>("Name")
            }).ToList();

            return users;
        }

        public void Insert(Entities.User obj)
        {
            obj.Created = DateTime.Now;
            obj.Modifed = null;
            obj.Active = false;
            obj.Email = obj.Email.ToLower();
            repository.Insert(obj);
        }

       public void Update(Entities.User obj)
       {
           // transaction scope
           using (var trans = new System.Transactions.TransactionScope())
           {
               {
                   // Users
                   obj.Email = obj.Email.ToLower();
                   obj.Modifed = DateTime.Now;
                   repository.Update(obj);

                   // UsersPassword
               }

               {
                   // UsersRole
                   Rent.Business.Interfaces.IUserRole userRole = new UserRole();
                   var role = userRole.Select(obj.Uid);
                   role.RoleId = obj.RoleId;
                   userRole.Update(role);
               }



               {
                  // if (obj.UsersNotification != null)
                   {
                       // UsersNotification
                       Rent.Business.Interfaces.IUserNotification _iUsersNotificationRepository = new UserNotification();
                       var userNotification = _iUsersNotificationRepository.SelectByUid(obj.Uid);
                       userNotification.Phone = obj.PhoneNotification;
                       userNotification.Email = obj.EmailNotification;
                       _iUsersNotificationRepository.Update(userNotification);
                   }
               }

               // commit transaction
               trans.Complete();
           }
       }

       public void Delete(object id)
        {
            var obj = Select(id);
            obj.Modifed = DateTime.Now;
            obj.Active = false;

            repository.Update(obj);
        }

       public void Create(Entities.User obj)
       {
           // transaction scope
           using (var trans = new System.Transactions.TransactionScope())
           {

               // insert into Users
               Insert(obj);

               // UsersRole object
               Entities.UsersRole objUsersRole = new UsersRole();
               objUsersRole.RoleId = obj.RoleId;
               objUsersRole.Uid = obj.Uid;

               // insert into UsersRole
               Rent.Business.Services.UserRole userRole = new UserRole();
               userRole.Insert(objUsersRole);

               // UsersPassword object
               Entities.UsersPassword objUsersPassword = new Entities.UsersPassword();
               objUsersPassword.Uid = obj.Uid;
               objUsersPassword.Password = obj.UsersPassword.Password;

               // insert into UsersPassword
               Rent.Business.Services.UsersPassword usersPassword = new UsersPassword();
               usersPassword.Insert(objUsersPassword);

               // UsersNotification object
               Entities.UsersNotification objUsersNotification = new UsersNotification();
               objUsersNotification.Uid = obj.Uid;

               // insert into UsersNotification
               Rent.Business.Services.UserNotification userNotification = new UserNotification();
               userNotification.Insert(objUsersNotification);

               // into into UserAccessCode
               Rent.Business.Interfaces.IUserAccess iUserAccess = new UserAcess();
               iUserAccess.Insert(Rent.Common.Helper.Generator.AccessCode(), obj.Uid);

               // UsersManager
               if (obj.ManagerId > 0)
               {
                   Rent.Entities.UsersManager objUsersManager = new UsersManager();
                   objUsersManager.ManagerId = (int) obj.ManagerId;
                   objUsersManager.Uid = obj.Uid;

                   // into into UsersUsersManager
                   Rent.Business.Interfaces.IUserManager iUserManager = new UserManager();
                   iUserManager.Insert(objUsersManager);
               }

               // commit transaction
               trans.Complete();
           }
       }

       public bool Validate(Entities.User obj, string status)
       {
           bool b = true;

           using (Rent.Entities.RentEntities context = new RentEntities())
           {
               switch (status)
               {
                   case "update":
                       b = context.Users.Any(x => x.Email == obj.Email.ToLower() && x.Active && x.Uid != obj.Uid);
                       break;
                   case "insert":
                       b = context.Users.Any(x => x.Email == obj.Email.ToLower() && x.Active);
                       break;
               }
           }

           return b;
       }

       public void Dispose()
       {
          GC.SuppressFinalize(this);
       }
    }
}
