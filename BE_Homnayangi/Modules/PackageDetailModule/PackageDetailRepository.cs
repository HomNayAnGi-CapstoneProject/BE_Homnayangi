﻿using System;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.PackageDetailModule.Interface;

namespace BE_Homnayangi.Modules.PackageDetailModule
{
    public class PackageDetailRepository : Repository<PackageDetail>, IPackageDetailRepository
    {
        private readonly HomnayangiContext _db;

        public PackageDetailRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<PackageDetail>> GetPackageDetailsBy(Expression<Func<PackageDetail, bool>> filter = null, Func<IQueryable<PackageDetail>, ICollection<PackageDetail>> options = null, string includeProperties = null)
        {
            IQueryable<PackageDetail> query = DbSet;

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

