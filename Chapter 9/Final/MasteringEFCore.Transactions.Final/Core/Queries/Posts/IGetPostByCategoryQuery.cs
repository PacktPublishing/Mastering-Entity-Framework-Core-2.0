using MasteringEFCore.Transactions.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Final.Core.Queries.Posts
{
    public interface IGetPostByCategoryQuery<T> : 
        IQueryHandler<IEnumerable<Post>>, IQueryHandlerAsync<IEnumerable<Post>>
    {
        string Category { get; set; }
    }
}
