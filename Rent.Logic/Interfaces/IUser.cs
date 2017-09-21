using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Entities;

namespace Rent.Business.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> Select();
        User Select(object id);
        IEnumerable<User> SelectList(object id);
        void Insert(User obj);
        void Update(User obj);
        void Delete(object id);
        void Create(User obj);
        bool Validate(User obj, string status);
    }
}
