using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T will be Category or any other generic model on whicj we want to perform the CRUD operation
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false); //get individual FirstOrDefault(u=>u.Id==id);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity); //which will delete multiple entities in a single call.

    }
}
