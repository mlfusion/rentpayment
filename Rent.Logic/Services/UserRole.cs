using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Business.Interfaces;
using Rent.DataAccess.Repositories;
using Rent.Entities;

namespace Rent.Business.Services
{
   public class UserRole : IUserRole, IDisposable
    {
        private Rent.DataAccess.Interfaces.IGenericRepository<Rent.Entities.UsersRole> repository = null;

        public UserRole()
        {
            repository = new GenericRepository<Entities.UsersRole>(new RentEntities());
        }

        public UserRole(RentEntities context)
        {
            repository = new GenericRepository<Entities.UsersRole>(context);
        }

        public IEnumerable<Entities.UsersRole> Select()
        {
            return repository.Select();
        }

        public Entities.UsersRole Select(object id)
        {
            return repository.Select().FirstOrDefault(x => x.Uid == (int) id);
        }

        public void Insert(Entities.UsersRole obj)
        {
            obj.Modifed = null;
            obj.Created = DateTime.Now;
            obj.Active = true;
            repository.Insert(obj);
        }

        public void Update(Entities.UsersRole obj)
        {
            obj.Modifed = DateTime.Now;
            repository.Update(obj);
        }

       public void User_UserRoleInsert(Entities.User obj)
       {
           using (var context = new RentEntities())
           {
               var user = context.Users.SingleOrDefault(x => x.Uid == obj.Uid);
               var userRole = context.UsersRoles.SingleOrDefault(x => x.UserRoleId == obj.UsersRole.UserRoleId);

               context.Users.Add(user);
               context.UsersRoles.Add(userRole);

               //user.UsersRole.Add(userRole);
               ////userRole.User.UsersRoles.Add(user);

               
               //userRole.Users.Add(user);
               context.SaveChanges();
           }
       }

       public void User_UserRoleUpdate(Entities.User obj)
       {
           using (var context = new RentEntities())
           {
               var user = context.Users.SingleOrDefault(x => x.Uid == obj.Uid);
               var userRole = context.UsersRoles.SingleOrDefault(x => x.Uid == obj.Uid);

               context.Users.Remove(user);
               context.UsersRoles.Remove(userRole);

               context.Users.Add(user);
               context.UsersRoles.Add(userRole);

               //user.UsersRoles.Add(userRole);
               //userRole.Users.Add(user);
               context.SaveChanges();
           }
       }

       public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
