using System;
using Library.Models;
using Repository.Repository.Interface;﻿
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderCookedDetailModule.Interface
{
	public interface IOrderCookedDetailRepository : IRepository<OrderCookedDetail>
	{
        public Task<ICollection<OrderCookedDetail>> GetOrderCookedDetailsBy(
               Expression<Func<OrderCookedDetail, bool>> filter = null,
               Func<IQueryable<OrderCookedDetail>, ICollection<OrderCookedDetail>> options = null,
               string includeProperties = null
           );
    }
}
