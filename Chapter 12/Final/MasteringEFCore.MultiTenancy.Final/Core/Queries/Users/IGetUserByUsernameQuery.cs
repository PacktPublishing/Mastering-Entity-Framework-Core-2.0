using MasteringEFCore.MultiTenancy.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Core.Queries.Users
{
    public interface IGetUserByAuthenticationQuery<T> :
        IQueryHandler<User>, IQueryHandlerAsync<User>
    {
        string Username { get; set; }
        string PasswordHash { get; set; }
    }
}
