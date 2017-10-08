using MasteringEFCore.QueryObjectPattern.Final.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MasteringEFCore.QueryObjectPattern.Final.Models;

namespace MasteringEFCore.QueryObjectPattern.Final.Queries
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
