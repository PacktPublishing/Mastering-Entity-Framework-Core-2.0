using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Starter.Core.Commands
{
    public interface ITransactionScope
    {
        void Commit();
        void Rollback();
    }
}
