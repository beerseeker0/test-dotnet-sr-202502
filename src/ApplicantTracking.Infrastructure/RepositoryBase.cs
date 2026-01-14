using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApplicantTracking.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected RepositoryBase(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>() as DbSet<T>;
        }
    }
}
