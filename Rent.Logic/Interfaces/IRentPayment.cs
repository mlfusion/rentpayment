using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Entities;

namespace Rent.Business.Interfaces
{
    public interface IRentPayment
    {
        IEnumerable<RentPayment> Select();
        IEnumerable<RentPayment> Select(Entities.Models.Paging obj);
        IEnumerable<RentPayment> SelectByUidList(object id);
        IEnumerable<RentPayment> SelectByUid(object id);
        RentPayment Select(object id);
        void Insert(RentPayment obj);
        void Update(RentPayment obj);
        void Delete(object id);
    }
}
