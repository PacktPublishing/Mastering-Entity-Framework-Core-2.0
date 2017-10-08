using MasteringEFCore.Transactions.Final.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Final.Infrastructure.Queries
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
