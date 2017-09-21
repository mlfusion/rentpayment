using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Business.Interfaces
{
   public interface IUserAccess
   {
       string Update(Entities.Models.AccessCode obj);
       string Insert(string accessCode, int id);
   }
}
