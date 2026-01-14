using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicantTracking.Domain.DTO;

namespace ApplicantTracking.Infrastructure.IRepository
{
    public interface ITimelineRepository
    {
        Task AddAsync(Timeline timeline);
    }
}
