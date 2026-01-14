using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicantTracking.Domain.DTO;
using ApplicantTracking.Infrastructure.IRepository;

namespace ApplicantTracking.Infrastructure.Repository
{
    public class TimelineRepository : RepositoryBase<Timeline>, ITimelineRepository
    {
        public TimelineRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(Timeline timeline)
        {
            await _dbSet.AddAsync(timeline);
        }
    }
}
