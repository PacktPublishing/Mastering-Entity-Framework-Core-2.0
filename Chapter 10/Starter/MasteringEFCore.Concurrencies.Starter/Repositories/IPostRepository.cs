using MasteringEFCore.Concurrencies.Starter.Data;
using MasteringEFCore.Concurrencies.Starter.Models;
using MasteringEFCore.Concurrencies.Starter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Starter.Core.Commands;
using MasteringEFCore.Concurrencies.Starter.Core.Queries;

namespace MasteringEFCore.Concurrencies.Starter.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> Get<T>(T query) where T : IQueryHandler<IEnumerable<Post>>;
        Task<IEnumerable<Post>> GetAsync<T>(T query) where T : IQueryHandlerAsync<IEnumerable<Post>>;
        Post GetSingle<T>(T query) where T : IQueryHandler<Post>;
        Task<Post> GetSingleAsync<T>(T query) where T : IQueryHandlerAsync<Post>;
        int Execute<T>(T command) where T : ICommandHandler<int>;
        Task<int> ExecuteAsync<T>(T command) where T : ICommandHandlerAsync<int>;
    }
}
