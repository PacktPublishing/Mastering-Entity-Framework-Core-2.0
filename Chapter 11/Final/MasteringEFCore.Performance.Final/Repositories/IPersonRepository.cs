using MasteringEFCore.Performance.Final.Core.Queries;
using MasteringEFCore.Performance.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Performance.Final.Repositories
{
    public interface IPersonRepository
    {
        Person GetSingle<T>(T query) where T : IQueryHandler<Person>;
        Task<Person> GetSingleAsync<T>(T query) where T : IQueryHandlerAsync<Person>;
    }
}
