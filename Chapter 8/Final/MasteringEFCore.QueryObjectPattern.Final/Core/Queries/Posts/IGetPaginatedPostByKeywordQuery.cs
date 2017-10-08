using MasteringEFCore.QueryObjectPattern.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.QueryObjectPattern.Final.Core.Queries.Posts
{
    public interface IGetPaginatedPostByKeywordQuery<in T> : 
        IQueryHandler<IEnumerable<Post>>, IQueryHandlerAsync<IEnumerable<Post>>
    {
        string Keyword { get; set; }
        int PageNumber { get; set; }
        int PageCount { get; set; }
    }
}
