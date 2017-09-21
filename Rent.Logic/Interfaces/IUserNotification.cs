using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Business.Interfaces
{
    public interface IUserNotification
    {
        IEnumerable<Entities.UsersNotification> Select();
        Entities.UsersNotification SelectByNotificationId(object id);
        Entities.UsersNotification SelectByUid(object id);
        void Update(Entities.UsersNotification objUsersNotification);
        void Insert(Entities.UsersNotification objUsersNotification);
    }
}
