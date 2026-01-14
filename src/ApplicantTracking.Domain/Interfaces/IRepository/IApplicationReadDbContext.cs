using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTracking.Domain.Interfaces.IRepository
{
    public interface IApplicationReadDbContext
    {
        IQueryable<T> Set<T>() where T : class;
    }
}
