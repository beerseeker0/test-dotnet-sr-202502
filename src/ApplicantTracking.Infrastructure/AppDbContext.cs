using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ApplicantTracking.Domain.DTO;
using ApplicantTracking.Domain.Interfaces;
using ApplicantTracking.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApplicantTracking.Infrastructure
{
    public class AppDbContext : DbContext, IApplicationReadDbContext
    {
        public IQueryable<T> Set<T>() where T : class
        => base.Set<T>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Timeline> Timeline { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
