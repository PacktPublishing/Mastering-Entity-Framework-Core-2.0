using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Starter.Infrastructure.QueriesWithExpressions.Expressions
{
    public interface IQueryExpression<T>
    {
        Expression<Func<T, bool>> AsExpression();
    }
}
