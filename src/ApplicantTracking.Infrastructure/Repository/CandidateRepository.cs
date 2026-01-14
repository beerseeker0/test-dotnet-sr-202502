using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicantTracking.Domain.DTO;
using ApplicantTracking.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApplicantTracking.Infrastructure.Repository
{
    public class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
    {
        public CandidateRepository(AppDbContext context) : base(context) { }


        public Task<List<Candidate>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Candidate?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.IdCandidate == id);
        }

        public async Task AddAsync(Candidate candidate)
        {
            await _dbSet.AddAsync(candidate);
        }

        public void Update(Candidate candidate)
        {
            _dbSet.Update(candidate);
        }

        public void Delete(Candidate candidate)
        {
            _dbSet.Remove(candidate);
        }
    }
}
