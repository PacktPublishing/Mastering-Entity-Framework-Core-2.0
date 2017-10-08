using MasteringEFCore.QueryObjectPattern.Final.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.QueryObjectPattern.Final.Infrastructure.QueriesWithExpressions
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
