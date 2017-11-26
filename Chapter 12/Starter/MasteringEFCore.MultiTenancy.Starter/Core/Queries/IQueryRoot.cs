using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Starter.Core.Queries
{
    public interface IQueryRoot
    {
        bool IncludeData { get; set; }
    }
}
