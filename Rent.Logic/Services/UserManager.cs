using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Business.Interfaces;
using Rent.DataAccess.Repositories;
using Rent.Entities;

namespace Rent.Business.Services
{
   public class UserManager : IUserManager
   {
       private Rent.DataAccess.Interfaces.IGenericRepository<Entities.UsersManager> repository;
       private Rent.Business.Interfaces.ISqlSelectDb iSqlSelectDb;

       public UserManager()
       {
           repository = new GenericRepository<UsersManager>(new RentEntities());
           iSqlSelectDb = new SqlSelectDb();
       }

       public UserManager(RentEntities context)
       {
           repository = new GenericRepository<UsersManager>(context);
       }

       public IEnumerable<UsersManager> Select()
       {
           return repository.Select();
       }

       public IEnumerable<UsersManager> Select(object id)
       {
           return repository.Select().Where(x =>x.ManagerId == (int) id);
       }

       public UsersManager SelectByUId(object id)
       {
           return repository.Select().FirstOrDefault(x => x.Uid == (int)id);
       }

       public void Insert(UsersManager obj)
       {
           repository.Insert(obj);
       }

       public void Update(UsersManager obj)
       {
           repository.Update(obj);
       }

       public void AddorUpdate(UsersManager obj)
       {
           SqlParameter[] oSqlParameters = new SqlParameter[2];
           oSqlParameters[0] = new SqlParameter("@ManagerId", obj.ManagerId);
           oSqlParameters[1] = new SqlParameter("@Uid", obj.Uid);

           var r = iSqlSelectDb.Update("UsersManager_Update", oSqlParameters);
       }

       public void Delete(object id)
       {
           repository.Delete(id);
       }
    }
}
