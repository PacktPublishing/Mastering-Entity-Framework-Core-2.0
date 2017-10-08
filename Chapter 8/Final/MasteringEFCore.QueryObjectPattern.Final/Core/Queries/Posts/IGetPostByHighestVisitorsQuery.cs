using MasteringEFCore.QueryObjectPattern.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.QueryObjectPattern.Final.Core.Queries.Posts
{
    public interface IGetPostByHighestVisitorsQuery<T> : 
        IQueryHandler<IEnumerable<Post>>, IQueryHandlerAsync<IEnumerable<Post>>
    {
    }
}
