using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Entities;

namespace Rent.Business.Interfaces
{
    public interface IUserRole
    {
        IEnumerable<Entities.UsersRole> Select();
        UsersRole Select(object id);
        void Insert(Entities.UsersRole obj);
        void Update(Entities.UsersRole obj);
    }
}
