using MasteringEFCore.Concurrencies.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Final.Core.Queries.Posts
{
    public interface IGetPostByAuthorQuery<T> : 
        IQueryHandler<IEnumerable<Post>>, IQueryHandlerAsync<IEnumerable<Post>>
    {
        string Author { get; set; }
    }
}
