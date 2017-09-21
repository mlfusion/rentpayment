using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Business.Interfaces
{
   public interface IRoles
   {
       IEnumerable<Entities.Role1> Select();
       IEnumerable<Entities.Role1> SelectList(object id);
       IEnumerable<Entities.User> ManagersList();
   }
}
