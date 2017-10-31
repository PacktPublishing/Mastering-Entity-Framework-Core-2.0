using MasteringEFCore.Concurrencies.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Starter.Core.Commands.Files
{
    public interface IDeleteFileCommand<TReturn> :
        ICommandHandler<TReturn>, ICommandHandlerAsync<TReturn>
    {
    }
}
