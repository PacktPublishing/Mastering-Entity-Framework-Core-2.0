using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Starter.Models;

namespace MasteringEFCore.Concurrencies.Starter.Core.Queries
{
    public interface IQueryHandlerAsync<TReturn> : IQueryRoot
    {
        Task<TReturn> HandleAsync();
    }
}
