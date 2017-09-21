using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Entities;

namespace Rent.Business.Interfaces
{
    public interface IRentPaymentNoticeSendLog
    {
        IEnumerable<RentPaymentNoticeSendLog> Select();
        RentPaymentNoticeSendLog Select(object id);
        RentPaymentNoticeSendLog SelectByRentPaymentId(object id);
        void Insert(RentPaymentNoticeSendLog obj);
        void Update(RentPaymentNoticeSendLog obj);
    }
}
