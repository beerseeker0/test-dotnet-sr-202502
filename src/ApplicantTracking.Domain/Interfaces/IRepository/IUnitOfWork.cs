using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTracking.Infrastructure.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICandidateRepository Candidates { get; }
        ITimelineRepository Timelines { get; }

        Task<int> CommitAsync();
    }
}
