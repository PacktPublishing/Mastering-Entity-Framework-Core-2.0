using MasteringEFCore.MultiTenancy.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Starter.Core.Queries.Files
{
    public interface IGetFileByIdQuery<T> : 
        IQueryHandler<File>, IQueryHandlerAsync<File>
    {
        Guid? Id { get; set; }
    }
}
