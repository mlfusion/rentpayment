using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Business.Interfaces;
using Rent.DataAccess.SqlDataAccess;
using Rent.Entities;
using Rent.Entities.Models;
using System.Collections;

namespace Rent.Business.Services
{
    public class SqlSelectDb : ISqlSelectDb
    {
        private Rent.DataAccess.SqlDataAccess.ISqlDbHelper iSqlDbHelper;

        public SqlSelectDb()
        {
            iSqlDbHelper = new SqlDbHelper();
        }

        public IEnumerable<Entities.User> UserSelectBySearch(Paging objPaging)
        {
            var storeProc = "Users_Select";

            SqlParameter[] objSqlParameters = new SqlParameter[5];
            //objSqlParameters[0] = new SqlParameter("@ActionId", objPaging.ActionId);
            objSqlParameters[0] = new SqlParameter("@CurrentPage", objPaging.CurrentPage);
            objSqlParameters[1] = new SqlParameter("@Uid", objPaging.Id);
            objSqlParameters[2] = new SqlParameter("@SortColumn", objPaging.SortColumn);
            objSqlParameters[3] = new SqlParameter("@SortOrder", objPaging.SortOrder);
            objSqlParameters[4] = new SqlParameter("@Search", objPaging.Search);

            var d = iSqlDbHelper.ExecuteDataTable(storeProc, CommandType.StoredProcedure, objSqlParameters);
            IEnumerable<Entities.User> users = d.AsEnumerable().Select(x => new Entities.User()
            {
                Uid = x.Field<int>("Uid"),
                Name = x.Field<string>("Name"),
                Email = x.Field<string>("Email"),
                Phone = x.Field<string>("Phone"),
                Created = x.Field<DateTime>("Created"),
                Modifed = x.Field<DateTime?>("Modifed"),
                Active = x.Field<bool>("Active"),
                UsersRole = new UsersRole()
                {
                    UserRoleId = x.Field<int>("UserRoleId"),
                    Role1 = new Role1()
                    {
                        Name = x.Field<string>("RoleName")
                    }
                },
                Paging = new Paging()
                {
                    TotalCount = x.Field<int>("TotalCount"),
                    PageSize = objPaging.PageSize,
                    CurrentPage = objPaging.CurrentPage
                },
                RentPayment = new Entities.RentPayment()
                {
                   PaymentCount = x.Field<int>("Payments")
                }
            }).ToList();

            return users;
        }

        public IEnumerable<UsersLog> UsersLogSelectBySearch(Paging objPaging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entities.RentPayment> RentPaymentSelectBySearch(Paging objPaging)
        {
            var storeProc = "RentPayment_Select";

            SqlParameter[] objSqlParameters = new SqlParameter[6];
            objSqlParameters[0] = new SqlParameter("@CurrentPage", objPaging.CurrentPage);
            objSqlParameters[1] = new SqlParameter("@SortColumn", objPaging.SortColumn);
            objSqlParameters[2] = new SqlParameter("@SortOrder", objPaging.SortOrder);
            objSqlParameters[3] = new SqlParameter("@Search", objPaging.Search);
            objSqlParameters[4] = new SqlParameter("@ActionID", objPaging.ActionId);
            objSqlParameters[5] = new SqlParameter("@Uid", objPaging.Id);

            var d = iSqlDbHelper.ExecuteDataTable(storeProc, CommandType.StoredProcedure, objSqlParameters);
            
            IEnumerable<Entities.RentPayment> users = d.AsEnumerable().Select(x => new Entities.RentPayment()
            {
                Uid = x.Field<int>("Uid"),
                RentPaymentId = x.Field<int>("RentPaymentId"),
                PaymentDate = x.Field<DateTime>("PaymentDate"),
                Payment = x.Field<double>("Payment"),
                Created = x.Field<DateTime>("Created"),
                Modifed = x.Field<DateTime?>("Modifed"),
                Active = x.Field<bool>("Active"),
                
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

        public DataTable Select(string storeProc, SqlParameter[] oSqlParameters)
        {
            return iSqlDbHelper.ExecuteDataTable(storeProc, CommandType.StoredProcedure, oSqlParameters);
        }

        public int Update(string storeProc, SqlParameter[] oSqlParameters)
        {
            var r = iSqlDbHelper.ExecuteScalar(storeProc, CommandType.StoredProcedure, oSqlParameters);
            return r;
        }

        public int Delete(string storeProc, SqlParameter[] oSqlParameters)
        {
            var r = iSqlDbHelper.ExecuteScalar(storeProc, CommandType.StoredProcedure, oSqlParameters);
            return r;
        }
    }
}
