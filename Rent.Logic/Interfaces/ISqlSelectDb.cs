using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Entities;
using Rent.Entities.Models;

namespace Rent.Business.Interfaces
{
    public interface ISqlSelectDb 
   {
      // DataTable SqlSelectBySearch(Entities.Models.Paging obj, string storeProc);
       IEnumerable<Entities.User> UserSelectBySearch(Paging objPaging);
       IEnumerable<Entities.UsersLog> UsersLogSelectBySearch(Paging objPaging);
       IEnumerable<Entities.RentPayment> RentPaymentSelectBySearch(Paging objPaging);

        DataTable Select(string storeProc, SqlParameter[] oSqlParameters);
        int Update(string storeProc, SqlParameter[] oSqlParameters);
        int Delete(string storeProc, SqlParameter[] oSqlParameters);
   }
}
