using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.MultiTenancy.Starter.Models;

namespace MasteringEFCore.MultiTenancy.Starter.Core.Queries
{
    public interface IQueryHandler<out TReturn> : IQueryRoot
    {
        TReturn Handle();
    }
}
