using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Starter.Infrastructure.QueriesWithExpressions.Expressions
{
    public interface IQueryExpression<T>
    {
        Expression<Func<T, bool>> AsExpression();
    }
}
