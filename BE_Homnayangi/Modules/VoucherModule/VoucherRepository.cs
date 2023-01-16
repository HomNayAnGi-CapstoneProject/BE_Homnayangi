﻿using BE_Homnayangi.Modules.VoucherModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.VoucherModule
{
    public class VoucherRepository : Repository<Voucher>, IVoucherRepository
	{
        private readonly HomnayangiContext _db;

        public VoucherRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<Voucher>> GetVouchersBy(
            Expression<Func<Voucher, bool>> filter = null,
            Func<IQueryable<Voucher>, ICollection<Voucher>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<Voucher> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return options != null ? options(query).ToList() : await query.ToListAsync();
        }
    }
}

