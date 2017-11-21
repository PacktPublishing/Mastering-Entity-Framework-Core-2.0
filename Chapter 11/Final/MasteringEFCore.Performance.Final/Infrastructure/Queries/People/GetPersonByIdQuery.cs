using MasteringEFCore.Performance.Final.Core.Queries.People;
using MasteringEFCore.Performance.Final.Data;
using MasteringEFCore.Performance.Final.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Performance.Final.Infrastructure.Queries.People
{
    public class GetPersonByIdQuery : QueryBase, IGetPersonByIdQuery<GetPersonByIdQuery>
    {
        public GetPersonByIdQuery(BlogContext context) : base(context)
        {
        }

        public int? Id { get; set; }
        public bool IncludeData { get; set; }

        public Person Handle()
        {
            return IncludeData
                           ? Context.People
                                .Include(p => p.User)
                               .SingleOrDefault(x => x.Id.Equals(Id))
                           : Context.People
                               .SingleOrDefault(x => x.Id.Equals(Id));
        }

        public async Task<Person> HandleAsync()
        {
            return IncludeData
                           ? await Context.People
                                .Include(p => p.User)
                               .SingleOrDefaultAsync(x => x.Id.Equals(Id))
                           : await Context.People
                               .SingleOrDefaultAsync(x => x.Id.Equals(Id));
        }
    }
}
