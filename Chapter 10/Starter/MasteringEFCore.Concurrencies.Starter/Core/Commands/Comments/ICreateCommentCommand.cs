using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Starter.Core.Commands.Comments
{
    public interface ICreateCommentCommand<TReturn> : 
        ICommandHandler<TReturn>, ICommandHandlerAsync<TReturn>
    {
    }
}
