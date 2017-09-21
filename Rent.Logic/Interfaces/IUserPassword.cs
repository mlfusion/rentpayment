using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Entities;

namespace Rent.Business.Interfaces
{
   public interface IUserPassword
    {
        IEnumerable<UsersPassword> Select();
        UsersPassword Select(object id);
        void Insert(UsersPassword obj);
        void Update(UsersPassword obj);
        void Delete(object id);

        int Login(UsersPassword obj);
        bool DefaultPassword(object id);
    }
}
