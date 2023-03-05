using System;
using Library.Models;
using Repository.Repository.Interface;﻿
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderIngredientDetailModule.Interface
{
	public interface IOrderIngredientDetailRepository : IRepository<OrderIngredientDetail>
	{
        public Task<ICollection<OrderIngredientDetail>> GetOrderIngredientDetailsBy(
               Expression<Func<OrderIngredientDetail, bool>> filter = null,
               Func<IQueryable<OrderIngredientDetail>, ICollection<OrderIngredientDetail>> options = null,
               string includeProperties = null
           );
    }
}
