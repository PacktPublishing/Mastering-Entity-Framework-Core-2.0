using MasteringEFCore.MultiTenancy.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Core.Queries.Posts
{
    public interface IGetPostByUrlQuery<T> : 
        IQueryHandler<Post>, IQueryHandlerAsync<Post>
    {
        string Url { get; set; }
    }
}
