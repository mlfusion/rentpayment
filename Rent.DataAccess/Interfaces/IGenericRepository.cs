using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rent.DataAccess.Interfaces
{
   public interface IGenericRepository <T> where T : class
    {
        IEnumerable<T> Select();
        IEnumerable<T> Select(Expression<Func<T, bool>> filter);
        IEnumerable<T> Selects(object id);
        T Select(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void AddorUpdate(T obj);
    }
}
