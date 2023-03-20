using System;
using Library.Models;
using Repository.Repository.Interface;﻿
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderDetailModule.Interface
{
	public interface IOrderDetailRepository : IRepository<OrderDetail>
	{
        public Task<ICollection<OrderDetail>> GetOrderDetailsBy(
               Expression<Func<OrderDetail, bool>> filter = null,
               Func<IQueryable<OrderDetail>, ICollection<OrderDetail>> options = null,
               string includeProperties = null
           );
    }
}
