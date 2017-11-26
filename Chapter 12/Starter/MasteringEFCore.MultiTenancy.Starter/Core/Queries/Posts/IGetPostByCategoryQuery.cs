using MasteringEFCore.MultiTenancy.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Starter.Core.Queries.Posts
{
    public interface IGetPostByCategoryQuery<T> : 
        IQueryHandler<IEnumerable<Post>>, IQueryHandlerAsync<IEnumerable<Post>>
    {
        string Category { get; set; }
    }
}
