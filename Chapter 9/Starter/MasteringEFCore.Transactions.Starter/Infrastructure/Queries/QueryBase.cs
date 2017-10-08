using MasteringEFCore.Transactions.Starter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Starter.Infrastructure.Queries
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
