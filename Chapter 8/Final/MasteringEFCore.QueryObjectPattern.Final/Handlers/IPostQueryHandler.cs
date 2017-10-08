using MasteringEFCore.QueryObjectPattern.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.QueryObjectPattern.Final.Handlers
{
    public interface IPostQueryHandler<T>  where T : class
    {
        IEnumerable<Post> Handle(T query);
        Task<IEnumerable<Post>> HandleAsync(T query);
    }
}
