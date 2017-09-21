using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using Rent.DataAccess.Interfaces;

namespace Rent.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal Rent.Entities.RentEntities context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository()
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public GenericRepository(Rent.Entities.RentEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Select()
        {
            IQueryable<TEntity> q = dbSet;
            return q.ToList();
        }

        public virtual IEnumerable<TEntity> Selects(object id)
        {
          //  using (context)
            {
                return (IEnumerable<TEntity>) dbSet.Find(id);
            }
        }

        public virtual TEntity Select(object id)
        {
           // using (context)
            {
                return dbSet.Find(id);
            }
        }

        public IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> filter)
        {

            var resetSet = filter != null ? dbSet.Where(filter).AsQueryable() : dbSet.AsQueryable();
            //resetSet = rows == 0 ? resetSet.Take(row) : resetSet.OrderBy(filter).Skip(rows).Take(row);

            return resetSet.AsQueryable();
        }

        public virtual void Insert(TEntity entity)
        {
          //  using (context)
            {
                dbSet.Add(entity);
                Save();
            }
        }

        public virtual void AddorUpdate(TEntity entity)
        {
            dbSet.AddOrUpdate(entity);
            Save();
        }

        public virtual void Delete(object id)
        {
            TEntity entity = dbSet.Find(id);
            //Delete(entity);
            context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public virtual void Update(TEntity entity)
        {
           // using (context)
            {
                dbSet.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                Save();
            }
        }

        private void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                throw new DbEntityValidationException(exception.Message);
            }
        }
    }
}
