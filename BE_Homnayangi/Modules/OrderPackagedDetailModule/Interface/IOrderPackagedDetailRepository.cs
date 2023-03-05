using System;
using Library.Models;
using Repository.Repository.Interface;﻿
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderPackageDetailModule.Interface
{
	public interface IOrderPackageDetailRepository : IRepository<OrderPackageDetail>
	{
        public Task<ICollection<OrderPackageDetail>> GetOrderPackageDetailsBy(
               Expression<Func<OrderPackageDetail, bool>> filter = null,
               Func<IQueryable<OrderPackageDetail>, ICollection<OrderPackageDetail>> options = null,
               string includeProperties = null
           );
    }
}
