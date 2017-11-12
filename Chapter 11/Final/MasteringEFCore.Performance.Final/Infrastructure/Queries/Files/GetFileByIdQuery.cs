using MasteringEFCore.Performance.Final.Core.Queries.Files;
using MasteringEFCore.Performance.Final.Data;
using MasteringEFCore.Performance.Final.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Performance.Final.Infrastructure.Queries.Files
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
