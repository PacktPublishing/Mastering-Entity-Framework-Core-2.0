using MasteringEFCore.QueryObjectPattern.Final.Data;
using MasteringEFCore.QueryObjectPattern.Final.Handlers;
using MasteringEFCore.QueryObjectPattern.Final.Models;
using MasteringEFCore.QueryObjectPattern.Final.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.QueryObjectPattern.Final.Repositories
{
    public interface IPostRepositoryWithQueries
    {
        IEnumerable<Post> Get<T>(T query) where T : class;
        Task<IEnumerable<Post>> GetAsync<T>(T query) where T : class;
        Post GetSingle<T>(T query) where T : class;
        Task<Post> GetSingleAsync<T>(T query) where T : class;
    }
}
