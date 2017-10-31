using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Final.Core.Commands
{
    public interface ITransactionScope
    {
        void Commit();
        void Rollback();
    }
}
