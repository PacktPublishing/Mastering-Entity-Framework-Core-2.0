using MasteringEFCore.MultiTenancy.Final.Core.Queries.Users;
using MasteringEFCore.MultiTenancy.Final.Data;
using MasteringEFCore.MultiTenancy.Final.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Infrastructure.Queries.Users
{
    public class GetUserByAuthenticationQuery : QueryBase, IGetUserByAuthenticationQuery<GetUserByAuthenticationQuery>
    {
        public GetUserByAuthenticationQuery(BlogContext context) : base(context)
        {
        }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IncludeData { get; set; }

        public User Handle()
        {
            return Context.Users
                        .SingleOrDefault(x => x.Username.Equals(Username) 
                            && x.PasswordHash.Equals(PasswordHash));
        }

        public async Task<User> HandleAsync()
        {
            return await Context.Users
                        .SingleOrDefaultAsync(x => x.Username.Equals(Username)
                            && x.PasswordHash.Equals(PasswordHash));
        }
    }
}
