using MasteringEFCore.Concurrencies.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Final.Core.Commands.Files
{
    public interface IDeleteFileCommand<TReturn> :
        ICommandHandler<TReturn>, ICommandHandlerAsync<TReturn>
    {
    }
}
