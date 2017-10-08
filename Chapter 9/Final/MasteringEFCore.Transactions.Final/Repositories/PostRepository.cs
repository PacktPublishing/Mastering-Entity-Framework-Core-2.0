using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Transactions.Final.Core.Commands;
using MasteringEFCore.Transactions.Final.Core.Queries;
using MasteringEFCore.Transactions.Final.Models;
using MasteringEFCore.Transactions.Final.Data;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Transactions.Final.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context)
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
