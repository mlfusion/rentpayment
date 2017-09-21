using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Entities;

namespace Rent.Business.Interfaces
{
    public interface IUserLog
    {
        void Insert(UsersLog obj);
        IEnumerable<Entities.UsersLog> Select();
        IEnumerable<Entities.UsersLog> Select(Entities.Models.Paging obj);
        IEnumerable<Entities.UsersLog> SelectByUid(object id);
    }
}
