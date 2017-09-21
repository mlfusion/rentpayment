using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Business.Interfaces;
using Rent.DataAccess.Repositories;
using Rent.DataAccess.SqlDataAccess;
using Rent.Entities;

namespace Rent.Business.Services
{
   public class Roles : IRoles
    {
       private Rent.DataAccess.Interfaces.IGenericRepository<Rent.Entities.Role1> repository = null;
       

       public Roles()
       {
           
       }

       public Roles(RentEntities context)
       {
           repository = new GenericRepository<Role1>(context);
       }

       public IEnumerable<Role1> Select()
       {
           throw new NotImplementedException();
       }

       public IEnumerable<Entities.Role1> SelectList(object id)
       {
           Rent.DataAccess.SqlDataAccess.ISqlDbHelper iSqlDbHelper = new SqlDbHelper();

           var storeProc = "RoleList_Select";

           SqlParameter[] objSqlParameters = new SqlParameter[1];
           objSqlParameters[0] = new SqlParameter("@Uid", id);

           var d = iSqlDbHelper.ExecuteDataTable(storeProc, CommandType.StoredProcedure, objSqlParameters);
           IEnumerable<Entities.Role1> users = d.AsEnumerable().Select(x => new Entities.Role1()
           {
               RoleId = x.Field<int>("RoleId"),
               Name = x.Field<string>("Name")
           }).ToList();

           return users;
       }

       public IEnumerable<Entities.User> ManagersList()
       {
           Rent.DataAccess.SqlDataAccess.ISqlDbHelper iSqlDbHelper = new SqlDbHelper();

           var storeProc = "ManagerList";

           var d = iSqlDbHelper.ExecuteDataTable(storeProc, CommandType.StoredProcedure);
           IEnumerable<Entities.User> users = d.AsEnumerable().Select(x => new Entities.User()
           {    
               Uid = x.Field<int>("Uid"),
               Name = x.Field<string>("Name")
           }).ToList();

           return users;
       }

    }
}
