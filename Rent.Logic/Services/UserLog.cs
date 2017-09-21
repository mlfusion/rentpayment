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
using Rent.Entities.Models;

namespace Rent.Business.Services
{
   public class UserLog : IUserLog, IDisposable
    {
        private Rent.DataAccess.Interfaces.IGenericRepository<Rent.Entities.UsersLog> repository = null;
        private Rent.DataAccess.SqlDataAccess.ISqlDbHelper iSqlDbHelper;

        public UserLog(Entities.RentEntities context)
        {
            repository = new GenericRepository<Entities.UsersLog>(context);
            iSqlDbHelper = new SqlDbHelper();
        }

        public void Insert(Entities.UsersLog obj)
        {
            obj.Created = DateTime.Now;
            obj.Modifed = null;
            repository.Insert(obj);
        }

        public IEnumerable<Entities.UsersLog> Select()
        {
            return repository.Select();
        }

        public IEnumerable<Entities.UsersLog> Select(Entities.Models.Paging objPaging)
        {
            var storeProc = "UsersLog_Select";

            SqlParameter[] objSqlParameters = new SqlParameter[6];
            objSqlParameters[0] = new SqlParameter("@CurrentPage", objPaging.CurrentPage);
            objSqlParameters[1] = new SqlParameter("@SortColumn", objPaging.SortColumn);
            objSqlParameters[2] = new SqlParameter("@SortOrder", objPaging.SortOrder);
            objSqlParameters[3] = new SqlParameter("@Search", objPaging.Search);
            objSqlParameters[4] = new SqlParameter("@ActionID", objPaging.ActionId);
            objSqlParameters[5] = new SqlParameter("@Uid", objPaging.Id);

            var d = iSqlDbHelper.ExecuteDataTable(storeProc, CommandType.StoredProcedure, objSqlParameters);

            IEnumerable<Entities.UsersLog> users = d.AsEnumerable().Select(x => new Entities.UsersLog()
            {
                Uid = x.Field<int>("Uid"),
                UserLogId = x.Field<int>("UserLogId"),
                IpAddress = x.Field<string>("IpAddress"),
                Page = x.Field<string>("Page"),
                Created = x.Field<DateTime>("Created"),
                Modifed = x.Field<DateTime?>("Modifed"),

                User = new Entities.User()
                {
                    Name = x.Field<string>("Name"),
                    Email = x.Field<string>("Email"),
                }, 
                Paging = new Paging()
                {
                    TotalCount = x.Field<int>("TotalCount"),
                    PageSize = objPaging.PageSize,
                    CurrentPage = x.Field<int>("CurrentPage")
                }
            }).ToList();

            return users;
        }

        public IEnumerable<Entities.UsersLog> SelectByUid(object id)
        {
            return repository.Select().Where(x =>x.Uid == (int) id);
        }

       public void Dispose()
       {
           throw new NotImplementedException();
       }
    }
}
