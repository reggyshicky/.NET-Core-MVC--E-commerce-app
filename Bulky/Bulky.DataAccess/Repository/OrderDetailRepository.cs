﻿using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
/*public CategoryRepository(ApplicationDbContext db) : base(db): This line defines a 
constructor for the CategoryRepository class. The constructor takes an argument 
db of type ApplicationDbContext. When an instance of CategoryRepository is created, 
you need to provide an instance of ApplicationDbContext to this constructor.
base(db): In the constructor, you see base(db). This is calling the constructor of
the base class. In object-oriented programming, a derived class can call the
constructor of its base class to perform some common initialization tasks.
In this case, the base class constructor is being called with the db argument.
The base keyword is used to explicitly call a constructor of the base class.
In Entity Framework Core, the base class of a repository class is often EfRepository < TEntity >,
where TEntity is the type of entity that the repository deals with. By calling base(db),
you are passing the db (an instance of ApplicationDbContext) to the constructor of thebase class (EfRepository<TEntity>). This allows the base class to initialize itself using the
provided database context.
*/
