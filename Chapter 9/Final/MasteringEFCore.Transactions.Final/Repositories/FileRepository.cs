using MasteringEFCore.Transactions.Final.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Transactions.Final.Core.Commands;

namespace MasteringEFCore.Transactions.Final.Repositories
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
    }
}
