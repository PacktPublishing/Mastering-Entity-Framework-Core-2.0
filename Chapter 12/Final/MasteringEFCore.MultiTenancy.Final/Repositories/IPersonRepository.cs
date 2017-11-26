using MasteringEFCore.MultiTenancy.Final.Core.Queries;
using MasteringEFCore.MultiTenancy.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Repositories
{
    public interface IPersonRepository
    {
        Person GetSingle<T>(T query) where T : IQueryHandler<Person>;
        Task<Person> GetSingleAsync<T>(T query) where T : IQueryHandlerAsync<Person>;
    }
}
