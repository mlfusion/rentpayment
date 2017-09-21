using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Business.Interfaces;
using Rent.DataAccess.Repositories;
using Rent.Entities;

namespace Rent.Business.Services
{
    public class UsersPassword : IUserPassword, IDisposable
    {
        private Rent.DataAccess.Interfaces.IGenericRepository<Rent.Entities.UsersPassword> repository = null;

        public UsersPassword(RentEntities context)
        {
            repository = new GenericRepository<Entities.UsersPassword>(context);
        }

        public UsersPassword()
        {
            // TODO: Complete member initialization
            repository = new GenericRepository<Entities.UsersPassword>(new RentEntities());
        }

        public UsersPassword(object id)
        {
            Select(id);
        }

        public IEnumerable<Entities.UsersPassword> Select()
        {
            return repository.Select();
        }

        public Entities.UsersPassword Select(object id)
        {
            return repository.Select().FirstOrDefault(x => x.Uid == (int) id);
        }

        public void Insert(Entities.UsersPassword obj)
        {
            obj.ConfirmPassword = obj.Password;
            obj.Created = DateTime.Now;
            obj.Modifed = null;
            obj.Active = true;
            repository.Insert(obj);
        }

        public void Update(Entities.UsersPassword obj)
        {
            obj.ConfirmPassword = obj.Password;
            obj.Modifed = DateTime.Now;
            repository.Update(obj);
        }

        public void Delete(object id)
        {
            var obj = Select(id);
            obj.Modifed = DateTime.Now;
            obj.Active = false;
            repository.Update(obj);

            //repository.Delete(id);
        }

        public int Login(Entities.UsersPassword obj)
        {
            // var b = true;
            var user =
                Select()
                    .Where(x => x.User.Email == obj.User.Email.ToLower() && x.Password == obj.Password && x.User.Active)
                    .FirstOrDefault();

            return user != null ? user.Uid : 0;
        }

        public bool DefaultPassword(object id)
        {
            bool b = false;
            var password = Select(id).Password;

            if (password == Properties.Settings.Default.DefaultPassword)
                b = true;
            return b;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
