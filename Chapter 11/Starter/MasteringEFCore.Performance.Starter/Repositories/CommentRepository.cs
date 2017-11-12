using MasteringEFCore.Performance.Starter.Core.Commands;
using MasteringEFCore.Performance.Starter.Core.Queries;
using MasteringEFCore.Performance.Starter.Data;
using MasteringEFCore.Performance.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Performance.Starter.Repositories
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
