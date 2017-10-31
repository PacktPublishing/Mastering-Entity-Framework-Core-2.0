using MasteringEFCore.Transactions.Final.Core.Commands;
using MasteringEFCore.Transactions.Final.Core.Queries;
using MasteringEFCore.Transactions.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Final.Repositories
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> Get<T>(T query) 
            where T : IQueryHandler<IEnumerable<Comment>>;
        Task<IEnumerable<Comment>> GetAsync<T>(T query) 
            where T : IQueryHandlerAsync<IEnumerable<Comment>>;
        int Execute<T>(T command) where T : ICommandHandler<int>;
        Task<int> ExecuteAsync<T>(T command) where T : ICommandHandlerAsync<int>;
    }
}
