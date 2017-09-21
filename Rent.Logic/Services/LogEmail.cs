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
using Rent.Entities.Models;

namespace Rent.Business.Services
{
    public class LogEmail : ILogEmail
    {
        private Rent.DataAccess.Interfaces.IGenericRepository<Rent.Entities.RentPaymentNoticeSendLog> repository = null;
        private Rent.DataAccess.Interfaces.IGenericRepository<Rent.Entities.LogEmail> _ILogEmailrepository = null;
        private Rent.DataAccess.SqlDataAccess.ISqlDbHelper iSqlDbHelper;

        public LogEmail(RentEntities context)
        {
            repository = new GenericRepository<Entities.RentPaymentNoticeSendLog>(context);
            _ILogEmailrepository = new GenericRepository<Entities.LogEmail>(context);
            iSqlDbHelper = new SqlDbHelper();
        }

        public IEnumerable<Entities.LogEmail> Select()
        {
            return _ILogEmailrepository.Select();
        }

        public IEnumerable<Entities.LogEmail> Select(Entities.Models.Paging objPaging)
        {
            var storeProc = "LogEmail_Select";

            SqlParameter[] objSqlParameters = new SqlParameter[6];
            objSqlParameters[0] = new SqlParameter("@CurrentPage", objPaging.CurrentPage);
            objSqlParameters[1] = new SqlParameter("@SortColumn", objPaging.SortColumn);
            objSqlParameters[2] = new SqlParameter("@SortOrder", objPaging.SortOrder);
            objSqlParameters[3] = new SqlParameter("@Search", objPaging.Search);
            objSqlParameters[4] = new SqlParameter("@ActionID", objPaging.ActionId);
            objSqlParameters[5] = new SqlParameter("@Uid", objPaging.Id);

            var d = iSqlDbHelper.ExecuteDataTable(storeProc, CommandType.StoredProcedure, objSqlParameters);

            IEnumerable<Entities.LogEmail> users = d.AsEnumerable().Select(x => new Entities.LogEmail()
            {
                Uid = x.Field<int>("Uid"),
                LogId = x.Field<int>("LogId"),
                Message = x.Field<string>("Message"),
                Email = x.Field<string>("Email"),
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

        public IEnumerable<Entities.LogEmail> SelectByUid(object id)
        {
            return _ILogEmailrepository.Select().Where(x => x.Uid == (int) id).ToList();
        }

        public static int Insert(StringBuilder stringBuilder, int uid, string email)
        {
            SqlParameter[] _sqlParameters = new SqlParameter[3];
            _sqlParameters[0] = new SqlParameter("@Uid", uid);
            _sqlParameters[1] = new SqlParameter("@Message", stringBuilder.ToString());
            _sqlParameters[2] = new SqlParameter("@Email", email);

            Rent.DataAccess.SqlDataAccess.ISqlDbHelper _iSqlDbHelper = new SqlDbHelper();

            var x = _iSqlDbHelper.ExecuteScalar("LogEmail_Insert", CommandType.StoredProcedure, _sqlParameters);
            return x;
        }
    }
}
