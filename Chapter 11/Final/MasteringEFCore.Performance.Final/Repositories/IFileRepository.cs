using MasteringEFCore.Performance.Final.Core.Commands;
using MasteringEFCore.Performance.Final.Core.Queries;
using MasteringEFCore.Performance.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Performance.Final.Repositories
{
    public interface IFileRepository
    {
        int Execute<T>(T command) where T : ICommandHandler<int>;
        Task<int> ExecuteAsync<T>(T command) where T : ICommandHandlerAsync<int>;
        File GetSingle<T>(T query) where T : IQueryHandler<File>;
        Task<File> GetSingleAsync<T>(T query) where T : IQueryHandlerAsync<File>;
    }
}
