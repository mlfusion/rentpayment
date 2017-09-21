using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Rent.Business.Interfaces;
using Rent.DataAccess.Repositories;
using Rent.Entities;

namespace Rent.Business.Services
{
    public class LogError : ILogError
    {
        
        private static Rent.DataAccess.Interfaces.IGenericRepository<Rent.Entities.LogError> repository = null;

        public LogError(Entities.RentEntities context)
        {
            repository = new GenericRepository<Entities.LogError>(context);
        }

        public IEnumerable<Entities.LogError> Select()
        {
            return repository.Select();
        }

        public static void Insert(Exception exception, int uid)
        {
            Rent.Entities.RentEntities context = new RentEntities();
            repository = new GenericRepository<Entities.LogError>(context);

            Entities.LogError logError = new Entities.LogError();
            logError.Created = DateTime.Now;
            logError.Modifed = null;
            logError.Message = exception.Message;
            logError.InnerMessage = null;
            logError.Uid = uid;
            repository.Insert(logError);
        }
    }
}
