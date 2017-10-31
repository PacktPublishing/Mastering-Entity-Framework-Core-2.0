using MasteringEFCore.Concurrencies.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Starter.Core.Commands.Posts
{
    public interface ICreatePostCommand<TReturn> : 
        ICommandHandler<TReturn>, ICommandHandlerAsync<TReturn>
    {
    }
}
