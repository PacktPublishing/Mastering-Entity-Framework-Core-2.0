using MasteringEFCore.MultiTenancy.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Core.Queries.Files
{
    public interface IGetFileByIdQuery<T> : 
        IQueryHandler<File>, IQueryHandlerAsync<File>
    {
        Guid? Id { get; set; }
    }
}
