using MasteringEFCore.QueryObjectPattern.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.QueryObjectPattern.Final.Handlers
{
    public interface IPostQuerySingleHandler<T> where T : class
    {
        Post Handle(T query);
        Task<Post> HandleAsync(T query);
    }
}
