using System;
using Library.Models;
using Repository.Repository.Interface;﻿
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderModule.Interface
{
	public interface IOrderRepository : IRepository<Order>
	{
        public Task<ICollection<Order>> GetOrdersBy(
               Expression<Func<Order, bool>> filter = null,
               Func<IQueryable<Order>, ICollection<Order>> options = null,
               string includeProperties = null
           );

        public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction Transaction();
    }
}
