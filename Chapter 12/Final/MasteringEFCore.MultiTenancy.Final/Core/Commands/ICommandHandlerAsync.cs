using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Core.Commands
{
    public interface ICommandHandlerAsync<TReturn>
    {
        Task<TReturn> HandleAsync();
    }
}
