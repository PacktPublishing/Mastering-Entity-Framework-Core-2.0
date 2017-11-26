using MasteringEFCore.MultiTenancy.Starter.Core.Queries.Files;
using MasteringEFCore.MultiTenancy.Starter.Data;
using MasteringEFCore.MultiTenancy.Starter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Starter.Infrastructure.Queries.Files
{
    public class GetFileByIdQuery : QueryFileBase, IGetFileByIdQuery<GetFileByIdQuery>
    {
        public GetFileByIdQuery(BlogFilesContext context) : base(context)
        {
        }

        public Guid? Id { get; set; }
        public bool IncludeData { get; set; }

        public File Handle()
        {
            return Context.Files
                .SingleOrDefault(x => x.Id.Equals(Id));
        }

        public async Task<File> HandleAsync()
        {
            return await Context.Files
                .SingleOrDefaultAsync(x => x.Id.Equals(Id));
        }
    }
}
