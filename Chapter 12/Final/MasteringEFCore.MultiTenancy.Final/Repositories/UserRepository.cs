using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.MultiTenancy.Final.Core.Queries;
using MasteringEFCore.MultiTenancy.Final.Models;
using MasteringEFCore.MultiTenancy.Final.Data;

namespace MasteringEFCore.MultiTenancy.Final.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;

        public UserRepository(BlogContext context)
        {
            _context = context;
        }

        public User GetSingle<T>(T query) where T : IQueryHandler<User>
        {
            return query.Handle();
        }

        public async Task<User> GetSingleAsync<T>(T query) where T : IQueryHandlerAsync<User>
        {
            return await query.HandleAsync();
        }
    }
}
