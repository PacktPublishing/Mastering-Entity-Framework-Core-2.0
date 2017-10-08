using MasteringEFCore.Transactions.Starter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Starter.Infrastructure.Commands
{
    public class CommandBase
    {
        internal readonly BlogContext Context;

        public CommandBase(BlogContext context)
        {
            Context = context;
        }
    }
}
