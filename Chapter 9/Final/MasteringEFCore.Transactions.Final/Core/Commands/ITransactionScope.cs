using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Final.Core.Commands
{
    public interface ITransactionScope
    {
        void Commit();
        void Rollback();
    }
}
