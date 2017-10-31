using MasteringEFCore.Concurrencies.Starter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Starter.Core.Commands;
using MasteringEFCore.Concurrencies.Starter.Core.Queries;
using MasteringEFCore.Concurrencies.Starter.Models;

namespace MasteringEFCore.Concurrencies.Starter.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly BlogFilesContext _context;
        public FileRepository(BlogFilesContext context)
        {
            _context = context;
        }

        public int Execute<T>(T command) where T : ICommandHandler<int>
        {
            return command.Handle();
        }

        public async Task<int> ExecuteAsync<T>(T command) where T : ICommandHandlerAsync<int>
        {
            return await command.HandleAsync();
        }

        public File GetSingle<T>(T query) where T : IQueryHandler<File>
        {
            return query.Handle();
        }

        public async Task<File> GetSingleAsync<T>(T query) where T : IQueryHandlerAsync<File>
        {
            return await query.HandleAsync();
        }
    }
}
