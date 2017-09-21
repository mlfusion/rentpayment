using Rent.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.DataAccess.Repositories;
using Rent.Entities;

namespace Rent.Business.Services
{
    public class UserNotification : IUserNotification, IDisposable
    {
        private Rent.DataAccess.Interfaces.IGenericRepository<Entities.UsersNotification> repository;

        public UserNotification()
        {
            repository = new GenericRepository<Entities.UsersNotification>(new RentEntities());
        }

        public UserNotification(Entities.RentEntities context)
        {
            repository = new GenericRepository<UsersNotification>(context);
        }

        public IEnumerable<UsersNotification> Select()
        {
            return repository.Select();
        }

        public UsersNotification SelectByNotificationId(object id)
        {
            return repository.Select().FirstOrDefault(x => x.UserNotificationId == (int) id);
        }

        public UsersNotification SelectByUid(object id)
        {
            var d = repository.Select().FirstOrDefault(x => x.Uid == (int)id);

            return d;
        }

        public void Update(UsersNotification objUsersNotification)
        {
            objUsersNotification.Modifed = DateTime.Now;
            repository.Update(objUsersNotification);
        }

        public void Insert(UsersNotification objUsersNotification)
        {
            objUsersNotification.Email = true;
            objUsersNotification.Phone = true;
            objUsersNotification.Created = DateTime.Now;
            objUsersNotification.Modifed = null;
            repository.Insert(objUsersNotification);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
