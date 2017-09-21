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
    public class RentPaymentNoticeSendLog : IRentPaymentNoticeSendLog
    {
        private Rent.DataAccess.Interfaces.IGenericRepository<Rent.Entities.RentPaymentNoticeSendLog> repository = null;

        public RentPaymentNoticeSendLog(RentEntities context)
        {
            repository = new GenericRepository<Entities.RentPaymentNoticeSendLog>(context);
        }

        public IEnumerable<Entities.RentPaymentNoticeSendLog> Select()
        {
            return repository.Select();
        }

        public Entities.RentPaymentNoticeSendLog SelectByRentPaymentId(object id)
        {
            return repository.Select().FirstOrDefault(x => x.RentPaymentId == (int) id);
        }

        public Entities.RentPaymentNoticeSendLog Select(object id)
        {
            return repository.Select(id);
        }

        public void Insert(Entities.RentPaymentNoticeSendLog obj)
        {
            repository.Insert(obj);
        }

        public static int Insert(StringBuilder stringBuilder, Entities.RentPayment obj, string email)
        {
            Rent.Entities.RentPaymentNoticeSendLog objRentPaymentNoticeSendLog = new Entities.RentPaymentNoticeSendLog();
            objRentPaymentNoticeSendLog.Created = DateTime.Now;
            objRentPaymentNoticeSendLog.Message = stringBuilder.ToString();
            objRentPaymentNoticeSendLog.Uid = obj.Uid;
            objRentPaymentNoticeSendLog.RentPaymentId = obj.RentPaymentId;
            objRentPaymentNoticeSendLog.Email = email;
            objRentPaymentNoticeSendLog.Modifed = null;

            SqlParameter[] _sqlParameters = new SqlParameter[4];
            _sqlParameters[0] = new SqlParameter("@Message", objRentPaymentNoticeSendLog.Message);
            _sqlParameters[1] = new SqlParameter("@Uid", objRentPaymentNoticeSendLog.Uid);
            _sqlParameters[2] = new SqlParameter("@RentPaymentId", objRentPaymentNoticeSendLog.RentPaymentId);
            _sqlParameters[3] = new SqlParameter("@Email", objRentPaymentNoticeSendLog.Email);

            Rent.DataAccess.SqlDataAccess.ISqlDbHelper _iSqlDbHelper = new SqlDbHelper();

            var x = _iSqlDbHelper.ExecuteScalar("RentPaymentNoticeSendLog_Update", CommandType.StoredProcedure,
                _sqlParameters);
            return x;
        }

        public void Update(Entities.RentPaymentNoticeSendLog obj)
        {
            obj.Modifed = DateTime.Now;
            repository.Update(obj);
        }
    }
}
