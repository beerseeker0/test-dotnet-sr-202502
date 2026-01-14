using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicantTracking.Domain.DTO;
using ApplicantTracking.Domain.Interfaces.IRepository;

namespace ApplicantTracking.Tests
{
    public class FakeReadDbContext : IApplicationReadDbContext
    {
        private readonly Dictionary<Type, IQueryable<object>> _sets = new();

        public void AddSet<T>(IEnumerable<T> data)
            where T : class
        {
            _sets[typeof(T)] = data.AsQueryable().Cast<object>();
        }

        public IQueryable<T> Set<T>() where T : class
        {
            if (!_sets.ContainsKey(typeof(T)))
                return Enumerable.Empty<T>().AsQueryable();

            return _sets[typeof(T)].Cast<T>();
        }
    }
}
