using MasteringEFCore.MultiTenancy.Final.Core.Commands;
using MasteringEFCore.MultiTenancy.Final.Core.Queries;
using MasteringEFCore.MultiTenancy.Final.Data;
using MasteringEFCore.MultiTenancy.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext _context;

        public CommentRepository(BlogContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> Get<T>(T query)
            where T : IQueryHandler<IEnumerable<Comment>>
        {
            return query.Handle();
        }

        public async Task<IEnumerable<Comment>> GetAsync<T>(T query)
            where T : IQueryHandlerAsync<IEnumerable<Comment>>
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
