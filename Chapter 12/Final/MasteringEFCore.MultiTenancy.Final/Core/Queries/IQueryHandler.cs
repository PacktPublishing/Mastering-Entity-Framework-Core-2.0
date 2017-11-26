using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.MultiTenancy.Final.Models;

namespace MasteringEFCore.MultiTenancy.Final.Core.Queries
{
    public interface IQueryHandler<out TReturn> : IQueryRoot
    {
        TReturn Handle();
    }
}
