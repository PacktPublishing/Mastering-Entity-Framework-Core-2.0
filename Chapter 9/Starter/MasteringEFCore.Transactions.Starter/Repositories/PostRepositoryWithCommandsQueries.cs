using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Transactions.Starter.Core.Commands;
using MasteringEFCore.Transactions.Starter.Core.Queries;
using MasteringEFCore.Transactions.Starter.Models;
using MasteringEFCore.Transactions.Starter.Data;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.Transactions.Starter.Handlers;

namespace MasteringEFCore.Transactions.Starter.Repositories
{
    public class PostRepositoryWithCommandsQueries : IPostRepositoryWithCommandsQueries
    {
        private readonly BlogContext _context;

        public PostRepositoryWithCommandsQueries(BlogContext context)
        {
            _context = context;
        }

        public IEnumerable<Post> Get<T>(T query)
            where T : IQueryHandler<IEnumerable<Post>>
        {
            return query.Handle();
        }

        public async Task<IEnumerable<Post>> GetAsync<T>(T query)
            where T : IQueryHandlerAsync<IEnumerable<Post>>
        {
            return await query.HandleAsync();
        }

        public Post GetSingle<T>(T query)
            where T : IQueryHandler<Post>
        {
            return query.Handle();
        }

        public async Task<Post> GetSingleAsync<T>(T query)
            where T : IQueryHandlerAsync<Post>
        {
            return await query.HandleAsync();
        }

        public int Execute<T>(T command) where T : ICommandHandler<int>
        {
            return command.Handle();
        }

        public async Task<int> ExecuteAsync<T>(T command) where T : ICommandHandlerAsync<int>
        {
            return await command.HandleAsync();
        }
    }
}
