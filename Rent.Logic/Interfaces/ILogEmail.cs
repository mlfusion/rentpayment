using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Business.Interfaces
{
    public interface ILogEmail
    {
        IEnumerable<Entities.LogEmail> Select();
        IEnumerable<Entities.LogEmail> SelectByUid(object id);
        IEnumerable<Entities.LogEmail> Select(Entities.Models.Paging objPaging);
    }
}
