using MasteringEFCore.Concurrencies.Final.Data;
using MasteringEFCore.Concurrencies.Final.Models;
using MasteringEFCore.Concurrencies.Final.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Final.Core.Commands;
using MasteringEFCore.Concurrencies.Final.Core.Queries;

namespace MasteringEFCore.Concurrencies.Final.Repositories
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
