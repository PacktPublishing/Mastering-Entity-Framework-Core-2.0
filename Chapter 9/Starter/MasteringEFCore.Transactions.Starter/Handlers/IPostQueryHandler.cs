using MasteringEFCore.Transactions.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Starter.Handlers
{
    public interface IPostQueryHandler<T>  where T : class
    {
        IEnumerable<Post> Handle(T query);
        Task<IEnumerable<Post>> HandleAsync(T query);
    }
}
