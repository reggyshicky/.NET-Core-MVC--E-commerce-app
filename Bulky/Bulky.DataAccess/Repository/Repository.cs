using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //this is a keyword that refers to the current instance of the class,
            //Putting it all together, when you execute this.dbSet = _db.Set<T>();, you are telling your repository class to set 
            //its dbSet field to the DbSet<T> associated with the entity type T in the provided ApplicationDbContext.This allows 
            //your repository to interact with the specific database table that corresponds to the entity type T.So, any operations
            //you perform on dbSet will be carried out on the database table related to the entity T.
            //_db.Categories == dbset
            //_db.Products.Include(u => u.Category).Include(u=> u.CategoryId);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet; //you are assigning the dbSet (which is of type DbSet<T>)
                               //to the query variable, effectively making query a queryable
                               //collection of your entity type.
            }
            else
            {
                query = dbSet.AsNoTracking();

            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }
        //filter is a parameter representing a filtering condition expressed as a
        //lambda expression(e.g., c => c.Id == 123). This lambda expression defines
        ////how you want to filter the data;
        //query.FirstOrDefault() is another method call on the query variable.It's used to retrieve the first element from the filteredquery result.
        //FirstOrDefault() is a method provided by Entity Framework(and LINQ in general) that returns the first element of the 
        //query result or null if there are no matching elements. If your query doesn't find any matching records, 
        //it returns null.
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }


        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
            //entities are instances of classes.

        }
    }
}
