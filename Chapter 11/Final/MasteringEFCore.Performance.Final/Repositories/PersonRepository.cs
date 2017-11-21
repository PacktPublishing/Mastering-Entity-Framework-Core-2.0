using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Performance.Final.Core.Queries;
using MasteringEFCore.Performance.Final.Models;
using MasteringEFCore.Performance.Final.Data;

namespace MasteringEFCore.Performance.Final.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly BlogContext _context;

        public PersonRepository(BlogContext context)
        {
            _context = context;
        }

        public Person GetSingle<T>(T query) where T : IQueryHandler<Person>
        {
            return query.Handle();
        }

        public async Task<Person> GetSingleAsync<T>(T query) where T : IQueryHandlerAsync<Person>
        {
            return await query.HandleAsync();
        }
    }
}
