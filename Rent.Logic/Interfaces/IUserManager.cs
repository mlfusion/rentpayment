using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Entities;

namespace Rent.Business.Interfaces
{
    public interface IUserManager
    {
        IEnumerable<UsersManager> Select();
        IEnumerable<UsersManager> Select(object id);
        UsersManager SelectByUId(object id);
        void Insert(UsersManager obj);
        void Update(UsersManager obj);
        void Delete(object id);
        void AddorUpdate(UsersManager obj);
    }
}
