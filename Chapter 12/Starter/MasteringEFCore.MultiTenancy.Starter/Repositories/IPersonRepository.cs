using MasteringEFCore.MultiTenancy.Starter.Core.Queries;
using MasteringEFCore.MultiTenancy.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Starter.Repositories
{
    public interface IPersonRepository
    {
        Person GetSingle<T>(T query) where T : IQueryHandler<Person>;
        Task<Person> GetSingleAsync<T>(T query) where T : IQueryHandlerAsync<Person>;
    }
}
