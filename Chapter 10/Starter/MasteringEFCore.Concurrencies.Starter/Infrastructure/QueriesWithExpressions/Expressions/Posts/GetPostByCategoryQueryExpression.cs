using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Starter.Data;
using MasteringEFCore.Concurrencies.Starter.Core.Queries.Posts;
using MasteringEFCore.Concurrencies.Starter.Core.Queries;
using MasteringEFCore.Concurrencies.Starter.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Concurrencies.Starter.Infrastructure.QueriesWithExpressions.Expressions.Posts
{
    public class GetPostByCategoryQueryExpression : IQueryExpression<Post>
    {
        public string Category { get; set; }

        public Expression<Func<Post, bool>> AsExpression()
        {
            return (x => x.Category.Name.ToLower().Contains(Category.ToLower()));
        }
    }
}
