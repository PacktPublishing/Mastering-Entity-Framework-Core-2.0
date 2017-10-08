using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.QueryObjectPattern.Final.Core.Commands
{
    public interface ICommandHandlerAsync<TReturn>
    {
        Task<TReturn> HandleAsync();
    }
}
