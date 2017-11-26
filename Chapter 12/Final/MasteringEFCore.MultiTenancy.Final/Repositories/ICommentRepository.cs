using MasteringEFCore.MultiTenancy.Final.Core.Commands;
using MasteringEFCore.MultiTenancy.Final.Core.Queries;
using MasteringEFCore.MultiTenancy.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Repositories
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
