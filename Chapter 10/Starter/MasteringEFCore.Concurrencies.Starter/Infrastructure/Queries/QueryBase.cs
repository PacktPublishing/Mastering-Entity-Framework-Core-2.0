using MasteringEFCore.Concurrencies.Starter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Starter.Infrastructure.Queries
{
    public class QueryBase
    {
        internal readonly BlogContext Context;

        public QueryBase(BlogContext context)
        {
            this.Context = context;
        }
    }
}
