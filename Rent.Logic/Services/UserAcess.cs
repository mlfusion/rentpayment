using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Business.Interfaces;
using Rent.DataAccess.SqlDataAccess;

namespace Rent.Business.Services
{
   public class UserAcess : IUserAccess
    {
       public string Update(Entities.Models.AccessCode obj)
       {
           var result = "";

           try
           {
               SqlParameter[] parameters = new SqlParameter[2];
               parameters[0] = new SqlParameter("@AccessCode", obj.Code.ToUpper());
               parameters[1] = new SqlParameter("@ZipCode", obj.ZipCode);

               Rent.DataAccess.SqlDataAccess.ISqlDbHelper _iSqlDbHelper = new SqlDbHelper();

               var x = _iSqlDbHelper.ExecuteScalar("UsersAccess_Update", CommandType.StoredProcedure,
                   parameters);

               if (x > 0)
               {
                   // send email with username and password
                   Rent.Business.Interfaces.IEmail objEmail = new Email();
                   result = objEmail.UserAccessRegistrationEmail(x);
               }
               else
               {
                   result = "Incorrect access code. Please try again.";
               }
           }
           catch (Exception exception)
           {
               Business.Services.LogError.Insert(exception, 0);
               result = "Error: " + exception.Message;
           }
           return result;
       }
       
       public string Insert(string accessCode, int id)
       {
           var result = "";

           try
           {
               SqlParameter[] parameters = new SqlParameter[2];
               parameters[0] = new SqlParameter("@AccessCode", accessCode.ToUpper());
               parameters[1] = new SqlParameter("@Uid", id);

               Rent.DataAccess.SqlDataAccess.ISqlDbHelper _iSqlDbHelper = new SqlDbHelper();

               var x = _iSqlDbHelper.ExecuteScalar("UsersAccess_Insert", CommandType.StoredProcedure,
                   parameters);

               result = x > 0 ? null : "Invalid access code. Please try again.";
           }
           catch (Exception exception)
           {
               Business.Services.LogError.Insert(exception, 0);
               result = "Error: " + exception.Message;
           }
           return result;
       }

    }
}
