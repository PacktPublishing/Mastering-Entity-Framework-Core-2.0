using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.MultiTenancy.Final.Core.Queries;
using MasteringEFCore.MultiTenancy.Final.Models;
using MasteringEFCore.MultiTenancy.Final.Data;

namespace MasteringEFCore.MultiTenancy.Final.Repositories
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
