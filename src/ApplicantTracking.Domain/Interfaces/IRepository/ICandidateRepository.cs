using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicantTracking.Domain.DTO;

namespace ApplicantTracking.Infrastructure.IRepository
{
    public interface ICandidateRepository
    {
        Task<List<Candidate>> ListAsync();
        Task<Candidate?> GetByIdAsync(int id);
        Task AddAsync(Candidate candidate);
        void Update(Candidate candidate);
        void Delete(Candidate candidate);
    }
}
