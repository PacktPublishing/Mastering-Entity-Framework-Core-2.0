using MasteringEFCore.MultiTenancy.Final.Core.Queries;
using MasteringEFCore.MultiTenancy.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Repositories
{
    public interface IUserRepository
    {
        User GetSingle<T>(T query) where T : IQueryHandler<User>;
        Task<User> GetSingleAsync<T>(T query) where T : IQueryHandlerAsync<User>;
    }
}