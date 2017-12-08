using MasteringEFCore.Transactions.Starter.Data;
using MasteringEFCore.Transactions.Starter.Handlers;
using MasteringEFCore.Transactions.Starter.Models;
using MasteringEFCore.Transactions.Starter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Starter.Repositories
{
    public interface IPostRepositoryWithQueries
    {
        IEnumerable<Post> Get<T>(T query) where T : class;
        Task<IEnumerable<Post>> GetAsync<T>(T query) where T : class;
        Post GetSingle<T>(T query) where T : class;
        Task<Post> GetSingleAsync<T>(T query) where T : class;
    }
}
