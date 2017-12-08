using MasteringEFCore.Transactions.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Starter.Handlers
{
    public interface IPostQuerySingleHandler<T> where T : class
    {
        Post Handle(T query);
        Task<Post> HandleAsync(T query);
    }
}
