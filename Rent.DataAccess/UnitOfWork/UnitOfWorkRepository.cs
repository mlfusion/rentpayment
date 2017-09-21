using System;
using Rent.DataAccess.Interfaces;
using Rent.DataAccess.Repositories;
using Rent.Entities;

namespace Rent.DataAccess.UnitOfWork
{
    public class UnitOfWorkRepository : IDisposable
    {
        private Rent.Entities.RentEntities context = new RentEntities(); 

        #region UserPassword
        private GenericRepository<Rent.Entities.UsersPassword> passwordRepository;

        public GenericRepository<Rent.Entities.UsersPassword> PasswordRepository
        {
            get
            {
                if(this.passwordRepository == null)
                    this.passwordRepository = new GenericRepository<UsersPassword>(context);
                return passwordRepository;
            }
        }
        #endregion

        #region User
        private GenericRepository<Rent.Entities.User> userRepository;

        public GenericRepository<Rent.Entities.User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new GenericRepository<User>(context);
                return userRepository;
            }
        }
        #endregion

        #region UserLog
        private GenericRepository<Rent.Entities.UsersLog> usersLogRepository;

        public GenericRepository<Rent.Entities.UsersLog> UsersLogRepository
        {
            get
            {
                if (this.usersLogRepository == null)
                    this.usersLogRepository = new GenericRepository<Rent.Entities.UsersLog>(context);
                return usersLogRepository;
            }
        }
        #endregion

        #region RentPayment
        private GenericRepository<Rent.Entities.RentPayment> rentPaymentRepository;

        public GenericRepository<Rent.Entities.RentPayment> RentPaymentRepository
        {
            get
            {
                if (this.rentPaymentRepository == null)
                    this.rentPaymentRepository = new GenericRepository<Rent.Entities.RentPayment>(context);
                return rentPaymentRepository;
            }
        }
        #endregion

        #region RentPaymentNoticeSendLog
        private GenericRepository<Rent.Entities.RentPaymentNoticeSendLog> rentPaymentNoticeSendLogRepository;

        public GenericRepository<Rent.Entities.RentPaymentNoticeSendLog> RentPaymentNoticeSendLogRepository
        {
            get
            {
                if (this.rentPaymentNoticeSendLogRepository == null)
                    this.rentPaymentNoticeSendLogRepository = new GenericRepository<Rent.Entities.RentPaymentNoticeSendLog>(context);
                return rentPaymentNoticeSendLogRepository;
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
