using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Final.Models;

namespace MasteringEFCore.Concurrencies.Final.Core.Queries
{
    public interface IQueryHandler<out TReturn> : IQueryRoot
    {
        TReturn Handle();
    }
}
