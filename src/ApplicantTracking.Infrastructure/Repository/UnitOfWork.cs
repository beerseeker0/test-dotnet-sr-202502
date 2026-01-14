using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicantTracking.Infrastructure.IRepository;

namespace ApplicantTracking.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICandidateRepository Candidates { get; }
        public ITimelineRepository Timelines { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Candidates = new CandidateRepository(context);
            Timelines = new TimelineRepository(context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
