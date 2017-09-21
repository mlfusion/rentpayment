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
   public class RentPayment : IRentPayment, IDisposable
   {
       private Rent.DataAccess.Interfaces.IGenericRepository<Rent.Entities.RentPayment> repository = null;
       private Rent.DataAccess.SqlDataAccess.ISqlDbHelper iSqlDbHelper;

       public RentPayment(RentEntities context)
        {
            repository = new GenericRepository<Entities.RentPayment>(context);
           iSqlDbHelper = new SqlDbHelper();
        }

        public IEnumerable<Entities.RentPayment> Select()
        {
            var r = repository.Select().Where(x => x.Active);
            return r;
        }

        public IEnumerable<Entities.RentPayment> Select(Entities.Models.Paging objPaging)
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
                   Phone = x.Field<string>("Phone"),
               },
               Paging = new Paging()
               {
                   TotalCount = x.Field<int>("TotalCount"),
                   PageSize = objPaging.PageSize,
                   CurrentPage = x.Field<int>("CurrentPage")
               },
               RentPaymentNoticeSendLog = new Entities.RentPaymentNoticeSendLog()
               {
                NoticeCreated  = x.Field<DateTime?>("NoticeCreated")
               }
           }).ToList();

           return users;
       }

        public IEnumerable<Entities.RentPayment> SelectByUid(object id)
        {
            var r = repository.Select().Where(x => x.Uid == (int)id && x.User.Active && x.Active);
           
            return r;
        }

       public IEnumerable<Entities.RentPayment> SelectByUidList(object id)
       {
           return repository.Select().Where(x => x.Uid == (int) id);
       }

       public Entities.RentPayment Select(object id)
        {
            return repository.Select(id);
        }

        public void Insert(Entities.RentPayment obj)
        {
            obj.Created = DateTime.Now;
            obj.Modifed = null;
            obj.Active = true;
            repository.Insert(obj);
        }

        public void Update(Entities.RentPayment obj)
        {
            obj.Modifed = DateTime.Now;
            repository.Update(obj);
        }

        public void Delete(object id)
        {
            var obj = Select(id);
            obj.Modifed = DateTime.Now;
            obj.Active = false;
            repository.Update(obj);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
