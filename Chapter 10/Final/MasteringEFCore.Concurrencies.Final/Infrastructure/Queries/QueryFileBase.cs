using MasteringEFCore.Concurrencies.Final.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Final.Infrastructure.Queries
{
    public class QueryFileBase
    {
        internal readonly BlogFilesContext Context;

        public QueryFileBase(BlogFilesContext context)
        {
            this.Context = context;
        }
    }
}
