using BE_Homnayangi.Modules.PriceNoteModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.PriceNoteModule
{
    public class PriceNoteRepository: Repository<PriceNote>, IPriceNoteRepository
    {
        private readonly HomnayangiContext _db;

        public PriceNoteRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<PriceNote>> GetPriceNotesBy(
            Expression<Func<PriceNote,
            bool>> filter = null,
            Func<IQueryable<PriceNote>,
                ICollection<PriceNote>> options = null,
            string includeProperties = null
            )
        {
            IQueryable<PriceNote> query = DbSet;

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
