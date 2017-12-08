using MasteringEFCore.Transactions.Starter.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MasteringEFCore.Transactions.Starter.Models;

namespace MasteringEFCore.Transactions.Starter.Queries
{
    public class GetAllPostsQuery
    {
        public GetAllPostsQuery(bool includeData)
        {
            this.IncludeData = includeData;
        }

        public bool IncludeData { get; set; }
    }
}
